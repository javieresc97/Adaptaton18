using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint;
using AdaptatonApiEndpoint.Models;
using AdaptatonMobile.Helpers;
using AdaptatonMobile.Views;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Credentials Credentials { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginViewModel(string dni)
        {
            LoginCommand = new Command(async () => await DoLogin());
            Credentials = new Credentials() { Dni = dni };
        }

        private async Task DoLogin()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var user = await AdaptatonEndpoint.Instance.Authenticate(Credentials);
                if (user != null)
                {
                    Settings.UserId = user.Id;
                    ChangeMainPage(new HomePage(), true);
                }
                else
                {
                    await ShowAlert("Las credenciales no son correctas");
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
