using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint.Models;
using AdaptatonMobile.Helpers;
using AdaptatonMobile.Views;
using Plugin.Media;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class RegisterIncidentStateViewModel : BaseViewModel
    {
        public IncidentRegister Incident { get; set; }
        public ICommand SafeZoneCommand { get; set; }
        public ICommand UnsafeZoneCommand { get; set; }

        public RegisterIncidentStateViewModel(decimal latitude, decimal longitude)
        {
            Incident = new IncidentRegister { Latitude = latitude, Longitude = longitude, UserId = Settings.UserId };
            SafeZoneCommand = new Command(async () => await ManageSafeZone());
            UnsafeZoneCommand = new Command(async () => await ManageUnsafeZone());
        }

        private async Task ManageUnsafeZone()
        {
            await NavigateTo(new UnsafeZonePage(Incident));
        }

        private async Task ManageSafeZone()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await ShowAlert("Es necesario usar tu cámara, por favor activa el permiso necesario.");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });
            if (file == null)
                return;

            await NavigateTo(new SafeZonePage(Incident, file));
        }
    }
}
