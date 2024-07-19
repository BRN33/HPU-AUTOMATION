using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace HPU_OTOMASYON
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            bool TekProcess;
            Mutex mx = new Mutex(true, "BenimGuzelMutexim", out TekProcess);
            if (!TekProcess)
            {
                //MessageBox.Show("Baþka bir kopya çalýþtýramazsýnýz");
                return;
            }

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Bir Hata Ýle Karþýlaþýldý! \nLütfen Program Loglarýna Bakýnýz...\nHata Mesajý : " + e.Exception.Message, "ExceptionMessages.LoggingExceptionMessage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logging.WriteLog(DateTime.Now.ToString(), e.Exception.Message.ToString(),
                e.Exception.StackTrace.ToString(), e.Exception.TargetSite.ToString(), "");
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //System.Diagnostics.Process.Start(
            //System.Reflection.Assembly.GetEntryAssembly().Location,
            //string.Join(" ", Environment.GetCommandLineArgs()));
            //Environment.Exit(1);

            MessageBox.Show("Bir Hata Ýle Karþýlaþýldý! \nLütfen Program Loglarýna Bakýnýz...\nHata Mesajý : " + (e.ExceptionObject as Exception).Message, "ExceptionMessages.LoggingExceptionMessage", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Exception ex = (Exception)e.ExceptionObject;

            Logging.WriteLog(DateTime.Now.ToString(), ex.Message.ToString(),
                ex.StackTrace.ToString(), ex.TargetSite.ToString(), "");
        }
    }
}
