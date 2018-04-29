using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using AdaptatonMobile.Controls;
using Xamarin.Forms;
using AdaptatonMobile.Droid.Renderers;

[assembly: ExportRenderer(typeof(ExtendedMap), typeof(ExtendedMapRenderer))]
namespace AdaptatonMobile.Droid.Renderers
{
    public class ExtendedMapRenderer : MapRenderer
    {
        private bool _mapReady;
        private bool _isDrawn;

        public ExtendedMapRenderer(Context context) : base (context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            if (NativeMap != null)
                NativeMap.MapClick -= GoogleMap_MapClick;

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !_isDrawn)
            {
                _isDrawn = true;
                OnGoogleMapReady();
            }
        }

        private void OnGoogleMapReady()
        {
            if (_mapReady) return;

            NativeMap.MapClick += GoogleMap_MapClick;

            _mapReady = true;
        }

        private void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((ExtendedMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }

    }
}
