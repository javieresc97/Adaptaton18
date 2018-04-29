using System;
using System.Collections.Generic;
using AdaptatonApiEndpoint.Models;
using AdaptatonMobile.ViewModels;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AdaptatonMobile.Views
{
    public partial class SafeZonePage : ContentPage
    {
        MediaFile mediaFile;

        public SafeZonePage(IncidentRegister incident, MediaFile mediaFile)
        {
            InitializeComponent();
            EvidenceImg.Source = ImageSource.FromStream(mediaFile.GetStream);
            this.mediaFile = mediaFile;
            BindingContext = new SafeZoneViewModel(incident, mediaFile);
        }
    }
}
