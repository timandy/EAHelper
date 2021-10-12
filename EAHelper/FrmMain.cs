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
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.SetVisibleCore(false);
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.ClickYes();
            this.SetEANoTopMost();
        }

        private void ClickYes()
        {
            IntPtr hwndDialog = UnsafeNativeMethods.FindWindow("#32770", "Unsaved Property Changes");
            if (hwndDialog == IntPtr.Zero)
                return;

            IntPtr hwndYes = UnsafeNativeMethods.FindWindowEx(hwndDialog, IntPtr.Zero, "Button", "Yes");
            if (hwndYes == IntPtr.Zero)
                return;
            Util.SendMouseClick(hwndYes, Point.Empty);
        }

        private void SetEANoTopMost()
        {
            UnsafeNativeMethods.EnumWindows(EnumWindowsFunc, NativeMethods.NULL);
        }

        private static bool EnumWindowsFunc(IntPtr hwnd, object lpParam)
        {
            string text = Util.GetText(hwnd);
            if (text.Contains("Enterprise Architect Ultimate Edition") && Util.IsTopMost(hwnd))
            {
                Util.SetNoTopMost(hwnd);
                return false;
            }

            return true;
        }
    }
}