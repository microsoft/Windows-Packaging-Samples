using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.Web.Http;

namespace HttpPing
{
    public sealed class SiteVerifier : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            taskInstance.Canceled += TaskInstance_Canceled;
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();
            var msg = await MeasureRequestTime();
            ShowToast(msg);
            deferral.Complete();
        }

        private async Task<string> MeasureRequestTime()
        {
            string msg;
            try
            {
                var url = ApplicationData.Current.LocalSettings.Values["UrlToVerify"] as string;
                var http = new HttpClient();
                Stopwatch clock = Stopwatch.StartNew();
                var response = await http.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                var elapsed = clock.ElapsedMilliseconds;
                clock.Stop();
                msg = $"{url} took {elapsed.ToString()} ms";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        
        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            BackgroundTaskDeferral deferral = sender.GetDeferral();
            ShowToast("Background task canceled: " + reason.ToString());
            deferral.Complete();
        }

        private void ShowToast(string msg)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
