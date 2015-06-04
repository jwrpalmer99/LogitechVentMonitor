using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WhoIsSpeaking
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form1 form1 = new Form1();
            if (args.Length == 1 && (args[0] == "/minimized" || args[0] == "-minimized" || args[0] == "--minimized"))
            {
                Form1.StartMinimized = true;
                //form1.ShowInTaskbar = false;
                //form1.WindowState = FormWindowState.Minimized;
            }
            Application.Run(new Form1());
        }
    }
}
