using Java.Interop;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;

using FloatingSearchViews.Suggestions.Models;

namespace FloatingSearchViewSample
{
	public class ColorSuggestion : Java.Lang.Object, ISearchSuggestion
	{
		public ColorSuggestion (string colorName)
		{
			Color = null;
			ColorName = colorName;
		}

		public ColorSuggestion (Colour color)
		{
			Color = color;
			ColorName = Color.Name;
		}

		public Colour Color { get; private set; }

		public string ColorName { get; private set; }

		public bool IsHistory { get; set; }

		// ISearchSuggestion interface

		public string GetBody ()
		{
			return ColorName;
		}

		public void SetBodyText (TextView textView)
		{
		}

		public bool SetLeftIcon (ImageView imageView)
		{
			if (IsHistory) {
				imageView.SetImageDrawable (imageView.Resources.GetDrawable (Resource.Drawable.ic_history_black_24dp));
				imageView.Alpha = 0.36f;
			} else {
				imageView.SetImageDrawable (new ColorDrawable (Color.Color));
			}

			return true;
		}

		public IParcelableCreator GetCreator ()
		{
			return CREATOR ();
		}

		// IParcelable interface

		[ExportField ("CREATOR")]
		public static IParcelableCreator CREATOR ()
		{
			return new ColorSuggestionCreator ();
		}

		public int DescribeContents ()
		{
			return 0;
		}

		public void WriteToParcel (Parcel dest, ParcelableWriteFlags flags)
		{
			dest.WriteString (ColorName);
		}

		public class ColorSuggestionCreator : Java.Lang.Object, IParcelableCreator
		{
			public Java.Lang.Object CreateFromParcel (Parcel source)
			{
				return new ColorSuggestion (source.ReadString ());
			}

			public Java.Lang.Object[] NewArray (int size)
			{
				return new ColorSuggestion[size];
			}
		}
	}
}
