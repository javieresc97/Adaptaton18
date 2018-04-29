using System;
using AdaptatonApiEndpoint.Models;

namespace AdaptatonMobile.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel
    {
        public UserRegister NewUser { get; set; }

        public RegisterUserViewModel(string dni)
        {
            NewUser = new UserRegister() { DNI = dni };
        }
    }
}
