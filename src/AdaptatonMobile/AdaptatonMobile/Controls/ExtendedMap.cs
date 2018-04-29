using System;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace AdaptatonMobile.Controls
{
    public class ExtendedMap : Map
    {
        public event EventHandler<TapEventArgs> Tap;

        public ExtendedMap()
        {

        }

        public ExtendedMap(MapSpan region) : base(region)
        {

        }

        public void OnTap(Position coordinate)
        {
            OnTap(new TapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(TapEventArgs e)
        {
            Tap?.Invoke(this, e);
        }
    }

    public class TapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }
}
