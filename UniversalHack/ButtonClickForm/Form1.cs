using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ButtonClickForm
{
    public partial class Form1 : Form
    {
        UNIVERSALHack hack = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            hack = new UNIVERSALHack(@"C:\Users\javier.santos\Desktop\New folder", @"C:\Users\javier.santos\Desktop\New folder\test");
            hack.Open();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (hack != null)
                hack.StartProcess();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (hack != null)
                hack.Close();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (hack != null)
            {
                UNIVERSALHack.SetWindowLong(hack.Handle, UNIVERSALHack.GWL_STYLE, UNIVERSALHack.GetWindowLong(hack.Handle, UNIVERSALHack.GWL_STYLE) & ~UNIVERSALHack.WS_CAPTION);
                UNIVERSALHack.SetParent(hack.Handle, panelApp.Handle);
                UNIVERSALHack.MoveWindow(hack.Handle, 0, 0, panelApp.Width, panelApp.Height, true);

            }
        }
        
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (hack != null)
            {
                if (btnHide.Text == "HIDE")
                {
                    UNIVERSALHack.ShowWindow(hack.Handle, UNIVERSALHack.SW_HIDE);
                    btnHide.Text = "SHOW";
                }
                else
                {
                    UNIVERSALHack.ShowWindow(hack.Handle, UNIVERSALHack.SW_SHOW);
                    btnHide.Text = "HIDE";
                }
            }
        }

    }
}
