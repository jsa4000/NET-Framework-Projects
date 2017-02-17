using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KeyProc
{
    static class Program
    {
        private static Main main = null;
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.EnterThreadModal += new EventHandler(Application_EnterThreadModal);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
            // Creating main class 
            main = new Main();
            main.Start();
            //
            Application.Run();
        }

        static void Application_EnterThreadModal(object sender, EventArgs e)
        {
            if (main != null)
                main.Stop();
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            if (main != null)
                main.Stop();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (main != null)
                main.Stop();
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (main != null)
                main.Stop();
        }
    }
}
