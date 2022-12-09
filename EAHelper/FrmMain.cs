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
            base.SetVisibleCore(false);
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ClickYes();
            SetEaNoTopMost();
        }

        private static void ClickYes()
        {
            nint handDialog = UnsafeNativeMethods.FindWindow("#32770", "Unsaved Property Changes");
            if (handDialog == nint.Zero)
                return;

            nint handYes = UnsafeNativeMethods.FindWindowEx(handDialog, IntPtr.Zero, "Button", "Yes");
            if (handYes == nint.Zero)
                return;
            Util.SendMouseClick(handYes, Point.Empty);
        }

        private static void SetEaNoTopMost()
        {
            UnsafeNativeMethods.EnumWindows(EnumWindowsFunc, NativeMethods.NULL);
        }

        private static bool EnumWindowsFunc(nint hand, object lpParam)
        {
            string text = Util.GetText(hand);
            // ReSharper disable once InvertIf
            if (text.Contains("Enterprise Architect Ultimate Edition") && Util.IsTopMost(hand))
            {
                Util.SetNoTopMost(hand);
                return false;
            }

            return true;
        }
    }
}