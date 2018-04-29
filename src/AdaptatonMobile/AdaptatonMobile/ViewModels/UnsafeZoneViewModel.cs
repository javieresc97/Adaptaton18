using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint;
using AdaptatonApiEndpoint.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class UnsafeZoneViewModel: BaseViewModel
    {
        public IncidentRegister Incident { get; set; }
        public MediaFile MediaFile { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand TakePicCommand { get; set; }

        public UnsafeZoneViewModel(IncidentRegister incident)
        {
            Incident = incident;
            SendCommand = new Command(async () => await Send());
            TakePicCommand = new Command(async () => await TakePic());
        }

        private async Task TakePic()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await ShowAlert("Es necesario usar tu cámara, por favor activa el permiso necesario.");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small
            });

            if (file == null)
                return;

            MediaFile = file;
        }

        private async Task Send()
        {
            if (MediaFile != null)
                Incident.Photo = ReadFully(MediaFile.GetStream());
            Incident.Secure = false;
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
