using System;
using System.Collections.Generic;
using AdaptatonMobile.ViewModels;
using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(string dni)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(dni);
        }
    }
}
