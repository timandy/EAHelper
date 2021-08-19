using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace EAHelper
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            this.InitializeComponent();
        }


        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            IntPtr hwndDialog = UnsafeNativeMethods.FindWindow("#32770", "Unsaved Property Changes");
            if (hwndDialog == IntPtr.Zero)
                return;

            IntPtr hwndYes = UnsafeNativeMethods.FindWindowEx(hwndDialog, IntPtr.Zero, "Button", "Yes");
            if (hwndYes == IntPtr.Zero)
                return;
            Util.SendMouseClick(hwndYes, Point.Empty);
        }
    }
}