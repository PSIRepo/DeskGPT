using System.IO;
using System.Threading;
using System.Windows;

namespace dGPT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //static System.Threading.Mutex mutex = new System.Threading.Mutex(true, "{D775061A-A841-46C8-8596-8C64A7DD49A4}");
        public static Mutex _instanceMutex = null;

        private bool retr = false;

        protected override void OnStartup(StartupEventArgs e)
        {
        ret:
            bool createdNew;
            _instanceMutex = new Mutex(true, @"{D775061A-A841-46C8-8596-8C64A7DD49A4}", out createdNew);
            if (!createdNew)
            {
                System.Threading.Thread.Sleep(5000);
                if (!retr)
                {
                    retr = true;
                    goto ret;
                }
                _instanceMutex = null;
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(dir);
        }
    }
}