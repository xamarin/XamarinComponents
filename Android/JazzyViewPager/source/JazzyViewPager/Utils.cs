using System;
using Android.Content.Res;

namespace Jazzy
{
    internal static class Utils
    {
        private static readonly float density = Resources.System.DisplayMetrics.Density;

        public static int Dp2Px(int dp)
        {
            return (int)Math.Round(dp * density);
        }
    }
}
