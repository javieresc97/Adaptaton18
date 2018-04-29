using System;
using System.Collections.Generic;
using AdaptatonMobile.ViewModels;
using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class RegisterIncidentStatePage : ContentPage
    {
        public RegisterIncidentStatePage()
        {
            InitializeComponent();
        }

        public RegisterIncidentStatePage(double latitude, double longitude)
        {
            InitializeComponent();
            BindingContext = new RegisterIncidentStateViewModel((decimal)latitude, (decimal)longitude);
        }
    }
}
