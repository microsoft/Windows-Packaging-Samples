using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPhotoViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
               // ProcessCommandLine(e.Args);
            }

            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.Show();

        }

        
        public void Activate()
        {
            this.MainWindow.Activate();
        }

        public void Init()
        {
            ((MainWindow)base.MainWindow).LoadData();
        }

        internal void ProcessCommandLine(string[] args)
        {
            if (args.Length > 0)
            {
                var path = args[0];
                var sourcePath = new FileInfo(path);
                if (sourcePath.Exists)
                {
                    var destPath = new FileInfo(Path.Combine(PhotosFolder.Current, sourcePath.Name));
                    if (!destPath.Exists)
                    {
                        File.Copy(sourcePath.FullName, destPath.FullName);
                    }
                }
            }
        }
    }
}
