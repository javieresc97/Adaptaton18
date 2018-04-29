using System;
using System.Collections.Generic;
using AdaptatonApiEndpoint.Models;
using AdaptatonMobile.ViewModels;
using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class UnsafeZonePage : ContentPage
    {
        public UnsafeZonePage(IncidentRegister incident)
        {
            InitializeComponent();
            BindingContext = new UnsafeZoneViewModel(incident);
            ProblemPicker.SelectedIndex = 0;
        }
    }
}
