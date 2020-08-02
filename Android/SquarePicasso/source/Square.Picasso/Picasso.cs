using Android.Content;
using System;

namespace Square.Picasso
{
	partial class Picasso
	{
		[Obsolete("Use Get() instead.")]
		public static Picasso With(Context context) => Get();
	}
}
