using System;
using System.Threading.Tasks;

namespace Mapbox.Annotations
{
    //public partial class MarkerOptions
    //{
    //    protected override Java.Lang.Object RawMarker {
    //        get {
    //            return this.Marker;
    //        }
    //    }

    //    protected override Java.Lang.Object RawThis {
    //        get {
    //            return this.This;
    //        }
    //    }
    //}

    public partial class MarkerViewOptions
    {
        protected override Java.Lang.Object RawMarker {
            get {
                return this.Marker;
            }
        }

        protected override Java.Lang.Object RawThis {
            get {
                return this.This;
            }
        }
    }
}

namespace Mapbox.Maps
{
    public partial class MapView
    {
        public Task<MapboxMap> GetMapAsync ()
        {
            var tcs = new TaskCompletionSource<MapboxMap> ();

            var listener = new MapViewReadCallbackListener {
                OnMapReadyHandler = map => {
                    tcs.TrySetResult (map);
                }
            };

            this.GetMap (listener);

            return tcs.Task;
        }

        internal class MapViewReadCallbackListener : Java.Lang.Object, IOnMapReadyCallback
        {
            public Action<MapboxMap> OnMapReadyHandler { get; set; }
            public void OnMapReady (MapboxMap map)
            {
                if (OnMapReadyHandler != null)
                    OnMapReadyHandler (map);
            }
        }
    }
}

