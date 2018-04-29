using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint;
using AdaptatonApiEndpoint.Models;
using AdaptatonMobile.Views;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class CheckLoginViewModel : BaseViewModel
    {
        public string DNI { get; set; }
        public ICommand CheckDNICommand { get; set; }
        
        public CheckLoginViewModel()
        {
            CheckDNICommand = new Command(async (obj) => await CheckDNI());
        }

        private async Task CheckDNI()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (DNI.Length != 8)
                {
                    await ShowAlert("El DNI no es correcto. Debe tener 8 números.");
                }
                else
                {
                    var isUserRegistered = await AdaptatonEndpoint.Instance.CheckDNI(DNI);
                    if (isUserRegistered)
                    {
                        await NavigateTo(new LoginPage(DNI));
                    }
                    else
                    {
                        await NavigateTo(new RegisterUserPage(DNI));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
