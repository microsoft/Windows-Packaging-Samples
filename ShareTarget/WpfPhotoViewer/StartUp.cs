using Rido.SingleInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPhotoViewer
{
    public class StartUp
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var wrapper = new SingleInstanceManager<App>();
            wrapper.Run(args);
        }
    }
}
