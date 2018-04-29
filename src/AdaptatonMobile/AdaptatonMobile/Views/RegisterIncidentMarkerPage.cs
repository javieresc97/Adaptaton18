using System;
using AdaptatonMobile.Controls;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AdaptatonMobile.Views
{
    public class RegisterIncidentMarker : ContentPage
    {
        private Button _incidentBtn;
        private IGeolocator _geolocator;
        private ExtendedMap _map;
        private Plugin.Geolocator.Abstractions.Position _position;
        public Pin MyPositionPin { get; private set; }

        public RegisterIncidentMarker()
        {
            Title = "Registro de incidencia";

            _geolocator = CrossGeolocator.Current;
            _map = new ExtendedMap
            {
                MapType = MapType.Street,
                IsShowingUser = true
            };
            _map.Tap += _map_Tap;
            _incidentBtn = new Button
            {
                BackgroundColor = (Color)App.Current.Resources["Blue"],
                TextColor = Color.White,
                Text = "Agregar"
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
            await Navigation.PushAsync(new RegisterIncidentStatePage(MyPositionPin.Position.Latitude,
                                                                     MyPositionPin.Position.Longitude));
        }


        void _map_Tap(object sender, TapEventArgs e)
        {
            if (_map.Pins.Count == 0)
            {
                BuildPin(e.Position);
                _map.Pins.Add(MyPositionPin);
                return;
            }

            MovePin(e.Position);
        }

        private void BuildPin(Xamarin.Forms.Maps.Position position)
        {
            NewPin(position);
        }


        private void MovePin(Xamarin.Forms.Maps.Position position)
        {
            NewPin(position);

            Device.BeginInvokeOnMainThread(() =>
            {
                _map.Pins.Clear();
                _map.Pins.Add(MyPositionPin);
            });

        }
        private void NewPin(Xamarin.Forms.Maps.Position position)
        {
            MyPositionPin = new Pin()
            {
                Position = position,
                Type = PinType.Generic,
                Label = "Mi ubicación",
                Address = "Mi ubicación"
            };

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
                    var mapSpan = MapSpan.FromCenterAndRadius(formsMapPosition, Distance.FromKilometers(1));
                    _map.MoveToRegion(mapSpan);

                    MyPositionPin = new Pin
                    {
                        Address = "Mi ubicación",
                        Position = formsMapPosition,
                        Type = PinType.Generic,
                        Label = "Mi ubicación"
                    };
                    _map.Pins.Add(MyPositionPin);
                }
                else
                {
                    await DisplayAlert("Un momento", "Debes activar tu GPS para usar la aplicación.", "OK");
                }
            });

        }
    }
}

