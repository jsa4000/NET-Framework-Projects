using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KeyHook;

namespace KeyProc
{
    public class Main
    {

        private KeyHookManager _keyManager;

        public Main()
        {
            _keyManager = new KeyHookManager();
        }
        public void Start()
        {
            if (_keyManager!= null)
                _keyManager.Start();
        }

        public void Stop()
        {
            if (_keyManager != null)
                _keyManager.Stop();
        }

    }
}
