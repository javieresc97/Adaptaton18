using System;
using System.Collections.Generic;
using AdaptatonMobile.ViewModels;
using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class RegisterUserPage : ContentPage
    {
        public RegisterUserPage()
        {
            InitializeComponent();
        }

        public RegisterUserPage(string dni)
        {
            InitializeComponent();
            BindingContext = new RegisterUserViewModel(dni);
        }
    }
}
