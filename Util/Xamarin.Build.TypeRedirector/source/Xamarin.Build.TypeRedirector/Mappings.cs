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

            public static readonly AssemblyNameReference OpenTK =
                AssemblyNameReference.Parse("OpenTK-1.0, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065");
        }

        /// <summary>
        /// Map from Target Framework name to type mappings
        /// </summary>
        public static readonly Dictionary<string, Dictionary<string, AssemblyNameReference>> MovedTypes = new Dictionary<string, Dictionary<string, AssemblyNameReference>>
        {
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
                    // OpenTK
                    { "System.Drawing.Color", Assemblies.OpenTK },
                    { "System.Drawing.KnownColor", Assemblies.OpenTK },
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
                }
            }
        };
    }
}
