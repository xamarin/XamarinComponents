The TrackBeam Theme for iOS provides a great looking theme for your iPhone and iPad applications.

**Features**  

 - New Fluent API  
 - Easily Customisable by sub-classing
 - Simple Clean API
 - Uses Apples UIAppearance API
 - Works with Unified and Classic Xamarin.iOS
 - V1 API still available for backward compatibility with previous Xamarin Themes

**Usage**  

To style your app with the default theme, `TrackBeamTheme`, you register the theme and apply it to the app within the `FinishedLaunching` method

	using Xamarin.Themes;
	using Xamarin.Themes.Core;
	using Xamarin.Themes.TrackBeam;
	...
	
	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		...
		ThemeManager.Register<TrackBeamTheme>().Apply();
	}


To apply a customised theme simply sub-class the `TrackBeamTheme` and override the virutal properties that you want to change

	using Xamarin.Themes.TrackBeam;
	...
	
	public class CustomTheme : TrackBeamTheme
	{
		public override UIKit.UIColor BaseTintColor
		{
			get
			{
				return UIColor.Red;
			}
		}
	}
 
 You can then apply your custom theme simply by registering and applying it to the app as before
 
	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		...
		ThemeManager.Register<CustomTheme>().Apply();
	}
	

