using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SharePhotoUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFile file;
        ShareOperation operation;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            operation = (ShareOperation)e.Parameter;
            if (operation.Data.Contains(StandardDataFormats.StorageItems))
            {
                var items = await operation.Data.GetStorageItemsAsync();
                file = items[0] as StorageFile;
                IRandomAccessStreamWithContentType stream = await file.OpenReadAsync();

                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    BitmapImage image = new BitmapImage();
                    this.img.Source = image;
                    await image.SetSourceAsync(stream);
                });
            }
        }

        private async void ShareBtn_Click(object sender, RoutedEventArgs e)
        {
            await file.CopyAsync(ApplicationData.Current.LocalFolder);
            operation.ReportCompleted();            
            await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
        }        
    }
}
