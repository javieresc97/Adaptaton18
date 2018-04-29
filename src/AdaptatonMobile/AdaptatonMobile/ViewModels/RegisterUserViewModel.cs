using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AdaptatonApiEndpoint.Models;
using Xamarin.Forms;

namespace AdaptatonMobile.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel
    {
        public UserRegister NewUser { get; set; }
        public ICommand RegisterUserCommand { get; set; }

        public RegisterUserViewModel(string dni)
        {
            NewUser = new UserRegister() { Dni = dni };
            RegisterUserCommand = new Command(async () => await RegisterUser());
        }

        private async Task RegisterUser()
        {
            
        }
    }
}
