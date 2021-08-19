using System;
using System.Threading;
using System.Windows.Forms;

namespace EAHelper
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            const string name = "EAHelper";
            using Mutex mutex = new(true, name, out bool run);
            if (run)
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain());

                mutex.ReleaseMutex(); //添加此行,防止生成Release版本时Mutex无效.
            }
        }
    }
}