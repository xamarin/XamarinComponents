using System;
using System.Collections.Generic;
using Android.Runtime;

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
            public Builder Build()
            {
                return _Build().JavaCast<Builder>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }
    }
}

namespace Mapbox.Services.Directions.V5
{
    public partial class MapboxDirections
    {
        public partial class Builder
        {
            public Builder Build()
            {
                return _Build().JavaCast<Builder>();
            }
        }
    }
}

namespace Mapbox.Services.GeoCoding.V5
{
    public partial class MapboxGeocoding
    {
        public partial class Builder
        {
            public Builder Build()
            {
                return _Build().JavaCast<Builder>();
            }

            public Builder SetAccessToken(string accessToken)
            {
                return _SetAccessToken(accessToken).JavaCast<Builder>();
            }
        }
    }
}

namespace Mapbox.Services.StaticImage.V1
{
    public partial class MapboxStaticImage
    {
        public partial class Builder
        {
            public Builder Build()
            {
                return _Build().JavaCast<Builder>();
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
    }
}
