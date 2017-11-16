using System;
using Xamarin.Themes.TrackBeam;
using UIKit;

namespace TrackBeamTheme_Sample_iOS.CustomTheme
{
	public class CustomTheme : TrackBeamTheme
	{
		public override UIColor BaseTintColor
		{
			get
			{
				return UIColor.Red;
			}
		}
	}
}

