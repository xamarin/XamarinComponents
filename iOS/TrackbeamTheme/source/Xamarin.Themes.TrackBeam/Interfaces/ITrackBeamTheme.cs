using System;
using Xamarin.Themes.Enums;
using Xamarin.Themes.Core.Interfaces;
using UIKit;
using CoreGraphics;

namespace Xamarin.Themes.TrackBeam.Interfaces
{
	public interface ITrackBeamTheme : ITheme
	{
		UIStatusBarStyle StatusBarStyle {get;}

		UIColor MainColor {get;}
		UIColor SecondColor {get;}
		UIColor NavigationTextColor {get;}
		UIColor HighlightColor {get;}
		UIColor ShadowColor {get;}
		UIColor HighlightShadowColor {get;}
		UIColor NavigationTextShadowColor {get;}
		UIColor BackgroundColor {get;}

		UIFont NavigationFont {get;}
		UIFont BarButtonFont {get;}
		UIFont SegmentFont {get;}

		UIColor BaseTintColor {get;}
		UIColor AccentTintColor {get;}
		UIColor SelectedTabbarItemTintColor {get;}

		UIColor SwitchThumbColor {get;}
		UIColor SwitchOnColor {get;}
		UIColor SwitchTintColor {get;}
		UIColor SegmentedTintColor {get;}

		CGSize ShadowOffset {get;}

		UIImage TopShadow {get;}
		UIImage BottomShadow {get;}

		UIImage SearchBackground {get;}
		UIImage SearchScopeBackground {get;}
		UIImage SearchFieldImage {get;}
		UIImage SearchScopeButtonDivider {get;}


		UIImage TableBackground {get;}
		UIImage TableSectionHeaderBackground {get;}
		UIImage TableFooterBackground {get;}
		UIImage ViewBackground {get;}
		UIImage ViewBackgroundPattern {get;}
		UIImage ViewBackgroundTimeline {get;}

		UIImage SwitchOnImage {get;}
		UIImage SwitchOffImage {get;}
		UIImage SwitchOnIcon {get;}
		UIImage SwitchOffIcon {get;}
		UIImage SwitchTrack {get;}
		UIImage SliderMinTrack {get;}
		UIImage SliderMaxTrack {get;}
		UIImage SliderMinTrackDouble {get;}
		UIImage SliderMaxTrackDouble {get;}

		UIImage ProgressTrackImage {get;}
		UIImage ProgressProgressImage {get;}

		UIImage ProgressPercentTrackImage {get;}
		UIImage ProgressPercentProgressImage {get;}
		UIImage ProgressPercentProgressValueImage {get;}

		UIImage StepperIncrementImage {get;}
		UIImage StepperDecrementImage {get;}
		UIImage TabBarBackground {get;}
		UIImage TabBarSelectionIndicator {get;}

		UIImage NavigationBackgroundForIPad(UIInterfaceOrientation orientation);
		UIImage BarButtonBackground(UIControlState state, UIBarButtonItemStyle style, UIBarMetrics barMetrics);
		UIImage BackBackground(UIControlState state, UIBarMetrics barMetrics);
		UIImage ToolbarBackground(UIBarMetrics metrics);
		UIImage ViewBackgroundForOrientation(UIInterfaceOrientation orientation);
		UIImage ButtonBackground(UIControlState state);
		UIImage StepperBackground(UIControlState state);
		UIImage StepperDivider(UIControlState state);
		UIImage SwitchThumb(UIControlState state);
		UIImage SliderThumb(UIControlState state);
		UIImage SegmentedBackground(UIControlState state, UIBarMetrics barMetrics);
		UIImage SegmentedDivider(UIBarMetrics barMetrics);
		UIImage SearchImage(UISearchBarIcon icon,  UIControlState state);
		UIImage SearchScopeButtonBackground(UIControlState state);

		UIImage ButtonImage {get;}
		UIImage ButtonPressedImage {get;}

		/// <summary>
		/// Gets the background image name for Phone Portrait.
		/// </summary>
		/// <value>The phone background.</value>
		string PhoneBackgroundName {get;}
		/// <summary>
		/// Gets the background image name for Phone Landscape.
		/// </summary>
		/// <value>The phone background landscape.</value>
		string PhoneBackgroundLandscapeName {get;}
		/// <summary>
		/// Gets the background image name for iPad Portait.
		/// </summary>
		/// <value>The pad background.</value>
		string PadBackgroundName  {get;}
		/// <summary>
		/// Gets the background image name for iPad Landscape.
		/// </summary>
		/// <value>The pad background landscape.</value>
		string PadBackgroundLandscapeName {get;}

		/// <summary>
		/// Gets the name of the toolbar background.
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		string ToolbarBackgroundName {get;}

		/// <summary>
		/// Gets the name of the toolbar landscape.
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		string ToolbarLandscapeBackgroundName {get;}

		/// <summary>
		/// Gets the name of the Navigation Bar background in portrait
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		string NavigationBarBackgroundName {get;}

		/// <summary>
		/// Gets the name of the Navigation Bar background in Landscape
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		string NavigationBarLandscapeBackgroundName {get;}

		/// <summary>
		/// Gets the name of the right Navigation Bar background in portrait
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		string NavigationBarRightBackgroundName {get;}

		/// <summary>
		/// Gets the name of the right Navigation Bar background in landscape
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		string NavigationBarRightLandscapeBackgroundName {get;}
	
		/// <summary>
		/// Gets the name of the tabbar background image
		/// </summary>
		/// <value>The name of the tabbar background.</value>
		string TabbarBackgroundName {get;}
	}


}

