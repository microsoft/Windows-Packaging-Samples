
using System.Windows;

namespace WpfUIWithBackgroundTask
{
    public class StartUp
    {
        [System.STAThread]
        public static void Main(string[] args)
        {
            var wrapper = new Rido.SingleInstance.SingleInstanceManager<App>();
            wrapper.Run(args);
        }
    }

    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}