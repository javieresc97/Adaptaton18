using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint;
using AdaptatonApiEndpoint.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class SafeZoneViewModel : BaseViewModel
    {
        public IncidentRegister Incident { get; set; }
        public MediaFile MediaFile { get; set; }
        public ICommand SendCommand { get; set; }

        public SafeZoneViewModel(IncidentRegister incident, MediaFile mediaFile)
        {
            Incident = incident;
            MediaFile = mediaFile;
            SendCommand = new Command(async () => await SendIncident());
        }

        private async Task SendIncident()
        {
            Incident.Photo = ReadFully(MediaFile.GetStream());
            Incident.Secure = true;
            await AdaptatonEndpoint.Instance.SaveIncident(Incident);
            await NavigateToRoot();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
