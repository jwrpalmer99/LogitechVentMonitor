using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using Ini.Net;
using Essy.Tools.InputBox;
using System.Security.Principal;

namespace WhoIsSpeaking
{
    public partial class Form1 : Form
    {
        private int initialScrollPause = 20; //starting delay in multiples of scroll speed
        private int stickyDelay = 100; //delay between keys in sticky mode
        private bool stickyRepeat = true;
        private int scrollPosition;
        private int spellPosition;
        private BackgroundWorker bwSpell;
        public static string spellText="";
        private int maxScroll;
        private Timer timer1;
        private string scrollText;
        private bool ArxConnected = false;
        Process process = null;
               
        private KeyboardHook hook = new KeyboardHook();
        
        private IntPtr hWnd = IntPtr.Zero;
        private IntPtr hEdit = IntPtr.Zero;

        private List<string> ventNames;

        internal static bool UseKeysaver = true;
        internal static int KeySaverTime = 30;
        private static bool useArx = true;
        internal static bool useAnimation = true;
        internal static bool useLogitechColours = true;
        internal static Color m_startColour = Color.Green;
        internal static Color m_endColour = Color.Red;
        internal static int m_fadespeed = 15;
        internal static int m_gradientspeed = 7;
        internal static bool m_Wave = true;
        internal static int m_AnimationSpeed = 100;
        internal static double m_WaveSpeed = 2.0;
        internal static double m_distanceFalloff = 1.0;
        internal static double m_KeysaveNewRippleInterval = 1500;
        LogitechArx.logiArxCbContext contextCallback;
   
        private enum LEDDisplay { Off, Scroll, Spell };
        private LEDDisplay LEDMode = LEDDisplay.Scroll;

        private static string currentProfile = "default";

        private HardwareMonitor hwmonitor;

        public Form1()
        {
            InitializeComponent();

            if (StartMinimized)
            {
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
            }

            notifyIcon1.BalloonTipClosed += (sender, e) => { var thisIcon = (NotifyIcon)sender; thisIcon.Visible = false; thisIcon.Dispose(); };
            WindowsPrincipal myPrincipal = new WindowsPrincipal (WindowsIdentity .GetCurrent());
            if (myPrincipal.IsInRole(WindowsBuiltInRole .Administrator) == false )
            {
            //show messagebox - displaying a messange to the user that rights are missing
            //MessageBox .Show("You need to run the application using the 'run as administrator' option" , "administrator right required" , MessageBoxButtons .OK, MessageBoxIcon .Exclamation); 
            }

            //read settings
            currentProfile = Properties.Settings.Default.ProfileDefault;
            LoadSettings(currentProfile);

            LogitechGSDK.LogiLedInit().ToString();            
            System.Threading.Thread.Sleep(1000); //pause to allow connection
            LogitechGSDK.LogiLedSaveCurrentLighting();
                        
            if (!useLogitechColours) LogitechGSDK.LogiLedSetLighting(0, 0, 0);
            if (useArx)
            {
                InitArx();
            }          
        }

        private void InitArx()
        {
            contextCallback.arxCallBack = new LogitechArx.logiArxCB(SDKCallback);
            contextCallback.arxContext = System.IntPtr.Zero;
            bool retVal = LogitechArx.LogiArxInit("ventrilo", "Ventrilo", ref contextCallback);
            if (!retVal)
            {
                int retCode = LogitechArx.LogiArxGetLastError();
                Debug.WriteLine("loading	sdk	failed:" + retCode);
                lblArxStatus.Text = "Failed";
                lblArxStatus.ForeColor = Color.DarkRed;
            }
            else
            {
                lblArxStatus.Text = "Connected";
                lblArxStatus.ForeColor = Color.DarkGreen;
                ArxConnected = true;
                System.Threading.Thread.Sleep(100);
                LogitechArx.LogiArxAddUTF8StringAs(getHTML(""), "name.html");
                LogitechArx.LogiArxSetIndex("name.html");
                timer1 = new Timer();
                timer1.Enabled = true;
                timer1.Interval = 100;
                timer1.Tick += timer1_Tick;
            }
        }

        private void SDKCallback(int eventType, int eventValue, System.String eventArg, System.IntPtr context)
        {
            if (eventType == LogitechArx.LOGI_ARX_EVENT_MOBILEDEVICE_ARRIVAL)
            {
                //Send	your	files	here
            }
            else if (eventType == LogitechArx.LOGI_ARX_EVENT_MOBILEDEVICE_REMOVAL)
            {
                //Device	disconnected
            }
            else if (eventType == LogitechArx.LOGI_ARX_EVENT_TAP_ON_TAG)
            {
                if (eventArg == "myBtn")
                {
                    //Do	something	on	this	input
                    MessageBox.Show("received button");
                }
            }
        }

        delegate void getUsersDelegate();

        void timer1_Tick(object sender, EventArgs e)
        {
            //do heatmap
            if (useArx || LEDMode != LEDDisplay.Off)
            {
                getUsersDelegate getusers = GetUsers;
                getusers.BeginInvoke(null, null);
            }
            //GetUsers();

            //getUsersDelegate getArxImage = getArxEmulator;
            //getArxImage.BeginInvoke(null, null);
        }
        
        private void getArxEmulator()
        {
            IntPtr hWnd;
            IntPtr hEdit;
            foreach (var p in Process.GetProcessesByName("LCore"))
            {
                process = p;
                Debug.WriteLine("found new LCore process " + p.Id.ToString());
            }

            if (process == null)
            {
                System.Threading.Thread.Sleep(1000);
                //pause so we dont look for processes constantly
                return;
            }
            hWnd = IntPtr.Zero;
            
            hWnd = FindWindow(null, "LCD Emulator");
           
            if (hWnd != IntPtr.Zero)
            {
                Bitmap b = getEmulatorImage(hWnd);
                b.Save(@"C:\Temp\b.png");
                Debug.WriteLine("image grabbed");
            }
            return ;
        }

        private Bitmap getEmulatorImage(IntPtr hWnd)
        {
            return MakeSnapshot(hWnd, true, Win32API.WindowShowStyle.Show);
        }

        private void GetUsers()
        {
            //find the "first" window
            if (process != null && process.HasExited)
            {
                hWnd = IntPtr.Zero;
                hEdit = IntPtr.Zero;
                process.WaitForExit();
                process = null;
                procHandle = IntPtr.Zero;
            }
            if (process == null )
            {
                foreach (var p in Process.GetProcessesByName("Ventrilo"))
                {
                    process = p;
                    Debug.WriteLine("found new ventrilo process " + p.Id.ToString());
                }

                if (process == null)
                {
                    //System.Threading.Thread.Sleep(1000);
                    //pause so we dont look for processes constantly
                    return;
                }
                hWnd = IntPtr.Zero;
                hEdit = IntPtr.Zero;
            }
            if (hWnd == IntPtr.Zero)
            {
                //allow time for vent to initialse
                System.Threading.Thread.Sleep(10);
                hWnd = FindWindow(null, "Ventrilo");
            }
            if (hWnd != IntPtr.Zero && hEdit == IntPtr.Zero)
            {
                //allow time for vent to initialse
                System.Threading.Thread.Sleep(10);
                //find the control window that has the text
                hEdit = FindWindowEx(hWnd, IntPtr.Zero, "SysTreeView32", null);

                //initialize the buffer.  using a StringBuilder here
                System.Text.StringBuilder sb = new System.Text.StringBuilder(255);  // or length from call with GETTEXTLENGTH

                //get the text from the child control
                int RetVal = SendMessage(hEdit, WM_GETTEXT, sb.Capacity, sb);

               
            }
            if (hEdit != IntPtr.Zero)
                getTreeText(hEdit);
            return ;
        }

        [DllImport("user32", EntryPoint = "SendMessageW")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [ DllImport("kernel32.dll")]
        public static extern IntPtr LocalAlloc(uint flags, uint cb);

        [ DllImport("kernel32.dll")]
        public static extern IntPtr LocalFree(IntPtr p);
        
        private void getTreeText(IntPtr treehandle)
        {

            if (process.HasExited)
            {
                foreach (var p in Process.GetProcessesByName("Ventrilo"))
                {
                    process = p;
                    procHandle = IntPtr.Zero;
                }
                if (process == null)
                    return;
            }
            //Bitmap bitmap = MakeSnapshot(treehandle, true, Win32API.WindowShowStyle.Restore);
            ventNames = new List<string> { };

            int count = 0;
            //listBox1.BeginUpdate();
            //listBox1.SuspendLayout();
            //listBox1.Items.Clear();
            int hNode = SendMessage(treehandle, tree_api.TVM_GETNEXTITEM, tree_api.TVGN_ROOT, 0);
            int rootnodecount = 0;
            while (hNode != 0)
            {
                NodeData nodedata1 = AllocTest(process, treehandle, (IntPtr)hNode);
                if (rootnodecount > 0)
                {
                    //Color col = bitmap.GetPixel(nodedata1.bounds.Left - 15, nodedata1.bounds.Top + (nodedata1.bounds.Height / 2));
                    string strText = nodedata1.Text;
                    if (nodedata1.Speaking) //(col.G > 100)  
                    {
                        //listBox1.Items.Add("     *" + strText + "*");     
                        ventNames.Add(strText);
                    }
                    //else if (col.R > 100)
                        //listBox1.Items.Add("     " + strText);
                    //else
                        //listBox1.Items.Add("  " + strText);
                }
                else
                {
                    //listBox1.Items.Add(nodedata1.Text);
                }
                count++;
                int hChild = SendMessage(treehandle, tree_api.TVM_GETNEXTITEM, tree_api.TVGN_CHILD, hNode);
                while (hChild != 0)
                {
                    count++;                   
                    NodeData nodedata = AllocTest(process, treehandle, (IntPtr)hChild);
                    //Color col = bitmap.GetPixel(nodedata.bounds.Left - 15, nodedata.bounds.Top + (nodedata.bounds.Height / 2));
                    
                    string strText = nodedata.Text;//GetTreeItem_Local(hChild, treehandle);
                    if (nodedata.Speaking)// (col.R < 100)
                    { 
                        //listBox1.Items.Add("        *" + strText + "*");
                        ventNames.Add(strText);
                    }
                    //else
                    //    listBox1.Items.Add("        " + strText);
                    hChild = SendMessage(treehandle, tree_api.TVM_GETNEXTITEM, tree_api.TVGN_NEXT, hChild);
                }
                hNode = SendMessage(treehandle, tree_api.TVM_GETNEXTITEM, tree_api.TVGN_NEXT, hNode);
                rootnodecount++;
            }
            //listBox1.ResumeLayout();
            //listBox1.EndUpdate();
            //if (bitmap != null) bitmap.Dispose();

            //send to Arx
            if (useArx && ArxConnected)
            {
                if (ventNames.Count > 0)
                {
                    string names = "";
                    foreach (var name in ventNames)
                    {

                        if (names != "")
                            names += "," + name;
                        else
                            names = name;

                    }
                    //set arx text
                    LogitechArx.LogiArxSetTagContentById("speaking", names);
                    //lblSpeaking.Text = names;
                    lblSpeaking.Invoke((MethodInvoker)(() => lblSpeaking.Text = names));
                }
                else
                {
                    //clear arx status
                    LogitechArx.LogiArxSetTagContentById("speaking", " ");
                    lblSpeaking.Invoke((MethodInvoker)(() => lblSpeaking.Text = ""));
                }
            }
            else
            if (ventNames.Count > 0)
            {
                string names = "";
                foreach (var name in ventNames)
                {

                    if (names != "")
                        names += "," + name;
                    else
                        names = name;

                }
                //lblSpeaking.Text = names;
                lblSpeaking.Invoke((MethodInvoker)(() => lblSpeaking.Text = names));
            }
            else
            {
                lblSpeaking.Invoke((MethodInvoker)(() => lblSpeaking.Text = ""));
            }

            //scroll on keyboard
            if (LEDMode == LEDDisplay.Scroll)
            {
                foreach (var name in ventNames)
                {
                    if (scrollText != name)
                    {
                        Bitmap b = new Bitmap(21, 6);
                        Graphics g = Graphics.FromImage(b);
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

                        g.DrawString(name, new Font("Smallest Pixel-7", 7, FontStyle.Regular), new SolidBrush(Color.Red), new Point(-2, -2)); //create new graphic
                        maxScroll = (int)g.MeasureString(name, new Font("Smallest Pixel-7", 7, FontStyle.Regular)).Width; //measure string for max scroll width of marquee
                        g.Dispose();
                        //g.DrawString("the quick brown fox jumped over the lazy dog", new Font("Smallest Pixel-7", 7, FontStyle.Regular), new SolidBrush(Color.Red), new Point(-2, -2));

                        byte[] tempBytes;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(name);
                        string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

                        scrollText = asciiStr;
                        scrollPosition = initialScrollPause;

                        LogitechGSDK.LogiLedSetLighting(0, 0, 0);

                        spellText = asciiStr;
                        spellPosition = 1;
                        if (bwSpell != null)
                        {
                            bwSpell.CancelAsync();
                        }
                        bwSpell = new BackgroundWorker();
                        bwSpell.WorkerSupportsCancellation = true;
                        bwSpell.DoWork -= bwSpell_DoWork;
                        bwSpell.DoWork += bwSpell_DoWorkSticky;
                        bwSpell.RunWorkerAsync();
                    }
                    return;
                }
            }           
          
            //spell on keyboard
            if (LEDMode == LEDDisplay.Spell)
            {
                foreach (var name in ventNames)
                {
                    if (spellText != name)
                    {
                        byte[] tempBytes;
                        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(name);
                        string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);

                        spellText = asciiStr;
                        spellPosition = 1;
                        int keyCode = name[0];

                        LogitechGSDK.LogiLedSetLighting(0, 0, 0);

                        if (bwSpell != null)
                        {
                            bwSpell.CancelAsync();
                        }
                        bwSpell = new BackgroundWorker();
                        bwSpell.WorkerSupportsCancellation = true;
                        bwSpell.DoWork -= bwSpell_DoWorkSticky;
                        bwSpell.DoWork += bwSpell_DoWork;
                        bwSpell.RunWorkerAsync();
                    }
                    return;
                }
            }
                        
            if (bwSpell != null && bwSpell.IsBusy)
            {
                bwSpell.CancelAsync();
                LogitechGSDK.LogiLedRestoreLighting();
            }
            scrollText = "";
            spellText = "";
        }

        private void bwSpell_DoWorkSticky(object sender, DoWorkEventArgs e)
        {
            do
            {
                if (bwSpell.CancellationPending) return;
                LogitechGSDK.LogiLedSetLighting(0, 0, 0);                
                float step = 100.0f / spellText.Length;
                for (int pos = 0; pos < spellText.Length; pos++)
                {
                    if (bwSpell.CancellationPending) return;
                    if (spellText == "") return;
                    string keychar = spellText[pos].ToString().ToUpper();
                    if (keychar == " ") keychar = "SPACE";
                    if (keychar == "-") keychar = "MINUS";
                    int keyCode = spellText[pos];
                    try
                    {
                        KeyCodes.KeyCode code = (KeyCodes.KeyCode)Enum.Parse(typeof(KeyCodes.KeyCode), keychar);
                        LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)code, (int)(step * pos), 100 - (int)(step * pos), 0);
                        System.Threading.Thread.Sleep(stickyDelay);
                    }
                    catch { }
                    int cc = (int)(100 - (step * pos));
                   
                }
            }
            while (stickyRepeat);
        }

        void bwSpell_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (bwSpell.CancellationPending) return;
                if (spellText == "") return;
                Font fnt = new Font("Verdana", 12);
                //fade previous keys
                int pos = 0;
                spellPosition += 1;
                if (spellPosition > 470 + spellText.Length * 100) spellPosition = 1; //use 70 here to add a 20 cycle delay before spelling restarts
                int prevkeycount = Math.Min(spellPosition / 100, spellText.Length - 1);
                for (int prevkey = 0; prevkey <= prevkeycount; prevkey++)
                {
                    if (bwSpell.CancellationPending) return;
                    if (spellText == "") return;
                   
                        string keychar = spellText[prevkey].ToString().ToUpper();
                        int keyCode = spellText[prevkey];
                        int c = (450 - spellPosition + (prevkey * 100)); //use 150 here to add a 50 cycle delay before colour starts fading
                        if (c > -1)
                        {
                            try
                            {
                                int p = Math.Min(c, 100);
                                KeyCodes.KeyCode code = (KeyCodes.KeyCode)Enum.Parse(typeof(KeyCodes.KeyCode), keychar);
                                if (prevkey == 0)
                                    LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)code, p, 0, 0);
                                else
                                    LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)code, 0, p, 0);
                            }
                            catch { }
                            pos++;
                        }                    
                }
                System.Threading.Thread.Sleep(2);
            }
        }
               

        private static string getHTML(string name)
        {
            string html = @"<html>
                                            <head>
                                                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0, maximum-scale=1, target-densityDpi=device-dpi, user-scalable=no"" />
                                                <style>
                                                    body
                                                    {
                                                        background-image:url('');
                                                        background-color: black;
                                                        background-repeat:no-repeat;
                                                        background-attachment:fixed;
                                                        background-position:center; 
                                                    }
                                                    
                                                    body {width: 100%; height: 100%;}

                                                    div {
                                                        position:absolute; height:100%; width:100%;
                                                        display: table;
                                                    }
                                                    h1 {
                                                        display: table-cell;
                                                        text-align:center;
                                                        font-size:800%;
                                                        color:rgb(255,255,255);
                                                    }
                                                    h2 {
                                                        text-align:center;
                                                        font-size:500%;
                                                        color:rgb(107,149,255);
                                                    }
                
                                                </style>
                                            </head>
                                            <body>    
                                                <h2 id=""header"">Ventrilo</h2>
                                                <div>                                                    
                                                    <h1 id =""speaking"">" + name + @"</h1>     
                                                </div>  
                                            </body>
                                        </html>";
            return html;
        }
              
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        private static Bitmap MakeSnapshot(IntPtr AppWndHandle,
            bool IsClientWnd, Win32API.WindowShowStyle nCmdShow)
        {
            if (AppWndHandle == IntPtr.Zero || !Win32API.IsWindow(AppWndHandle) ||
                        !Win32API.IsWindowVisible(AppWndHandle))
                return null;
            if (Win32API.IsIconic(AppWndHandle))
                Win32API.ShowWindow(AppWndHandle, nCmdShow);//show it
            //if (!Win32API.SetForegroundWindow(AppWndHandle))
            //    return null;//can't bring it to front
            System.Threading.Thread.Sleep(10);//give it some time to redraw
            RECT appRect;
            bool res = IsClientWnd ? Win32API.GetClientRect
                (AppWndHandle, out appRect) : Win32API.GetWindowRect
                (AppWndHandle, out appRect);
            if (!res || appRect.Height == 0 || appRect.Width == 0)
            {
                return null;//some hidden window
            }
            // calculate the app rectangle
            if (IsClientWnd)
            {
                Point lt = new Point(appRect.Left, appRect.Top);
                Point rb = new Point(appRect.Right, appRect.Bottom);
                Win32API.ClientToScreen(AppWndHandle, ref lt);
                Win32API.ClientToScreen(AppWndHandle, ref rb);
                appRect.Left = lt.X;
                appRect.Top = lt.Y;
                appRect.Right = rb.X;
                appRect.Bottom = rb.Y;
            }
            //Intersect with the Desktop rectangle and get what's visible
            IntPtr DesktopHandle = Win32API.GetDesktopWindow();
            RECT desktopRect;
            Win32API.GetWindowRect(DesktopHandle, out desktopRect);
            RECT visibleRect;
            if (!Win32API.IntersectRect
                (out visibleRect, ref desktopRect, ref appRect))
            {
                visibleRect = appRect;
            }
            if (Win32API.IsRectEmpty(ref visibleRect))
                return null;

            int Width = visibleRect.Width;
            int Height = visibleRect.Height;
            IntPtr hdcTo = IntPtr.Zero;
            IntPtr hdcFrom = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            try
            {
                Bitmap clsRet = null;

                // get device context of the window...
                hdcFrom = IsClientWnd ? Win32API.GetDC(AppWndHandle) :
                        Win32API.GetWindowDC(AppWndHandle);

                // create dc that we can draw to...
                hdcTo = Win32API.CreateCompatibleDC(hdcFrom);
                hBitmap = Win32API.CreateCompatibleBitmap(hdcFrom, Width, Height);

                //  validate
                if (hBitmap != IntPtr.Zero)
                {
                    // adjust and copy
                    int x = appRect.Left < 0 ? -appRect.Left : 0;
                    int y = appRect.Top < 0 ? -appRect.Top : 0;
                    IntPtr hLocalBitmap = Win32API.SelectObject(hdcTo, hBitmap);
                    Win32API.BitBlt(hdcTo, 0, 0, Width, Height,
                        hdcFrom, x, y, Win32API.SRCCOPY);
                    Win32API.SelectObject(hdcTo, hLocalBitmap);
                    //  create bitmap for window image...
                    clsRet = System.Drawing.Image.FromHbitmap(hBitmap);
                }
                return clsRet;
            }
            finally
            {
                //  release the unmanaged resources
                if (hdcFrom != IntPtr.Zero)
                    Win32API.ReleaseDC(AppWndHandle, hdcFrom);
                if (hdcTo != IntPtr.Zero)
                    Win32API.DeleteDC(hdcTo);
                if (hBitmap != IntPtr.Zero)
                    Win32API.DeleteObject(hBitmap);
            }
        }

        private struct WinText
        {
            public IntPtr hWnd;
            public string Text;
        }

        const int WM_GETTEXT = 0x0D;
        const int WM_GETTEXTLENGTH = 0x0E;


        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

                [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int Param, System.Text.StringBuilder text);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            IntPtr lpBuffer,
            int nSize,
            out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            IntPtr lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        // privileges
        const int PROCESS_CREATE_THREAD = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_READ = 0x0010;

        // used for memory allocation
        const uint MEM_COMMIT = 0x00001000;
        const int MEM_DECOMMIT = 0x4000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;

        const int TVM_GETITEMRECT = (0x1100 + 4);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, ref RECT lParam);

        ///<summary>Retries the tree node information.</summary>
        ///<param name="hwndItem">Handle to a tree node item.</param>
        ///<param name="hwndTreeView">Handle to a tree view control.</param>
        ///<param name="process">Process hosting the tree view control.</param>

        private static IntPtr procHandle = IntPtr.Zero;
        public static bool randomColours = false;
        public static bool StartMinimized;
        public static bool keysaveBreathe;
        
        private static NodeData AllocTest(Process process, IntPtr hwndTreeView, IntPtr hwndItem)
        {
            // code based on article posted here: http://www.codingvision.net/miscellaneous/c-inject-a-dll-into-a-process-w--createremotethread
            // handle of the process with the required privileges
            if (procHandle == IntPtr.Zero)
                procHandle = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, process.Id);

            // Write TVITEM to memory
            // Invoke TVM_GETITEM
            // Read TVITEM from memory

            var item = new WhoIsSpeaking.tree_api.TVITEMEX();
            item.hItem = hwndItem;
            item.mask = (int)(WhoIsSpeaking.tree_api.TVIF.TVIF_IMAGE | WhoIsSpeaking.tree_api.TVIF.TVIF_STATE | WhoIsSpeaking.tree_api.TVIF.TVIF_HANDLE | WhoIsSpeaking.tree_api.TVIF.TVIF_CHILDREN | WhoIsSpeaking.tree_api.TVIF.TVIF_TEXT);
            item.stateMask = 0xF000;
            item.cchTextMax = 1024;
            item.pszText = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)item.cchTextMax, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE); // node text pointer

            byte[] data = getBytes(item);

            uint dwSize = (uint)data.Length;
            IntPtr allocMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, dwSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE); // TVITEM pointer

            uint nSize = dwSize;
            UIntPtr bytesWritten;
            bool successWrite = WriteProcessMemory(procHandle, allocMemAddress, data, nSize, out bytesWritten);

            var sm = SendMessage(hwndTreeView, (int)WhoIsSpeaking.tree_api.TVM.TVM_GETITEM, IntPtr.Zero, allocMemAddress);

            UIntPtr bytesRead;
            bool successRead = ReadProcessMemory(procHandle, allocMemAddress, data, nSize, out bytesRead);

            UIntPtr bytesReadText;
            byte[] nodeText = new byte[item.cchTextMax];
            bool successReadText = ReadProcessMemory(procHandle, item.pszText, nodeText, (uint)item.cchTextMax, out bytesReadText);

            RECT rc;
            Rectangle r = new Rectangle(0,0,0,0);
            unsafe
            {
                *(IntPtr*)&rc = hwndItem;
                byte[] rectdata = getBytes(rc);
                uint rSize = (uint)rectdata.Length;
                IntPtr allocRectMemAddress = VirtualAllocEx(procHandle, IntPtr.Zero, rSize, MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE); // TVITEM pointer

                uint nRSize = rSize;
                UIntPtr bytesWrittenR;
                bool successWriteR = WriteProcessMemory(procHandle, allocRectMemAddress, rectdata, nRSize, out bytesWrittenR);

                SendMessage(hwndTreeView, TVM_GETITEMRECT, new IntPtr(1), allocRectMemAddress);

                UIntPtr bytesReadR;
                bool successReadR = ReadProcessMemory(procHandle, allocRectMemAddress, rectdata, nRSize, out bytesReadR);

                int left = BitConverter.ToInt16(rectdata, 0);
                int top = BitConverter.ToInt16(rectdata, 4);
                int right = BitConverter.ToInt16(rectdata, 8);
                int bottom = BitConverter.ToInt16(rectdata, 12);

                r = new Rectangle(left, top, right - left, bottom - top);
            }


            bool success1 = VirtualFreeEx(procHandle, allocMemAddress, dwSize, MEM_DECOMMIT);
            bool success2 = VirtualFreeEx(procHandle, item.pszText, (uint)item.cchTextMax, MEM_DECOMMIT);

            var item2 = fromBytes<WhoIsSpeaking.tree_api.TVITEMEX>(data);

         
            String name = Encoding.Unicode.GetString(nodeText);
    
            IntPtr buffer = VirtualAllocEx(procHandle, IntPtr.Zero, 4096, MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
                      
            //Debug.WriteLine(name.Replace("\0", string.Empty) + " "  + r.Left.ToString() + "," +  r.Top.ToString());
            int x = name.IndexOf('\0');
            if (x >= 0)
                name = name.Substring(0, x);

            //if (name.Contains("Jargahl"))
                //Debug.WriteLine(item2.iImage.ToString() + ":" + item2.state.ToString());

            NodeData node = new NodeData();
            node.Text = name;
            node.HasChildren = (item2.cChildren == 1);
            node.bounds = r;
            if (item2.iImage == 3) node.Speaking = true;

            return node;
        }

        [DllImport("kernel32 ", CharSet = CharSet.Unicode)]
        public static extern int CopyMemory(RECT Destination, IntPtr Source, int Length);
              
        public class NodeData
        {
            public bool Speaking;
            public String Text { get; set; }
            public bool HasChildren { get; set; }
            public Rectangle bounds { get; set;}
        }

        private static byte[] getBytes(Object item)
        {
            int size = Marshal.SizeOf(item);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(item, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        private static T fromBytes<T>(byte[] arr)
        {
            T item = default(T);
            int size = Marshal.SizeOf(item);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, ptr, size);
            item = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return item;
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public static byte[] getLEDGridFromBitmap(Bitmap bitmap)
        {
            try
            {
                BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                bitmap.UnlockBits(bmpdata);
                return bytedata;
            }
            catch
            { return null; }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogitechGSDK.LogiLedRestoreLighting();
            notifyIcon1.Visible = false;
            notifyIcon1.Icon = null;
            notifyIcon1.Dispose();
            notifyIcon1 = null;
            //LogitechGSDK.LogiLedShutdown(); //this seems to mess up some keys...
        }

        private void chkUseArx_CheckedChanged(object sender, EventArgs e)
        {
            useArx = chkUseArx.Checked;
            if (!useArx)
            {
                
                LogitechArx.LogiArxShutdown();
                lblArxStatus.Text = "Diconnected";
                lblArxStatus.ForeColor = Color.DarkRed;
                if (timer1 != null) timer1.Enabled = false;
            }
            else
            {
                InitArx();
               
            }
        }

        private void radioOFF_CheckedChanged(object sender, EventArgs e)
        {
            if (bwSpell != null) bwSpell.CancelAsync();
            if (radioOFF.Checked)
                LEDMode = LEDDisplay.Off;
        }

        private void radioScroll_CheckedChanged(object sender, EventArgs e)
        {
            if (bwSpell != null) bwSpell.CancelAsync();
            if (radioScroll.Checked)
                LEDMode = LEDDisplay.Scroll;
        }

        private void radioSpell_CheckedChanged(object sender, EventArgs e)
        {
            if (bwSpell != null) bwSpell.CancelAsync();
            if (radioSpell.Checked)
                LEDMode = LEDDisplay.Spell;
        }
        
        private void chkLEDAnimation_CheckedChanged(object sender, EventArgs e)
        {
            useAnimation = chkLEDAnimation.Checked;

            //if (useAnimation)
            //{
            //    KeyboardHook._hookID = hook.SetHook(hook._proc);
            //}
            //else
            //{
            //    KeyboardHook.UnhookWindowsHookEx(KeyboardHook._hookID);
            //}
        }

        private void chkLogitechColours_CheckedChanged(object sender, EventArgs e)
        {
            useLogitechColours = chkLogitechColours.Checked;
        }

        private void picStartColour_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = m_startColour;
            DialogResult r = c.ShowDialog();
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                picStartColour.BackColor = c.Color;
                m_startColour = c.Color;
            }
        }

        private void picEndColour_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = m_endColour;
            DialogResult r = c.ShowDialog();
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                picEndColour.BackColor = c.Color;
                m_endColour = c.Color;
            }
        }

        private void numGradientSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_gradientspeed = (int)numGradientSpeed.Value;
        }

        private void numFadeSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_fadespeed = (int)numFadeSpeed.Value;
        }

        private void chkWave_CheckedChanged(object sender, EventArgs e)
        {
            m_Wave = chkWave.Checked;
        }

        private void numAnimationSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_AnimationSpeed = (int)numAnimationSpeed.Value;
        }


        private void numDistanceFalloff_ValueChanged(object sender, EventArgs e)
        {
            m_distanceFalloff = (double)numDistanceFalloff.Value;
        }

        private void numWaveSpeed_ValueChanged(object sender, EventArgs e)
        {
            m_WaveSpeed = (double)numWaveSpeed.Value;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string p = InputBox.ShowInputBox("Enter name of profile");

            if (p != null)
            {
                string filename = Application.StartupPath + "\\Profiles\\" + p + ".prf";
                if (File.Exists(filename))
                {
                    DialogResult check = MessageBox.Show("Overwrite existing profile file?", "Overwrite File?", MessageBoxButtons.YesNo);
                    if (check == System.Windows.Forms.DialogResult.No) return;
                }
                writeSettings(filename);
            }
        }

        private void writeSettings(string p)
        {
            var pFile = new IniFile(p);
            pFile.WriteBoolean("LED", "LEDAnimation", useAnimation);
            pFile.WriteBoolean("LED", "KeepLogitechColours", useLogitechColours);
            pFile.WriteString("LED", "StartColour", ColorTranslator.ToHtml(m_startColour));
            pFile.WriteString("LED", "EndColour", ColorTranslator.ToHtml(m_endColour));
            pFile.WriteBoolean("LED", "RandomColours", randomColours);
            pFile.WriteBoolean("LED", "UseKeysaver", UseKeysaver);
            pFile.WriteInteger("LED", "KeysaverTime", KeySaverTime);
            pFile.WriteBoolean("LED", "KeySaveBreathe", keysaveBreathe);
            pFile.WriteInteger("Animation", "AnimationDelay", m_AnimationSpeed);
            pFile.WriteInteger("Animation", "GradientSpeed", m_gradientspeed);
            pFile.WriteInteger("Animation", "FadeSpeed", m_fadespeed);
            pFile.WriteDouble("Animation", "EffectDistance", m_distanceFalloff);
            pFile.WriteBoolean("Animation", "Wave", m_Wave);
            pFile.WriteDouble("Animation", "WaveFalloff", m_WaveSpeed);
            pFile.WriteBoolean("Ventrilo", "UseArx", useArx);
            pFile.WriteString("Ventrilo", "DisplayMethod", LEDMode.ToString());
            pFile.WriteDouble("KeySave", "NewRippleInterval", m_KeysaveNewRippleInterval);
        }

        private void setAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ProfileDefault = currentProfile;
            Properties.Settings.Default.Save();
        }

        private void loadProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstProfiles.Items.Clear();
            lstProfiles.BringToFront();
            foreach(string file in Directory.GetFiles(Application.StartupPath + "\\Profiles", "*.prf"))
            {
                string trunc = Path.GetFileName(file).Replace(".prf", "");
                lstProfiles.Items.Add(trunc);
            }

            lstProfiles.Visible = true;
            cmdCancelProfileLoad.Visible = true;

        }

        private void cmdCancelProfileLoad_Click(object sender, EventArgs e)
        {
            lstProfiles.Visible = false;
            cmdCancelProfileLoad.Visible = false;
        }

        private void lstProfiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lstProfiles.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                LoadSettings(lstProfiles.Items[index].ToString());
                currentProfile = lstProfiles.Items[index].ToString();
            }
            lstProfiles.Visible = false;
            cmdCancelProfileLoad.Visible = false;
        }

        private void LoadSettings(string p)
        {
            
            if (!File.Exists(Application.StartupPath + "\\Profiles\\" + p + ".prf"))
            {
                MessageBox.Show("Invalid profile file specified, reverting to defaults");
                return;
            }

            var pFile = new IniFile(Application.StartupPath + "\\Profiles\\" + p + ".prf");
            useAnimation = pFile.ReadBoolean("LED", "LEDAnimation");
            useLogitechColours = pFile.ReadBoolean("LED", "KeepLogitechColours");
            m_startColour = ColorTranslator.FromHtml(pFile.ReadString("LED", "StartColour"));
            m_endColour = ColorTranslator.FromHtml(pFile.ReadString("LED", "EndColour"));
            randomColours = pFile.ReadBoolean("LED", "RandomColours");
            m_AnimationSpeed = pFile.ReadInteger("Animation", "AnimationDelay");
            m_gradientspeed = pFile.ReadInteger("Animation", "GradientSpeed" );
            m_fadespeed= pFile.ReadInteger("Animation", "FadeSpeed" );
            m_distanceFalloff = pFile.ReadDouble("Animation", "EffectDistance" );
            m_Wave = pFile.ReadBoolean("Animation", "Wave" );
            try
            {
                m_KeysaveNewRippleInterval = pFile.ReadDouble("KeySave", "m_KeysaveNewRippleInterval");
            }
            catch { }
            m_WaveSpeed  = pFile.ReadDouble("Animation", "WaveFalloff");
            useArx = pFile.ReadBoolean("Ventrilo", "UseArx");
            LEDMode = (LEDDisplay)Enum.Parse(typeof(LEDDisplay), pFile.ReadString("Ventrilo", "DisplayMethod"));
            UseKeysaver = pFile.ReadBoolean("LED", "UseKeysaver");
            KeySaverTime = pFile.ReadInteger("LED", "KeySaverTime");
            keysaveBreathe = pFile.ReadBoolean("LED", "KeySaveBreathe");

            chkBreathe.Checked = keysaveBreathe;
            chkRandomColours.Checked = randomColours;
            chkKeySaver.Checked = UseKeysaver;
            numKeySaverTime.Value = KeySaverTime;
            picStartColour.BackColor = m_startColour;
            picEndColour.BackColor = m_endColour;
            numFadeSpeed.Value = m_fadespeed;
            numGradientSpeed.Value = m_gradientspeed;
            chkWave.Checked = m_Wave;
            numAnimationSpeed.Value = m_AnimationSpeed;
            numWaveSpeed.Value = (decimal)m_WaveSpeed;
            numDistanceFalloff.Value = (decimal)m_distanceFalloff;
            numKeyRippleInterval.Value = (decimal)m_KeysaveNewRippleInterval;
            chkUseArx.Checked = useArx;

            radioSpell.Checked = false;
            radioOFF.Checked = false;
            radioScroll.Checked = false;

            if (LEDMode == LEDDisplay.Off)
                radioOFF.Checked = true;
            if (LEDMode == LEDDisplay.Scroll)
                radioScroll.Checked = true;
            if (LEDMode == LEDDisplay.Spell)
                radioSpell.Checked = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
	        if (this.WindowState == FormWindowState.Minimized)//this code gets fired on every resize
	        {																					   //so we check if the form was minimized
		        Hide();//hides the program on the taskbar
		        notifyIcon1.Visible = true;//shows our tray icon
	        }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();//shows the program on taskbar
            this.WindowState = FormWindowState.Normal;//undoes the minimized state of the form
            notifyIcon1.Visible = false;//hides tray icon again
        }

        private void chkKeySaver_CheckedChanged(object sender, EventArgs e)
        {
            UseKeysaver = chkKeySaver.Checked;
            KeyboardHook.timerKeySaver.Enabled = UseKeysaver;
            if (UseKeysaver)
                KeyboardHook.timerKeySaver.Start();
            else
                KeyboardHook.timerKeySaver.Stop();
        }

        private void numKeySaverTime_ValueChanged(object sender, EventArgs e)
        {
            KeySaverTime = (int)numKeySaverTime.Value;
            KeyboardHook.timerKeySaver.Enabled = UseKeysaver;
            KeyboardHook.timerKeySaver.Interval = (int)numKeySaverTime.Value * 1000;
        }

        private void lstProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkRandomColours_CheckedChanged(object sender, EventArgs e)
        {
            randomColours = chkRandomColours.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            KeyboardHook._hookID = hook.SetHook(hook._proc);
            
            hwmonitor = new HardwareMonitor();
            getTemperatures();
            if (hwmonitor.isElevated)
            {
                Timer timertemp = new Timer();
                timertemp.Interval = 1000;
                timertemp.Tick += timertemp_Tick;
                timertemp.Start();
            }
        }
        BackgroundWorker tempWorker;
        void timertemp_Tick(object sender, EventArgs e)
        {
            tempWorker = new BackgroundWorker();
            tempWorker.DoWork += tempWorker_DoWork;
            tempWorker.RunWorkerAsync();
        }

        void tempWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            getTemperatures();
        }

        private void getTemperatures()
        {
            try
            {
                if (hwmonitor.isElevated)
                {
                    float cputemp = hwmonitor.gettemp();
                    lblCpuTemp.Invoke((MethodInvoker)(() => lblCpuTemp.Text = cputemp.ToString("##") + "°C"));

                    float gputemp = hwmonitor.getGPUtemp();
                    lblGPUTemp.Invoke((MethodInvoker)(() => lblGPUTemp.Text = gputemp.ToString("##") + "°C"));
                }
            }
            catch { }
        }

        private void chkBreathe_CheckedChanged(object sender, EventArgs e)
        {
            keysaveBreathe = chkBreathe.Checked; 
        }

        private void numKeyRippleInterval_ValueChanged(object sender, EventArgs e)
        {
            m_KeysaveNewRippleInterval = (double)numKeyRippleInterval.Value;
        }
    }
}
