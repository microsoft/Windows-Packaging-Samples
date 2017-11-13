using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Storage;

namespace WpfUIWithBackgroundTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UWPBackgroundTaskCatalog catalog = new UWPBackgroundTaskCatalog();
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }
        
        void LoadTasks()
        {
            var registeredTasks = catalog.GetRegisteredTasks();
            if (registeredTasks.Count == 0)
            {
                InfoBGTask.Text = "No BG Tasks Registered";
                RegisterButton.IsEnabled = true;
                UnregisterButton.IsEnabled = false;
                return;
            }
            RegisterButton.IsEnabled = false;
            UnregisterButton.IsEnabled = true;
            InfoBGTask.Text = string.Empty;
            foreach (var item in registeredTasks)
            {
                InfoBGTask.Text += item;
            }
            var url = ApplicationData.Current.LocalSettings.Values["UrlToVerify"].ToString();
            if (!string.IsNullOrEmpty(url))
            {
                UrlToTest.Text = url;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["UrlToVerify"] = UrlToTest.Text;
            catalog.RegisterBackgroundTask("MySampleTask");
            LoadTasks();
        }

        private void UnregisterButton_Click(object sender, RoutedEventArgs e)
        {
            catalog.Unregister("MySampleTask");
            LoadTasks();
        }
    }
}
