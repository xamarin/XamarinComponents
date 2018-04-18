## Usage  

**Registering and Applying a theme**

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


To apply a customised theme simply sub-class `TrackBeamTheme` and override the virtual properties that you want to change

	using Xamarin.Themes.TrackBeam;
	...
	
	public class CustomTheme : TrackBeamTheme
	{
		//Change the BaseTintColor
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

**Background images**

As of version 1.5 you are able to override the filenames of the background images for the Views, Toolbar, Tabbar and Navigation bars.

Simply create a sub-class of the `TrackBeamTheme` class and override one or more of the following properties.

 - PhoneBackgroundName
 - PhoneBackgroundLandscapeName
 - PadBackgroundName
 - PadBackgroundLandscapeName
 - ToolbarBackgroundName
 - ToolbarLandscapeBackgroundName
 - NavigationBarBackgroundName
 - NavigationBarLandscapeBackgroundName
 - NavigationBarRightBackgroundName
 - NavigationBarRightLandscapeBackgroundName
 - TabbarBackgroundName


**Apply to views**  

You can also selectively apply the theme to specific views by using the relevant `Apply` method for the view type.

	public override void ViewWillAppear (bool animated)
	{
		base.ViewWillAppear (animated);
		
		//View is a UIView
		ThemeManager.Current.Apply(View);
	
	}

or by the Appearance for that view type

	public override void ViewWillAppear (bool animated)
	{
		base.ViewWillAppear (animated);
		
		//
		ThemeManager.Current.Apply(UIView.Appearance);
	}
	
To apply the theme to specific view classes only when they are contained within other specific view classes, use AppearanceWhenContainedIn.

	public override void ViewWillAppear (bool animated)
	{
		base.ViewWillAppear (animated);
		
		//
		ThemeManager.Current.Apply(UIProgressView.AppearanceWhenContainedIn (typeof (UINavigationBar)));
	}  

**Fluent API**  
The TrackBeamTheme has been designed with a Fluent style API allowing you to chain several calls together to create more attractive and readable code.

	public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();
		
		this.Title = "Elements";
		
		//Apply theme to multiple elements
		ThemeManager.Current.Apply(View)
			.Apply(loginButton)
			.Apply(registerButton)
			.Apply(textField);
	}

**V1 API**  
In previous Xamarin Themes we have used an API based around static methods on the theme Class, which we now refer to as the V1 API.  We are moving away from the approach but we have provided a V1 style implementation just incase you wish to use it.

The V1 implementation exists in the `Xamarin.Themes.V1.TrackBeam` namespace and there is a `TrackBeamTheme` class that provides all of the V1 methods.  This can be used instead of the new API in the same as before.

	using Xamarin.Themes.V1.TrackBeam;
	...
	
	public override bool FinishedLaunching (UIApplication app, NSDictionary options)
	{
		...
		TrackBeamTheme.Apply();
	}

*Note: This class and all the methods are flagged as obsolete and it is just a thin wrapper around the new API* 

**Supported Views**  
Below is a list of the controls that can themed, though not all of them are implemented. 
 
 - UIView  
 - UIBarButtonItem  
 - UIToolbar  
 - UISlider  
 - UISegmentedControl  
 - UIProgressView  
 - UITabBar  
 - UIButton  
 - UISearchBar  
 - UISwitch  
 - UITextField  
 - UILabel  
 - UITableView
 - UIRefreshControl
 - UITableViewCell
 - UITableViewController
 - UIViewController
 

