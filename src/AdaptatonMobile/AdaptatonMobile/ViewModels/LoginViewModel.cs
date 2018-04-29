using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint.Models;
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
            Credentials = new Credentials() { DNI = dni };
        }

        private async Task DoLogin()
        {
            ChangeMainPage(new HomePage(), true);
        }
    }
}
