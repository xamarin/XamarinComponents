using Android.OS;
using Java.Interop;

using FloatingSearchViews.Suggestions.Models;

namespace FloatingSearchViewSample
{
	public class ColorSuggestion : Java.Lang.Object, ISearchSuggestion
	{
		public ColorSuggestion(string colorName)
		{
			Color = null;
			ColorName = colorName;
		}

		public ColorSuggestion(ColorModel color)
		{
			Color = color;
			ColorName = Color.Name;
		}

		public ColorModel Color { get; private set; }

		public string ColorName { get; private set; }

		public bool IsHistory { get; set; }

		// ISearchSuggestion interface

		public string GetBody() => ColorName;

		// IParcelable interface

		[ExportField("CREATOR")]
		public static IParcelableCreator CREATOR() => new ColorSuggestionCreator();

		int IParcelable.DescribeContents() => 0;

		void IParcelable.WriteToParcel(Parcel dest, ParcelableWriteFlags flags)
		{
			dest.WriteString(ColorName);
		}

		private class ColorSuggestionCreator : Java.Lang.Object, IParcelableCreator
		{
			public Java.Lang.Object CreateFromParcel(Parcel source)
			{
				return new ColorSuggestion(source.ReadString());
			}

			public Java.Lang.Object[] NewArray(int size)
			{
				return new ColorSuggestion[size];
			}
		}
	}
}
