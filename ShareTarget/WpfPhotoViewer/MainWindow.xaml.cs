using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfPhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PathInfo.Text = PhotosFolder.Current;
            ConfigureFileSystemWatcher();
            LoadData();
        }

        void ConfigureFileSystemWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(PhotosFolder.Current);
            watcher.EnableRaisingEvents = true;
            watcher.Created += Watcher_Created; ;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                if (File.Exists(e.FullPath))
                {
                    ImagesList.Items.Add(e.FullPath);
                    ImageView.Source = new BitmapImage(new Uri(e.FullPath));
                }
            }));
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            var b = e == EventArgs.Empty;

            System.Diagnostics.Debug.WriteLine("OnStateChanged " + b);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var b = e == EventArgs.Empty;

            System.Diagnostics.Debug.WriteLine("Activted " + b);            
        }

        public void LoadData()
        {
            ImagesList.Items.Clear();
            foreach (var item in Directory.EnumerateFiles(PhotosFolder.Current))
            {
                ImagesList.Items.Add(item);
            }
        }
        
        private void ImagesList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if ((ImagesList.SelectedValue!=null) && File.Exists(ImagesList.SelectedValue.ToString()))
            {
                ImageView.Source = new BitmapImage(new Uri(ImagesList.SelectedValue.ToString()));
            }
            
        }
    }
}
