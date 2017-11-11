using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace WpfUIWithBackgroundTask
{
    public class UWPBackgroundTaskCatalog
    {
        public List<string> GetRegisteredTasks()
        {
            return BackgroundTaskRegistration.AllTasks.Select(t => t.Value.Name).ToList<string>();
        }

        public void Unregister(string triggerName)
        {
            var current = BackgroundTaskRegistration.AllTasks
                .Where(b => b.Value.Name == triggerName).FirstOrDefault().Value;

            if (current is null)
            {
                System.Diagnostics.Debug.WriteLine("Task not registered:" + triggerName);
            } else
            { 
                current.Unregister(true);
            }
        }

        public void RegisterBackgroundTask(String triggerName)
        {
            var current = BackgroundTaskRegistration.AllTasks
                .Where(b => b.Value.Name == triggerName).FirstOrDefault().Value;

            if (current is null)
            {
                BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
                builder.Name = triggerName;
                builder.SetTrigger(new MaintenanceTrigger(15, false));
                builder.TaskEntryPoint = "HttpPing.SiteVerifier";
                builder.Register();
                System.Diagnostics.Debug.WriteLine("BGTask registered:" + triggerName);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Task already:" + triggerName);
            }
        }
    }
}
