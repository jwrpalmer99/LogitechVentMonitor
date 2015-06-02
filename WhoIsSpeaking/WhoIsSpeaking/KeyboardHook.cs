using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ColorManagment;
using System.Drawing.Imaging;

namespace WhoIsSpeaking
{
    public class KeyboardHook : Control
    {
        private static Bitmap keyheatmap = new Bitmap(21,6);

        public static Timer timerKeySaver = new Timer();
        private static BackgroundWorker bw_Keysave;

        private bool bIsHooked = false;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x104;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYUP = 0x105;
        public LowLevelKeyboardProc _proc = HookCallback;
        public static IntPtr _hookID = IntPtr.Zero;
        private static List<centroid> centroids = new List<centroid> { };
        private static Timer WaveTimer;
        private static BackgroundWorker bw;
        private struct centroid
        {
            public Point point;
            public int countup;
        }
        private static Bitmap bmp;

        public IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            timerKeySaver.Tick += timerKeySaver_Tick;
            timerKeySaver.Enabled = true;
            if (Application.OpenForms.Count > 0)
            {
                timerKeySaver.Interval = ((Form1)(Application.OpenForms[0])).KeySaverTime;
                if (((Form1)(Application.OpenForms[0])).UseKeysaver)
                {
                    timerKeySaver.Enabled = true;
                    timerKeySaver.Start();
                }
            }

            bw_Keysave = new BackgroundWorker();
            bw_Keysave.DoWork += bw_Keysave_DoWork;
            bw_Keysave.WorkerSupportsCancellation = true;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }            
        }

        void timerKeySaver_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("timer ticked @ " + DateTime.Now.ToLongTimeString());
            bw_Keysave.RunWorkerAsync();
            timerKeySaver.Stop();
        }

        public struct keysavePoint
        {
            public Point point;
            public int counter;
        }
        void bw_Keysave_DoWork(object sender, DoWorkEventArgs e)
        {
            LogitechGSDK.LogiLedSetLighting(0, 0, 0);
            DateTime inittime = DateTime.Now;
            int counter = 0;
            List<keysavePoint> points = new List<keysavePoint> { };
            Random rnd = new Random();
            while (true)
            {
                if (bw_Keysave.CancellationPending) return;
                TimeSpan elapsed = DateTime.Now - inittime;
                if (elapsed.TotalMilliseconds > 500) //change this namber to change speed new stars appear
                {
                    //for (int c = 0; c < 2; c++)
                    //{ 
                        int newx = rnd.Next(0,20);
                        int newy = rnd.Next(0, 5);
                        keysavePoint kp = new keysavePoint();
                        kp.point = new Point(newx, newy);
                        kp.counter = 0;
                        points.Add(kp);
                    //}
                    //Console.Write(".");
                    inittime = DateTime.Now;
                }
                if (counter > 4) //change this number to change speed stars change
                {                    
                    counter = 0;

                    System.Drawing.Color c1 = Form1.m_startColour;
                    System.Drawing.Color c2 = Form1.m_endColour;

                    int fadespeed = Form1.m_fadespeed;
                    int gradientspeed = Form1.m_gradientspeed;

                    ColorManagment.ColorConverter Converter = new ColorManagment.ColorConverter();    //create a new instance of a ColorConverter
                    ColorRGB rgb1 = new ColorRGB(RGBSpaceName.sRGB, c1.R, c1.G, c1.B);  //create an RGB color
                    ColorLab lab1 = Converter.ToLab(rgb1);
                    ColorRGB rgb2 = new ColorRGB(RGBSpaceName.sRGB, c2.R, c2.G, c2.B);  //create an RGB color
                    ColorLab lab2 = Converter.ToLab(rgb2);

                    bmp = new Bitmap(21, 6);
                    LockBitmap lockBitmap = new LockBitmap(bmp);
                    lockBitmap.LockBits();



                    for (int x = 0; x < 21; x++)
                        for (int y = 0; y < 6; y++)
                        {
                            distances[x, y] = double.MaxValue;
                            times[x, y] = int.MaxValue;
                        }

                    for (int i = 0; i < points.Count; i++)
                    {
                        keysavePoint c = points[i];

                        for (int x = 0; x < 21; x++)
                            for (int y = 0; y < 6; y++)
                            {
                                double distance = Math.Sqrt(((x - c.point.X) * (x - c.point.X) + (y - c.point.Y) * (y - c.point.Y)));
                                distance = Math.Abs(distance) / (Form1.m_distanceFalloff / 2);
                                if (Form1.m_Wave)
                                {
                                    distance -= c.counter;
                                    distance = Math.Abs(distance);
                                }
                                if (distance < Math.Abs(distances[x, y]))
                                {
                                    distances[x, y] = distance;
                                }
                                if (c.counter < times[x, y])
                                    times[x, y] = c.counter;
                            }
                        c.counter++;
                        points[i] = c;
                    }

                    for (int x = 0; x < 21; x++)
                        for (int y = 0; y < 6; y++)
                        {
                            double distance = distances[x, y];
                            System.Drawing.Color colour = System.Drawing.Color.White;
                            if (Form1.m_Wave == true)
                                colour = getColour(lab1, lab2, distance + times[x, y] + Math.Pow(distance, Form1.m_WaveSpeed), gradientspeed, fadespeed);
                            else
                                colour = getColour(lab1, lab2, distance + times[x, y], gradientspeed, fadespeed);
                            lockBitmap.SetPixel(x, y, colour);
                        }


                    //for (int ki = 0; ki < points.Count - 1; ki++)
                    //{
                    //    double distance = points[ki].counter;
                    //    keysavePoint kchanged = new keysavePoint();
                    //    kchanged.point = points[ki].point;
                    //    kchanged.counter = (int)(distance + 1);
                    //    points[ki] = kchanged;
                    //    int x = points[ki].point.X;
                    //    int y = points[ki].point.Y;
                    //    System.Drawing.Color colour = System.Drawing.Color.White;

                    //    colour = getColour(lab1, lab2, distance, gradientspeed, fadespeed);
                    //    lockBitmap.SetPixel(x, y, colour);
                    //}

                    for (int k = points.Count - 1; k >= 0; k--)
                    {
                        if (points[k].counter > 20) points.RemoveAt(k);
                    }
                    lockBitmap.UnlockBits();

                    byte[] b = Form1.getLEDGridFromBitmap(bmp);
                    //((Form1)Application.OpenForms[0]).pic1.Image = bmp;
                    //bmp.Save(@"C:\temp\heatmap.png");
                    LogitechGSDK.LogiLedSetLightingFromBitmap(b);
                }
                counter++;
                System.Threading.Thread.Sleep(10);
            }
        }
        
        public delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        delegate void AddKeyDelegate(int vkCode);

        private static readonly List<int> KeysDown = new List<int>();

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (timerKeySaver.Enabled || bw_Keysave.IsBusy) //reset timer for keysaver
            {
                bw_Keysave.CancelAsync();
                timerKeySaver.Stop();
                timerKeySaver.Start();
            }
            int vkCode = Marshal.ReadInt32(lParam);

            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                if (!KeysDown.Contains(vkCode))
                {
                    if (WaveTimer == null)
                    {
                        WaveTimer = new Timer();
                        WaveTimer.Interval = 10;
                        WaveTimer.Tick += timer1_Tick;
                        WaveTimer.Start();
                    }
                    if (bmp == null)
                    {
                        bmp = new Bitmap(21, 6);
                    }
                    //do heatmap
                    AddKeyDelegate addkey = AddKeyPress;
                    addkey.BeginInvoke(vkCode, null, null);
                }

                KeysDown.Add(vkCode);
                if ((Keys)vkCode == Keys.Escape && ModifierKeys.HasFlag(Keys.Control))
                {
                    return (IntPtr)1;//swallow ctrl-escape
                }
                 
            }

            if (wParam == (IntPtr)WM_KEYUP)
            {
                KeysDown.RemoveAll(k => k == vkCode);
            }

            return (IntPtr)0;// CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static void AddKeyPress(int vkCode)
        {
           

            KeysConverter kc = new KeysConverter();
            string keyChar = kc.ConvertToString(vkCode).ToUpper();

            string keyOut = "";
            switch (keyChar)
            {
                case "1":
                    keyOut = "ONE";
                    break;
                case "2":
                    keyOut = "TWO";
                    break;
                case "3":
                    keyOut = "THREE";
                    break;
                case "4":
                    keyOut = "FOUR";
                    break;
                case "5":
                    keyOut = "FIVE";
                    break;
                case "6":
                    keyOut = "SIX";
                    break;
                case "7":
                    keyOut = "SEVEN";
                    break;
                case "8":
                    keyOut = "EIGHT";
                    break;
                case "9":
                    keyOut = "NINE";
                    break;
                case "0":
                    keyOut = "ZERO";
                    break;
                default:
                    keyOut = keyChar;
                    break;
            }

            try
            {
                //KeyCodes.ScanCode code = (KeyCodes.ScanCode)Enum.Parse(typeof(KeyCodes.ScanCode), keyOut);
                var query = from KeyCodes.keyPosition k in KeyCodes.keypositions
                            where k.keyname == keyOut //(int)code
                            select k;
                
                centroid c = new centroid();
                c.point = new Point((int)query.First().x, (int)query.First().y);
                c.countup = 1;

                bool found = false;
                for (int i = 0; i < centroids.Count; i++)
                {
                    if (centroids[i].point == c.point)
                    {
                        centroids.RemoveAt(i);
                        break;
                    }
                }
                centroids.Add(c);
            }
            catch
            { }
        }

        static void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                WaveTimer.Interval = Form1.m_AnimationSpeed;
                DoAnimation();
            }
            catch { }
        }

        private static double[,] distances = new double[21, 6];
        private static int[,] times = new int[21, 6];
        private static void DoAnimation()
        {
            //return;
            //WaveTimer.Enabled = false;
            
                if (centroids.Count == 0 || ((Form1)(Application.OpenForms[0])).spellText != "")
                {
                    //LogitechGSDK.LogiLedSetLighting(0, 0, 0);
                    //WaveTimer.Enabled = true;
                    if (((Form1)(Application.OpenForms[0])).spellText == "" && ((Form1)(Application.OpenForms[0])).useLogitechColours && !bw_Keysave.IsBusy) LogitechGSDK.LogiLedRestoreLighting();
                    return;
                }
                else
                {                    
                    //calculate the 2 end point colours into LAB space
                    System.Drawing.Color c1 = Form1.m_startColour;
                    System.Drawing.Color c2 = Form1.m_endColour;

                    int fadespeed = Form1.m_fadespeed;
                    int gradientspeed = Form1.m_gradientspeed;

                    ColorManagment.ColorConverter Converter = new ColorManagment.ColorConverter();    //create a new instance of a ColorConverter
                    ColorRGB rgb1 = new ColorRGB(RGBSpaceName.sRGB, c1.R , c1.G, c1.B );  //create an RGB color
                    ColorLab lab1 = Converter.ToLab(rgb1);
                    ColorRGB rgb2 = new ColorRGB(RGBSpaceName.sRGB, c2.R , c2.G, c2.B );  //create an RGB color
                    ColorLab lab2 = Converter.ToLab(rgb2); 
                    
                    //bmp = new Bitmap(21, 6);
                    LockBitmap lockBitmap = new LockBitmap(bmp);
                    lockBitmap.LockBits();


                    for (int x = 0; x < 21; x++)
                        for (int y = 0; y < 6; y++)
                        {
                            distances[x, y] = double.MaxValue;
                            times[x, y] = int.MaxValue;
                        }

                    for (int i = 0; i < centroids.Count; i++)
                    {
                        centroid c = centroids[i];

                        for (int x = 0; x < 21; x++)
                            for (int y = 0; y < 6; y++)
                            {
                                double distance = Math.Sqrt(((x - c.point.X) * (x - c.point.X) + (y - c.point.Y) * (y - c.point.Y)));
                                distance = Math.Abs(distance) / (Form1.m_distanceFalloff / 2);
                                if (Form1.m_Wave)
                                {
                                    distance -= c.countup;
                                    distance = Math.Abs(distance);
                                }
                                if (distance < Math.Abs(distances[x, y]))
                                {                                    
                                    distances[x, y] = distance;
                                }
                                if (c.countup < times[x,y])
                                    times[x, y] = c.countup;
                            }
                        c.countup++;
                        centroids[i] = c;
                    }

                    for (int x = 0; x < 21; x++)
                        for (int y = 0; y < 6; y++)
                        {
                            double distance = distances[x, y];
                            System.Drawing.Color colour = System.Drawing.Color.White;
                            if (Form1.m_Wave == true)
                                colour = getColour(lab1, lab2, distance + times[x,y] + Math.Pow(distance, Form1.m_WaveSpeed), gradientspeed, fadespeed);
                            else
                                colour = getColour(lab1, lab2, distance + times[x,y], gradientspeed, fadespeed);
                            lockBitmap.SetPixel(x, y, colour);
                        }

                    lockBitmap.UnlockBits();

                    byte[] b = Form1.getLEDGridFromBitmap(bmp);
                    //((Form1)Application.OpenForms[0]).pic1.Image = bmp;
                    //bmp.Save(@"C:\temp\heatmap.png");
                    LogitechGSDK.LogiLedSetLightingFromBitmap(b);

                    for (int i = centroids.Count - 1; i >= 0; i--)
                    {
                        try
                        {                            
                            centroid c = centroids[i];
                            centroids[i] = c;
                            if (centroids[i].countup > 255 / fadespeed) centroids.RemoveAt(i);
                        }
                        catch { }
                    }
                   // System.Threading.Thread.Sleep(Form1.m_AnimationSpeed);
                }
                //WaveTimer.Enabled = true;            
        }

        private static System.Drawing.Color getColour(ColorLab c1, ColorLab c2, double countup, int speed = 7, int fadespeed = 15)
        {
            speed = 21 - speed;
            double alpha_seed = countup * fadespeed;
            if (alpha_seed > 255) alpha_seed = 255;
            int alpha = (int)Math.Max(0, 255 - alpha_seed);

            double step = 1.0 / speed;
            double ratio = (countup - 1) * step;
            if (ratio > 1) ratio = 1;
            if (ratio < 0) ratio = 0;
            double ratio2 = 1 - ratio;
            
            double L = ((c1.L * ratio2) + (c2.L * ratio));
            double a = ((c1.a * ratio2) + (c2.a * ratio));
            double b = ((c1.b * ratio2) + (c2.b * ratio));

            //Console.WriteLine("Lab = " + L.ToString() + "," + a.ToString() + "," + b.ToString());
            ColorManagment.ColorConverter Converter = new ColorManagment.ColorConverter();    //create a new instance of a ColorConverter
            ColorLab lab = new ColorLab(L,a,b); //create new Lab color
            ColorRGB rgb = Converter.ToRGB(lab); //convert to rgb
            ColorHSV hsv = Converter.ToHSV(rgb);  //conver to HSL
            //Console.WriteLine("hsl.l = " + hsv.V.ToString());
            hsv.V *= alpha / 255.0; //darken
            rgb = Converter.ToRGB(hsv); //convert to rgb
            //Console.WriteLine("RGB + " + rgb.R.ToString() + "," + rgb.G.ToString() + "," + rgb.B.ToString());
            return System.Drawing.Color.FromArgb(255, (int)(rgb.R * 255), (int)(rgb.G * 255), (int)(rgb.B * 255));
        }
                     

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Point centroid = (Point)e.Argument;
            
            for (int time = 0 ; time < 20; time++)
            {
                //BitmapData bdata = keyheatmap.LockBits(new Rectangle(0, 0, keyheatmap.Width, keyheatmap.Height), ImageLockMode.WriteOnly, keyheatmap.PixelFormat);
                
                for (int timedelta = 0; timedelta < time; timedelta++)
                {
                    if (centroid.X - timedelta > 0)
                        keyheatmap.SetPixel(centroid.X - timedelta, centroid.Y, System.Drawing.Color.FromArgb(Math.Max(0,255 - (time * 30)), 0, 0));
                    if (centroid.X + timedelta < 21)
                        keyheatmap.SetPixel(centroid.X + timedelta, centroid.Y, System.Drawing.Color.FromArgb(Math.Max(0,255 - (time * 30)), 0, 0));
                }
                byte[] b = Form1.getLEDGridFromBitmap(keyheatmap);
                LogitechGSDK.LogiLedSetLightingFromBitmap(b);
                //keyheatmap.UnlockBits(bdata);
                System.Threading.Thread.Sleep(50);
            }
            
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
