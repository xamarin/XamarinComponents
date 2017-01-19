using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Runtime;

namespace Mapbox.Services.Commons
{
    internal class RetrofitCallbackHandler<TResponse> : Java.Lang.Object, Square.Retrofit2.ICallback where TResponse : Java.Lang.Object
    {
        TaskCompletionSource<TResponse> tcsResponse = new TaskCompletionSource<TResponse>();

        public void OnFailure(Square.Retrofit2.ICall call, Java.Lang.Throwable error)
        {
            tcsResponse.TrySetException(new Exception(error.Message));
        }

        public void OnResponse(Square.Retrofit2.ICall call, Square.Retrofit2.Response response)
        {
            tcsResponse.TrySetResult(response.Body().JavaCast<TResponse>());
        }

        public Task<TResponse> GetResponseAsync()
        {
            return tcsResponse.Task;
        }
    }
}

namespace Mapbox.Services.Commons.GeoJson
{

    // Metadata.xml XPath class reference: path="/api/package[@name='com.mapbox.services.commons.geojson']/class[@name='LineString']"
    public partial class LineString
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return new JavaList(Coordinates); }
            set { Coordinates = (IList<Mapbox.Services.Commons.Models.Position>)value; }
        }
    }

    public partial class MultiLineString
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return new JavaList(Coordinates); }
            set { Coordinates = (IList<IList<Mapbox.Services.Commons.Models.Position>>)value; }
        }
    }

    public partial class Point
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return Coordinates; }
            set { Coordinates = (Mapbox.Services.Commons.Models.Position)value; }
        }
    }

    public partial class Polygon
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return new JavaList(Coordinates); }
            set { Coordinates = (IList<IList<Mapbox.Services.Commons.Models.Position>>)value; }
        }
    }

    public partial class MultiPoint
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return new JavaList(Coordinates); }
            set { Coordinates = (IList<Mapbox.Services.Commons.Models.Position>)value; }
        }
    }

    public partial class MultiPolygon
    {
        public Java.Lang.Object CoordinatesRaw
        {
            get { return new JavaList(Coordinates); }
            set { Coordinates = (IList<IList<IList<Mapbox.Services.Commons.Models.Position>>>)value; }
        }
    }
}

namespace Mapbox.Services.Directions.V4
{
    public partial class MapboxDirections
    {
        public partial class Builder
        {
            public MapboxDirections Build()
            {
                return _Build().JavaCast<MapboxDirections>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }

        public Task<Mapbox.Services.Directions.V4.Models.DirectionsResponse> ExecuteCallAsync()
        {
            var cb = new Mapbox.Services.Commons.RetrofitCallbackHandler<Mapbox.Services.Directions.V4.Models.DirectionsResponse>();

            EnqueueCall(cb);

            return cb.GetResponseAsync();
        }
    }
}

namespace Mapbox.Services.Directions.V5
{
    public partial class MapboxDirections
    {
        public partial class Builder
        {
            public MapboxDirections Build()
            {
                return _Build().JavaCast<MapboxDirections>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }

        public Task<Mapbox.Services.Directions.V5.Models.DirectionsResponse> ExecuteCallAsync()
        {
            var cb = new Mapbox.Services.Commons.RetrofitCallbackHandler<Mapbox.Services.Directions.V5.Models.DirectionsResponse>();

            EnqueueCall(cb);

            return cb.GetResponseAsync();
        }
    }
}

namespace Mapbox.Services.GeoCoding.V5
{
    public partial class MapboxGeocoding
    {
        public partial class Builder
        {
            public MapboxGeocoding Build()
            {
                return _Build().JavaCast<MapboxGeocoding>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }

        public Task<Mapbox.Services.GeoCoding.V5.Models.GeocodingResponse> ExecuteCallAsync()
        {
            var cb = new Mapbox.Services.Commons.RetrofitCallbackHandler<Mapbox.Services.GeoCoding.V5.Models.GeocodingResponse>();

            EnqueueCall(cb);

            return cb.GetResponseAsync();
        }
    }
}

namespace Mapbox.Services.StaticImage.V1
{
    public partial class MapboxStaticImage
    {
        public partial class Builder
        {
            public MapboxStaticImage Build()
            {
                return _Build().JavaCast<MapboxStaticImage>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }
    }
}

namespace Mapbox.Services.MapMatching.V4
{
    public partial class MapboxMapMatching
    {
        public partial class Builder
        {
            public override Java.Lang.Object _Build()
            {
                return Build();
            }

            public override Mapbox.Services.Commons.MapboxBuilder _SetAccessToken(string accessToken)
            {
                return SetAccessToken(accessToken);
            }
        }

        public Task<Mapbox.Services.MapMatching.V4.Models.MapMatchingResponse> ExecuteCallAsync()
        {
            var cb = new Mapbox.Services.Commons.RetrofitCallbackHandler<Mapbox.Services.MapMatching.V4.Models.MapMatchingResponse>();

            EnqueueCall(cb);

            return cb.GetResponseAsync();
        }
    }
}
