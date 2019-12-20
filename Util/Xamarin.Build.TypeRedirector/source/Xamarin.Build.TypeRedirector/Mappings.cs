using Mono.Cecil;
using System.Collections.Generic;

namespace Xamarin.Build.TypeRedirector
{
    public static class Mappings
    {
        public static class Assemblies
        {
            public static readonly AssemblyNameReference XamarinMac =
                AssemblyNameReference.Parse("Xamarin.Mac, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference XamariniOS =
                AssemblyNameReference.Parse("Xamarin.iOS, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference XamarinTVOS =
                AssemblyNameReference.Parse("Xamarin.TVOS, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference XamarinWatchOS =
                AssemblyNameReference.Parse("Xamarin.WatchOS, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference MonoTouch =
                AssemblyNameReference.Parse("monotouch, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference MonoAndroid =
                AssemblyNameReference.Parse("Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");

            public static readonly AssemblyNameReference System =
                AssemblyNameReference.Parse("System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");

            public static readonly AssemblyNameReference OpenTK =
                AssemblyNameReference.Parse("OpenTK, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
            public static readonly AssemblyNameReference OpenTK10 =
                AssemblyNameReference.Parse("OpenTK-1.0, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
        }

        /// <summary>
        /// Map from Target Framework name to type mappings
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, AssemblyNameReference>> MovedTypes = new Dictionary<string, Dictionary<string, AssemblyNameReference>>
        {
            {
                "Xamarin.Mac",
                new Dictionary<string, AssemblyNameReference>
                {
                    // Xamarin.iOS
                    { "System.Drawing.Point", Assemblies.XamarinMac },
                    { "System.Drawing.PointF", Assemblies.XamarinMac },
                    { "System.Drawing.Rectangle", Assemblies.XamarinMac },
                    { "System.Drawing.RectangleF", Assemblies.XamarinMac },
                    { "System.Drawing.Size", Assemblies.XamarinMac },
                    { "System.Drawing.SizeF", Assemblies.XamarinMac },
                    // OpenTK
                    { "System.Drawing.Color", Assemblies.OpenTK },
                    { "System.Drawing.KnownColor", Assemblies.OpenTK },
                    // System
                    { "System.Collections.Generic.Queue`1", Assemblies.System },
                    { "System.Collections.Generic.Stack`1", Assemblies.System },
                }
            },
            {
                "Xamarin.iOS",
                new Dictionary<string, AssemblyNameReference>
                {
                    // Xamarin.iOS
                    { "System.Drawing.Point", Assemblies.XamariniOS },
                    { "System.Drawing.PointF", Assemblies.XamariniOS },
                    { "System.Drawing.Rectangle", Assemblies.XamariniOS },
                    { "System.Drawing.RectangleF", Assemblies.XamariniOS },
                    { "System.Drawing.Size", Assemblies.XamariniOS },
                    { "System.Drawing.SizeF", Assemblies.XamariniOS },
                    // OpenTK-1.0
                    { "System.Drawing.Color", Assemblies.OpenTK10 },
                    { "System.Drawing.KnownColor", Assemblies.OpenTK10 },
                    // System
                    { "System.Collections.Generic.Queue`1", Assemblies.System },
                    { "System.Collections.Generic.Stack`1", Assemblies.System },
                }
            },
            {
                "Xamarin.TVOS",
                new Dictionary<string, AssemblyNameReference>
                {
                    // Xamarin.TVOS
                    { "System.Drawing.Point", Assemblies.XamarinTVOS },
                    { "System.Drawing.PointF", Assemblies.XamarinTVOS },
                    { "System.Drawing.Rectangle", Assemblies.XamarinTVOS },
                    { "System.Drawing.RectangleF", Assemblies.XamarinTVOS },
                    { "System.Drawing.Size", Assemblies.XamarinTVOS },
                    { "System.Drawing.SizeF", Assemblies.XamarinTVOS },
                    // OpenTK-1.0
                    { "System.Drawing.Color", Assemblies.OpenTK10 },
                    { "System.Drawing.KnownColor", Assemblies.OpenTK10 },
                    // System
                    { "System.Collections.Generic.Queue`1", Assemblies.System },
                    { "System.Collections.Generic.Stack`1", Assemblies.System },
                }
            },
            {
                "Xamarin.WatchOS",
                new Dictionary<string, AssemblyNameReference>
                {
                    // Xamarin.WatchOS
                    { "System.Drawing.Color", Assemblies.XamarinWatchOS },
                    { "System.Drawing.KnownColor", Assemblies.XamarinWatchOS },
                    { "System.Drawing.Point", Assemblies.XamarinWatchOS },
                    { "System.Drawing.PointF", Assemblies.XamarinWatchOS },
                    { "System.Drawing.Rectangle", Assemblies.XamarinWatchOS },
                    { "System.Drawing.RectangleF", Assemblies.XamarinWatchOS },
                    { "System.Drawing.Size", Assemblies.XamarinWatchOS },
                    { "System.Drawing.SizeF", Assemblies.XamarinWatchOS },
                    // System
                    { "System.Collections.Generic.Queue`1", Assemblies.System },
                    { "System.Collections.Generic.Stack`1", Assemblies.System },
                }
            },
            {
                "MonoAndroid",
                new Dictionary<string, AssemblyNameReference>
                {
                    // MonoAndroid
                    { "System.Drawing.Color", Assemblies.MonoAndroid },
                    { "System.Drawing.ColorConverter", Assemblies.MonoAndroid },
                    { "System.Drawing.KnownColor", Assemblies.MonoAndroid },
                    { "System.Drawing.Point", Assemblies.MonoAndroid },
                    { "System.Drawing.PointF", Assemblies.MonoAndroid },
                    { "System.Drawing.Rectangle", Assemblies.MonoAndroid },
                    { "System.Drawing.RectangleF", Assemblies.MonoAndroid },
                    { "System.Drawing.Size", Assemblies.MonoAndroid },
                    { "System.Drawing.SizeF", Assemblies.MonoAndroid },
                    { "System.Drawing.SystemColors", Assemblies.MonoAndroid },
                    // System
                    { "System.Collections.Generic.Queue`1", Assemblies.System },
                    { "System.Collections.Generic.Stack`1", Assemblies.System },
                }
            }
        };
    }
}
