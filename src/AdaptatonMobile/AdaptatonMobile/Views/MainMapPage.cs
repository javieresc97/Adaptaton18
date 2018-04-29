using System;
using System.Collections.Generic;
using AdaptatonApiEndpoint;
using AdaptatonApiEndpoint.Models;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AdaptatonMobile.Views
{
    public class MainMapPage : ContentPage
    {
        private List<Incident> Incidents;
        private Button _incidentBtn;
        private IGeolocator _geolocator;
        private Map _map;
        private Plugin.Geolocator.Abstractions.Position _position;

        public MainMapPage()
        {
            _geolocator = CrossGeolocator.Current;
            _map = new Map
            {
                MapType = MapType.Street,
                IsShowingUser = true
            };
            _incidentBtn = new Button()
            {
                BackgroundColor = (Color)App.Current.Resources["Blue"],
                TextColor = Color.White,
                Text = "Nuevo reporte"
            };
            _incidentBtn.Clicked += _incidentBtn_Clicked;
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = 40 }
                },
                RowSpacing = 0
            };
            grid.Children.Add(_map);
            grid.Children.Add(_incidentBtn, 0, 1);

            Content = grid;
        }

        async void _incidentBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterIncidentMarker());
        }


        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_geolocator.IsGeolocationEnabled)
                {
                    _position = await _geolocator.GetLastKnownLocationAsync();
                    if (_position == null)
                    {
                        _position = await _geolocator.GetPositionAsync();
                    }
                    var formsMapPosition = new Xamarin.Forms.Maps.Position(_position.Latitude, _position.Longitude);
                    var mapSpan = MapSpan.FromCenterAndRadius(formsMapPosition, Distance.FromKilometers(1.5));
                    _map.MoveToRegion(mapSpan);

                    Incidents = await AdaptatonEndpoint.Instance.GetIncidents();
                    foreach (var incident in Incidents)
                    {
                        _map.Pins.Add(new Pin
                        {
                            Position = new Xamarin.Forms.Maps.Position((double)incident.Latitude,
                                                                       (double)incident.Longitude),
                            Label = incident.Id
                        });
                    }
                }
                else
                {
                    await DisplayAlert("Un momento", "Debes activar tu GPS para usar la aplicación.", "OK");
                }
            });

        }
    }
}

