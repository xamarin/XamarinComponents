using System;
using Xamarin.Themes;
using Xamarin.Themes.Core;
using Xamarin.Themes.TrackBeam;
using Foundation;
using UIKit;

namespace TrackBeamTheme_Sample_iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			//Register the theme and use Fluent API to apply to the application
			ThemeManager.Register<TrackBeamTheme>().Apply();

			//Use a custom theme
			//ThemeManager.Register<CustomTheme>().Apply();

			////Use the V1 APi
			//Xamarin.Themes.V1.TrackBeam.TrackBeamTheme.Apply();
			return true;
		}
	}
}

