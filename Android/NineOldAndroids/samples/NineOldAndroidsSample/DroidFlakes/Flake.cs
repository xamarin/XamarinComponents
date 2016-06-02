using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace NineOldAndroidsSample.DroidFlakes
{
    public class Flake
    {
        // This map stores pre-scaled bitmaps according to the width. No reason to create
        // new bitmaps for sizes we've already seen.
        private static Dictionary<int, Bitmap> bitmapMap = new Dictionary<int, Bitmap>();

        public float X { get; set; }

        public float Y { get; set; }

        public float Rotation { get; set; }

        public float Speed { get; set; }

        public float RotationSpeed { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Bitmap Bitmap { get; private set; }

        /// <summary>
        /// Creates a new droidflake in the given xRange and with the given bitmap. Parameters of
        /// location, size, rotation, and speed are randomly determined.
        /// </summary>
        public static Flake CreateFlake(float xRange, Bitmap originalBitmap)
        {
            var rnd = new Random();

            var flake = new Flake();

            // Size each flake with a width between 5 and 55 and a proportional height
            flake.Width = rnd.Next(5, 55);
            var hwRatio = originalBitmap.Height / originalBitmap.Width;
            flake.Height = flake.Width * hwRatio;

            // Position the flake horizontally between the left and right of the range
            flake.X = rnd.Next((int)(xRange - flake.Width));
            // Position the flake vertically slightly off the top of the display
            flake.Y = -rnd.Next(flake.Height, flake.Height * 2);

            // Each flake travels at 50-200 pixels per second
            flake.Speed = rnd.Next(50, 200);

            // Flakes start at -90 to 90 degrees rotation, and rotate between -45 and 45
            // degrees per second
            flake.Rotation = rnd.Next(-90, 90);
            flake.RotationSpeed = rnd.Next(-45, 45);

            // Get the cached bitmap for this size if it exists, otherwise create and cache one
            if (!bitmapMap.ContainsKey(flake.Width))
            {
                flake.Bitmap = Bitmap.CreateScaledBitmap(originalBitmap, flake.Width, flake.Height, true);
                bitmapMap[flake.Width] = flake.Bitmap;
            }
            else
            {
                flake.Bitmap = bitmapMap[flake.Width];
            }

            return flake;
        }
    }
}
