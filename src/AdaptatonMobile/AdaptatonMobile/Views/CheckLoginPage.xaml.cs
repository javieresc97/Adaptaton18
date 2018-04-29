using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class CheckLoginPage : ContentPage
    {
        public CheckLoginPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.CheckLoginViewModel();
        }
    }
}
