using System;
using Xamarin.Themes.TrackBeam.Interfaces;
using Xamarin.Themes.Enums;
using Xamarin.Themes.Core;
using Xamarin.Themes.Core.Interfaces;
using UIKit;
using CoreGraphics;

namespace Xamarin.Themes.TrackBeam
{
	public class TrackBeamTheme : ITrackBeamTheme
	{
	
		#region Private
		private const string ImagesPath = "TrackBeamThemeImages";
		private String PathOfImage(String imageName)
		{
			var calName = imageName;

			if (!calName.EndsWith(".png"))
				calName += ".png";

			return String.Format("{0}/{1}", ImagesPath, calName);
		}
			
		private UIImage LoadImage(String name)
		{
			var image = UIImage.FromBundle(name);

			if (image == null)
				image = UIImage.FromBundle(PathOfImage(name));
				
			return image;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Navigations the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="metrics">Metrics.</param>
		public virtual UIImage NavigationBackground(UIBarMetrics metrics)
		{
			var name = NavigationBarBackgroundName;

			if (metrics == UIBarMetrics.LandscapePhone) {
				name = NavigationBarLandscapeBackgroundName;
			}

			var image = LoadImage(name);

			if (image != null)
				image = image.CreateResizableImage(new UIEdgeInsets(10.0f, 8.0f, 10.0f, 8.0f));

            return image;

		}

		/// <summary>
		/// Navigations the background for I pad.
		/// </summary>
		/// <returns>The background for I pad.</returns>
		/// <param name="orientation">Orientation.</param>
		public virtual UIImage NavigationBackgroundForIPad(UIInterfaceOrientation orientation)
		{
			var name = NavigationBarRightBackgroundName;

			if (ThemeManager.IsLandscape(orientation))
			{
				name = NavigationBarRightLandscapeBackgroundName;
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0,8,0,8));
			return image;

		}

		/// <summary>
		/// Bars the button background.
		/// </summary>
		/// <returns>The button background.</returns>
		/// <param name="state">State.</param>
		/// <param name="style">Style.</param>
		/// <param name="barMetrics">Bar metrics.</param>
		public virtual UIImage BarButtonBackground(UIControlState state, UIBarButtonItemStyle style, UIBarMetrics barMetrics)
		{
			var name = @"barButton";

			if (style == UIBarButtonItemStyle.Done)
			{
				name += "Done";
			}

			if (barMetrics == UIBarMetrics.LandscapePhone)
			{
				name += "Landscape";
			}

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0,5,0,5));
			return image;

		}

		/// <summary>
		/// Backs the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="state">State.</param>
		/// <param name="barMetrics">Bar metrics.</param>
		public virtual UIImage BackBackground(UIControlState state, UIBarMetrics barMetrics)
		{
			var name = @"backButton";

			if (barMetrics == UIBarMetrics.LandscapePhone)
			{
				name += "Landscape";
			}

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 17.0f, 0.0f, 5.0f));
			return image;
		}

		/// <summary>
		/// Toolbars the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="metrics">Metrics.</param>
		public virtual UIImage ToolbarBackground(UIBarMetrics metrics)
		{
			var name = ToolbarBackgroundName;

			if (metrics == UIBarMetrics.LandscapePhone)
			{
				name = ToolbarLandscapeBackgroundName;
			}
				
			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 8.0f, 0.0f, 8.0f));
			return image;
		}

		/// <summary>
		/// Views the background for orientation.
		/// </summary>
		/// <returns>The background for orientation.</returns>
		/// <param name="orientation">Orientation.</param>
		public virtual UIImage ViewBackgroundForOrientation(UIInterfaceOrientation orientation)
		{
			var name = String.Empty;

			if (ThemeManager.IsPortrait(orientation))
			{
				name = (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) ? PhoneBackgroundName : PadBackgroundName;
			}
			else
			{
				name = (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) ? PhoneBackgroundLandscapeName : PadBackgroundLandscapeName;
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(UIEdgeInsets.Zero, UIImageResizingMode.Stretch);
			return image;
		}

		/// <summary>
		/// Buttons the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage ButtonBackground(UIControlState state)
		{
			var name = @"button";
	
			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}
			else if (state == UIControlState.Disabled)
			{
				name += "Disabled";
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 16.0f, 0.0f, 16.0f));
			return image;
		}

		/// <summary>
		/// Steppers the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage StepperBackground(UIControlState state)
		{
			var name = @"stepperBackground";

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}
			else if (state == UIControlState.Disabled)
			{
				name += "Disabled";
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 13.0f, 0.0f, 13.0f));
			return image;

		}

		/// <summary>
		/// Steppers the divider.
		/// </summary>
		/// <returns>The divider.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage StepperDivider(UIControlState state)
		{
			var name = @"stepperDivider";

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}

			return LoadImage(name);
		}

		/// <summary>
		/// Switchs the thumb.
		/// </summary>
		/// <returns>The thumb.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage SwitchThumb(UIControlState state)
		{
			var name = @"switchHandle";

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}

			return LoadImage(name);;
		}

		/// <summary>
		/// Sliders the thumb.
		/// </summary>
		/// <returns>The thumb.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage SliderThumb(UIControlState state)
		{
			var name = @"sliderThumb";

			if (state == UIControlState.Highlighted)
			{
				name += "Highlighted";
			}
			else if (state == UIControlState.Reserved)
			{
				name += "-small";
			}

			return LoadImage(name);
		}

		/// <summary>
		/// Segmenteds the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="state">State.</param>
		/// <param name="barMetrics">Bar metrics.</param>
		public virtual UIImage SegmentedBackground(UIControlState state, UIBarMetrics barMetrics)
		{
			return null;
		}

		/// <summary>
		/// Segmenteds the divider.
		/// </summary>
		/// <returns>The divider.</returns>
		/// <param name="barMetrics">Bar metrics.</param>
		public virtual UIImage SegmentedDivider(UIBarMetrics barMetrics)
		{
			return null;
		}

		/// <summary>
		/// Searchs the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="icon">Icon.</param>
		/// <param name="state">State.</param>
		public virtual UIImage SearchImage(UISearchBarIcon icon, UIControlState state)
		{
			var name = String.Empty;

			if (icon == UISearchBarIcon.Search)
			{
				name += "searchIconSearch";
			}
			else if (icon == UISearchBarIcon.Clear)
			{
				name += "searchIconClear";
				if (state == UIControlState.Highlighted)
				{
					name += "Highlighted";
				}
			}

			if (String.IsNullOrWhiteSpace(name)) 
				return null;

			return LoadImage(name);
		}

		/// <summary>
		/// Searchs the scope button background.
		/// </summary>
		/// <returns>The scope button background.</returns>
		/// <param name="state">State.</param>
		public virtual UIImage SearchScopeButtonBackground(UIControlState state)
		{
			var name = @"searchScopeButton";

			if (state == UIControlState.Selected)
			{
				name += "Selected";
			}

			var image = LoadImage(name);
			if (image == null)
				return null;
			image = image.CreateResizableImage(new UIEdgeInsets(6.0f, 6.0f, 6.0f, 6.0f));
			return image;
		}
						
		#endregion

		#region Properties

		#region Other
		/// <summary>
		/// Gets the status bar style.
		/// </summary>
		/// <value>The status bar style.</value>
		public virtual UIStatusBarStyle StatusBarStyle
		{
			get
			{
				return UIStatusBarStyle.LightContent;
			}
		}

		/// <summary>
		/// Gets the shadow offset.
		/// </summary>
		/// <value>The shadow offset.</value>
		public virtual CGSize ShadowOffset
		{
			get
			{
				return new CGSize(0, 0);
			}
		}

		#endregion

		#region Colors
		/// <summary>
		/// Gets the color of the main.
		/// </summary>
		/// <value>The color of the main.</value>
		public virtual UIColor MainColor
		{
			get
			{
				return UIColor.FromWhiteAlpha(0.3f, 1.0f);
			}
		}

		/// <summary>
		/// Gets the color of the second.
		/// </summary>
		/// <value>The color of the second.</value>
		public virtual UIColor SecondColor
		{
			get
			{
				return new UIColor(1.0f,1.0f,1.0f,1.0f);
			}
		}

		/// <summary>
		/// Gets the color of the navigation text.
		/// </summary>
		/// <value>The color of the navigation text.</value>
		public virtual UIColor NavigationTextColor
		{
			get
			{
				return new UIColor(1.0f,1.0f,1.0f,1.0f);
			}
		}

		/// <summary>
		/// Gets the color of the highlight.
		/// </summary>
		/// <value>The color of the highlight.</value>
		public virtual UIColor HighlightColor
		{
			get
			{
				return new UIColor(0.63f,0.71f,0.78f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the shadow.
		/// </summary>
		/// <value>The color of the shadow.</value>
		public virtual UIColor ShadowColor
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the color of the highlight shadow.
		/// </summary>
		/// <value>The color of the highlight shadow.</value>
		public virtual UIColor HighlightShadowColor
		{
			get
			{
				return new UIColor(0.16f,0.20f,0.38f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the navigation text shadow.
		/// </summary>
		/// <value>The color of the navigation text shadow.</value>
		public virtual UIColor NavigationTextShadowColor
		{
			get
			{
				return UIColor.White;
			}
		}

		/// <summary>
		/// Gets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public virtual UIColor BackgroundColor
		{
			get
			{
				return new UIColor(0.33f,0.34f,0.38f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the base tint.
		/// </summary>
		/// <value>The color of the base tint.</value>
		public virtual UIColor BaseTintColor
		{
			get
			{
				return UIColor.White;
			}
		}

		/// <summary>
		/// Gets the color of the accent tint.
		/// </summary>
		/// <value>The color of the accent tint.</value>
		public virtual UIColor AccentTintColor
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the color of the selected tabbar item tint.
		/// </summary>
		/// <value>The color of the selected tabbar item tint.</value>
		public virtual UIColor SelectedTabbarItemTintColor
		{
			get
			{
				return new UIColor(0.50f,0.84f,0.06f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the switch thumb.
		/// </summary>
		/// <value>The color of the switch thumb.</value>
		public virtual UIColor SwitchThumbColor
		{
			get
			{
				return new UIColor(0.87f,0.87f,0.89f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the switch on.
		/// </summary>
		/// <value>The color of the switch on.</value>
		public virtual UIColor SwitchOnColor
		{
			get
			{
				return new UIColor(0.16f,0.65f,0.51f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the switch tint.
		/// </summary>
		/// <value>The color of the switch tint.</value>
		public virtual UIColor SwitchTintColor
		{
			get
			{
				return new UIColor(0.86f,0.86f,0.86f,1.00f);
			}
		}

		/// <summary>
		/// Gets the color of the segmented tint.
		/// </summary>
		/// <value>The color of the segmented tint.</value>
		public virtual UIColor SegmentedTintColor
		{
			get
			{
				return UIColor.White;
			}
		}
		#endregion

		#region Fonts
		/// <summary>
		/// Gets the navigation font.
		/// </summary>
		/// <value>The navigation font.</value>
		public virtual UIFont NavigationFont
		{
			get
			{
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone
					? UIFont.FromName(@"HelveticaNeue-UltraLight",24.0f)
						: UIFont.FromName(@"HelveticaNeue-Light",26.0f);
			}
		}

		/// <summary>
		/// Gets the bar button font.
		/// </summary>
		/// <value>The bar button font.</value>
		public virtual UIFont BarButtonFont
		{
			get
			{
				return UIFont.FromName(@"HelveticaNeue-Light",15.0f);
			}
		}

		/// <summary>
		/// Gets the segment font.
		/// </summary>
		/// <value>The segment font.</value>
		public virtual UIFont SegmentFont
		{
			get
			{
				var aFont = UIFont.FromName(@"ProximaNova-Semibold",13.0f);

				if (aFont == null)
					aFont = UIFont.FromName(@"HelveticaNeue-Light",13.0f);
				
				if (aFont.PointSize < 1)
					return null;

				return aFont;
			}
		}

		/// <summary>
		/// Gets the label font.
		/// </summary>
		/// <value>The label font.</value>
		public virtual UIFont LabelFont
		{
			get
			{
				return UIFont.FromName(@"HelveticaNeue-Light",13.0f);
			}
		}
		#endregion

		#region Images
		/// <summary>
		/// Gets the top shadow.
		/// </summary>
		/// <value>The top shadow.</value>
		public virtual UIImage TopShadow
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the bottom shadow.
		/// </summary>
		/// <value>The bottom shadow.</value>
		public virtual UIImage BottomShadow
		{
			get
			{
				return LoadImage("bottomShadow");
			}
		}

		/// <summary>
		/// Gets the search background.
		/// </summary>
		/// <value>The search background.</value>
		public virtual UIImage SearchBackground
		{
			get
			{
				return LoadImage("searchBackround");
			}
		}

		/// <summary>
		/// Gets the search scope background.
		/// </summary>
		/// <value>The search scope background.</value>
		public virtual UIImage SearchScopeBackground
		{
			get
			{
				var serachbk = LoadImage("searchBackround");

				return serachbk ?? SearchBackground;
			}
		}

		/// <summary>
		/// Gets the search field image.
		/// </summary>
		/// <value>The search field image.</value>
		public virtual UIImage SearchFieldImage
		{
			get
			{
			
				var searchField = LoadImage("searchBackround");

				if (searchField == null) 
					return null;

				searchField = searchField.CreateResizableImage(new UIEdgeInsets(0.0f, 16.0f, 0.0f, 16.0f));

				return searchField;
			}
		}

		/// <summary>
		/// Gets the search scope button divider.
		/// </summary>
		/// <value>The search scope button divider.</value>
		public virtual UIImage SearchScopeButtonDivider
		{
			get
			{
				return LoadImage("searchScopeButtonDivider");
			}
		}

		/// <summary>
		/// Gets the table background.
		/// </summary>
		/// <value>The table background.</value>
		public virtual UIImage TableBackground
		{
			get
			{
				var fileName = (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) ? PhoneBackgroundName : PadBackgroundName;
				var image = LoadImage(fileName);

				if (image == null)
					return null;

				image = image.CreateResizableImage(UIEdgeInsets.Zero);

				return image;

			}
		}

		/// <summary>
		/// Gets the table section header background.
		/// </summary>
		/// <value>The table section header background.</value>
		public virtual UIImage TableSectionHeaderBackground
		{
			get
			{
				var image = LoadImage("list-section-header-bg");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(UIEdgeInsets.Zero);

				return image;
			}
		}

		/// <summary>
		/// Gets the table footer background.
		/// </summary>
		/// <value>The table footer background.</value>
		public virtual UIImage TableFooterBackground
		{
			get
			{
				var image = LoadImage("ist-footer-bg");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(UIEdgeInsets.Zero);

				return image;
			}
		}

		/// <summary>
		/// Gets the view background.
		/// </summary>
		/// <value>The view background.</value>
		public virtual UIImage ViewBackground
		{
			get
			{
				var fileName = (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) ? PhoneBackgroundName : PadBackgroundName;
				var image = LoadImage(fileName);

				if (image == null)
					return null;

				image = image.CreateResizableImage(UIEdgeInsets.Zero);

				return image;
			}
		}

		/// <summary>
		/// Gets the view background pattern.
		/// </summary>
		/// <value>The view background pattern.</value>
		public virtual UIImage ViewBackgroundPattern
		{
			get
			{
				var image = LoadImage("backgroundMenu");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(10, 10, 10, 10));

				return image;
			}
		}

		/// <summary>
		/// Gets the view background timeline.
		/// </summary>
		/// <value>The view background timeline.</value>
		public virtual UIImage ViewBackgroundTimeline
		{
			get
			{
				var image = LoadImage("background-timeline");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(10, 30, 10, 10));

				return image;
			}
		}

		/// <summary>
		/// Gets the switch on image.
		/// </summary>
		/// <value>The switch on image.</value>
		public virtual UIImage SwitchOnImage
		{
			get
			{
				var image = LoadImage("switchOnBackground");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0, 20, 0, 20));

				return image;
			}
		}

		/// <summary>
		/// Gets the switch off image.
		/// </summary>
		/// <value>The switch off image.</value>
		public virtual UIImage SwitchOffImage
		{
			get
			{
				var image = LoadImage("switchOffBackground");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(5, 20, 5, 20));

				return image;
			}
		}

		/// <summary>
		/// Gets the switch on icon.
		/// </summary>
		/// <value>The switch on icon.</value>
		public virtual UIImage SwitchOnIcon
		{
			get
			{
				return LoadImage("switchOnIcon");
			}
		}

		/// <summary>
		/// Gets the switch off icon.
		/// </summary>
		/// <value>The switch off icon.</value>
		public virtual UIImage SwitchOffIcon
		{
			get
			{
				return LoadImage("switchOffIcon");
			}
		}

		/// <summary>
		/// Gets the switch track.
		/// </summary>
		/// <value>The switch track.</value>
		public virtual UIImage SwitchTrack
		{
			get
			{
				return LoadImage("switchTrack");
			}
		}

		/// <summary>
		/// Gets the slider minimum track.
		/// </summary>
		/// <value>The slider minimum track.</value>
		public virtual UIImage SliderMinTrack
		{
			get
			{
				var image = LoadImage("sliderMinTrack");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the slider max track.
		/// </summary>
		/// <value>The slider max track.</value>
		public virtual UIImage SliderMaxTrack
		{
			get
			{
				var image = LoadImage("sliderMaxTrack");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the slider minimum track double.
		/// </summary>
		/// <value>The slider minimum track double.</value>
		public virtual UIImage SliderMinTrackDouble
		{
			get
			{
				var image = LoadImage("sliderMinTrack");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the slider max track double.
		/// </summary>
		/// <value>The slider max track double.</value>
		public virtual UIImage SliderMaxTrackDouble
		{
			get
			{
				var image = LoadImage("sliderMaxTrack");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the progress track image.
		/// </summary>
		/// <value>The progress track image.</value>
		public virtual UIImage ProgressTrackImage
		{
			get
			{
				return LoadImage("progress-segmented-track");
			}
		}

		/// <summary>
		/// Gets the progress progress image.
		/// </summary>
		/// <value>The progress progress image.</value>
		public virtual UIImage ProgressProgressImage
		{
			get
			{
				var image = LoadImage("progress-segmented-fill");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0, 10, 0, 10));

				return image;
			}
		}

		/// <summary>
		/// Gets the progress percent track image.
		/// </summary>
		/// <value>The progress percent track image.</value>
		public virtual UIImage ProgressPercentTrackImage
		{
			get
			{
				var image = LoadImage("progressTrack");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the progress percent progress image.
		/// </summary>
		/// <value>The progress percent progress image.</value>
		public virtual UIImage ProgressPercentProgressImage
		{
			get
			{
				var image = LoadImage("progressProgress");

				if (image == null) 
					return null;

				image = image.CreateResizableImage(new UIEdgeInsets(0.0f, 5f, 0.0f, 5f));

				return image;
			}
		}

		/// <summary>
		/// Gets the progress percent progress value image.
		/// </summary>
		/// <value>The progress percent progress value image.</value>
		public virtual UIImage ProgressPercentProgressValueImage
		{
			get
			{
				return LoadImage("progressValue");
			}
		}

		/// <summary>
		/// Gets the stepper increment image.
		/// </summary>
		/// <value>The stepper increment image.</value>
		public virtual UIImage StepperIncrementImage
		{
			get
			{
				return LoadImage("stepperIncrement");
			}
		}

		/// <summary>
		/// Gets the stepper decrement image.
		/// </summary>
		/// <value>The stepper decrement image.</value>
		public virtual UIImage StepperDecrementImage
		{
			get
			{
				return LoadImage("stepperDecrement");
			}
		}

		/// <summary>
		/// Gets the tab bar background.
		/// </summary>
		/// <value>The tab bar background.</value>
		public virtual UIImage TabBarBackground
		{
			get
			{
				var bottomImage = ViewBackground;

				var image = LoadImage(TabbarBackgroundName);
				image = image.CreateResizableImage(new UIEdgeInsets(10, 10, 10, 10));

				var newSize = new CGSize(320,45);
				UIGraphics.BeginImageContext(newSize);

				var window = UIApplication.SharedApplication.KeyWindow;

				if (window == null)
					window = UIApplication.SharedApplication.Windows[0];

				nfloat padding;

				if (window.RootViewController.InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft 
					|| window.RootViewController.InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
				{

					padding = bottomImage.Size.Height - (bottomImage.Size.Height - window.Frame.Right + 45);
				}
				else
				{
					padding = bottomImage.Size.Height - (window.Frame.Bottom - bottomImage.Size.Height + 45);
				}

				bottomImage.Draw(new CGRect(0,-padding,newSize.Width, bottomImage.Size.Height));

				image.Draw(new CGRect(0,0,newSize.Width,newSize.Height), CGBlendMode.Normal, 1.0f);

				var newImage = UIGraphics.GetImageFromCurrentImageContext();

				UIGraphics.EndImageContext();

				return newImage;
			}
		}

		/// <summary>
		/// Gets the tab bar selection indicator.
		/// </summary>
		/// <value>The tab bar selection indicator.</value>
		public virtual UIImage TabBarSelectionIndicator
		{
			get
			{
				return LoadImage("tabBarSelectionIndicator");
			}
		}

		/// <summary>
		/// Gets the button image.
		/// </summary>
		/// <value>The button image.</value>
		public virtual UIImage ButtonImage 
		{
			get
			{
				return LoadImage("button");
			}
		}

		/// <summary>
		/// Gets the buttion pressed image.
		/// </summary>
		/// <value>The buttion pressed image.</value>
		public virtual UIImage ButtonPressedImage 
		{
			get
			{
				return LoadImage("button-pressed");
			}
		}

		/// <summary>
		/// Gets the text field background.
		/// </summary>
		/// <value>The text field background.</value>
		public virtual UIImage TextFieldBackground
		{
			get
			{
				var aImage  = LoadImage("input");

				return aImage.StretchableImage(20,20);
			}
		}
		#endregion

		#region Image strings

		/// <summary>
		/// Gets the background image name for Phone Portrait.
		/// </summary>
		/// <value>The phone background.</value>
		public virtual string PhoneBackgroundName 
		{
			get
			{
				return "background-568h";
			}
		}

		/// <summary>
		/// Gets the background image name for Phone Landscape.
		/// </summary>
		/// <value>The phone background landscape.</value>
		public virtual string PhoneBackgroundLandscapeName
		{
			get
			{
				return "backgroundLandscape-568h";
			}
		}

		/// <summary>
		/// Gets the background image name for iPad Portait.
		/// </summary>
		/// <value>The pad background.</value>
		public virtual string PadBackgroundName
		{
			get
			{
				return "background";
			}
		}

		/// <summary>
		/// Gets the background image name for iPad Landscape.
		/// </summary>
		/// <value>The pad background landscape.</value>
		public virtual string PadBackgroundLandscapeName
		{
			get
			{
				return "backgroundLandscape";
			}
		}

		/// <summary>
		/// Gets the name of the toolbar background.
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		public virtual string ToolbarBackgroundName
		{
			get
			{
				return @"toolbarBackground";
			}
		}

		/// <summary>
		/// Gets the name of the toolbar landscape.
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		public virtual string ToolbarLandscapeBackgroundName
		{
			get
			{
				return @"toolbarBackgroundLandscape";
			}
		}

		/// <summary>
		/// Gets the name of the Navigation Bar background in portrait
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		public virtual string NavigationBarBackgroundName
		{
			get
			{
				return @"navigationBackground";
			}
		}

		/// <summary>
		/// Gets the name of the Navigation Bar background in Landscape
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		public virtual string NavigationBarLandscapeBackgroundName
		{
			get
			{
				return @"navigationBackgroundLandscape";
			}
		}

		/// <summary>
		/// Gets the name of the right Navigation Bar background in portrait
		/// </summary>
		/// <value>The name of the toolbar background.</value>
		public virtual string NavigationBarRightBackgroundName
		{
			get
			{
				return @"navigationBackgroundRight";
			}
		}

		/// <summary>
		/// Gets the name of the right Navigation Bar background in landscape
		/// </summary>
		/// <value>The name of the toolbar landscape.</value>
		public virtual string NavigationBarRightLandscapeBackgroundName
		{
			get
			{
				return @"navigationBackgroundRightLandscape";
			}
		}

		/// <summary>
		/// Gets the name of the tabbar background.
		/// </summary>
		/// <value>The name of the tabbar background.</value>
		public virtual string TabbarBackgroundName
		{
			get
			{
				return @"tabBarBackground";
			}
		}

		#endregion
		#endregion

		#region Private

		/// <summary>
		/// Gets the title text attributes for the Navigation Bar
		/// </summary>
		/// <value>The title text attribs.</value>
		private UITextAttributes TitleTextAttribsNav
		{
			get
			{
				var titleTextAttributesNav = new UITextAttributes();

				var navTextColor = NavigationTextColor;

				if (navTextColor != null)
					titleTextAttributesNav.TextColor = navTextColor;

				var navTextShadowColor = NavigationTextShadowColor;
				if (navTextShadowColor != null)
				{
					titleTextAttributesNav.TextShadowColor =  navTextShadowColor;
					titleTextAttributesNav.TextShadowOffset = new UIOffset(ShadowOffset.Width, ShadowOffset.Height);
				}

				var navTextFont = NavigationFont;
				if (navTextFont != null)
				{
					titleTextAttributesNav.Font = navTextFont;
				}

				return titleTextAttributesNav;
			}
		}

		/// <summary>
		/// Gets the title text attributes for the UIBarButton.
		/// </summary>
		/// <value>The title text attribs bar button.</value>
		private UITextAttributes TitleTextAttribsBarButton
		{
			get
			{
				var titleTextAttributesBarButton = TitleTextAttribsNav;

				UIFont barButtonTextFont = BarButtonFont;
				if (barButtonTextFont != null) {
					titleTextAttributesBarButton.Font = barButtonTextFont;
				}

				return titleTextAttributesBarButton;
			}
		}

		/// <summary>
		/// Gets the title text attributes
		/// </summary>
		/// <value>The title text attribs.</value>
		private UITextAttributes TitleTextAttribs
		{
			get
			{
				var titleTextAttributes = new UITextAttributes();

				var mainColor = MainColor;
				if (mainColor != null) 
				{
					titleTextAttributes.TextColor = mainColor;
				}

				var segmentFont = SegmentFont;
				if (segmentFont != null) 
				{
					titleTextAttributes.Font = segmentFont;
				}

				var secondColor = SecondColor;
				if (secondColor != null) 
				{
					titleTextAttributes.TextColor = secondColor;
				}

				var shadowColor = ShadowColor;
				if (shadowColor != null)
				{
					titleTextAttributes.TextShadowColor =  shadowColor;
					titleTextAttributes.TextShadowOffset = new UIOffset(ShadowOffset.Width, ShadowOffset.Height);
				}

				return titleTextAttributes;
			}
		}

		/// <summary>
		/// Gets the title text attributes for hightlight state
		/// </summary>
		/// <value>The title text attribs highlighted.</value>
		private UITextAttributes TitleTextAttribsHighlighted
		{
			get
			{
				var titleTextAttributesH = new UITextAttributes();

				var highlightShadowColor = HighlightShadowColor;
				if (highlightShadowColor != null)
				{
					titleTextAttributesH.TextShadowColor =  highlightShadowColor;
					titleTextAttributesH.TextShadowOffset = new UIOffset(ShadowOffset.Width, ShadowOffset.Height);
				}

				var highlightColor = HighlightColor;
				if (highlightColor != null)
				{
					titleTextAttributesH.TextColor = highlightColor;
				}

				return titleTextAttributesH;
			}
		}
		#endregion

		#region ITheme Methods
		/// <summary>
		/// Apply theme to the whole application
		/// </summary>
		public virtual ITheme Apply()
		{
			UIApplication.SharedApplication.SetStatusBarStyle(this.StatusBarStyle, false);

			Apply(UINavigationBar.Appearance);
			Apply(UIBarButtonItem.Appearance);
			Apply(UIToolbar.Appearance);
			Apply(UISlider.Appearance);
			Apply(UISegmentedControl.Appearance);
			Apply(UIProgressView.Appearance);
			Apply(UITabBar.Appearance);
			Apply(UISearchBar.Appearance);
			Apply(UISwitch.Appearance);
			Apply(UITextField.Appearance);
			//Apply(UIButton.Appearance);
			Apply(UILabel.Appearance);

			return this;

		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UIView view, UIInterfaceOrientation orientation = UIInterfaceOrientation.Portrait)
		{
			var backgroundImage = ViewBackgroundForOrientation(orientation);

			var backgroundColor = BackgroundColor;

			if (backgroundImage != null)
			{
				backgroundImage = backgroundImage.Scale(view.Frame.Size);

				view.BackgroundColor = UIColor.FromPatternImage(backgroundImage);
			}
			else if (backgroundColor == null)
			{

				view.BackgroundColor = backgroundColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		public virtual ITheme Apply (UINavigationBar view)
		{
			view.SetBackgroundImage(NavigationBackground(UIBarMetrics.Default), UIBarMetrics.Default);
			view.SetBackgroundImage(NavigationBackground(UIBarMetrics.LandscapePhone), UIBarMetrics.LandscapePhone);

			var baseTintColor = BaseTintColor;

			if (baseTintColor != null)
			{
				view.TintColor = baseTintColor;
			}
				
			view.TitleTextAttributes = GetStringAttributes (TitleTextAttribsNav);

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UINavigationBar.UINavigationBarAppearance appearance)
		{
            
			appearance.SetBackgroundImage(NavigationBackground(UIBarMetrics.Default), UIBarMetrics.Default);
			appearance.SetBackgroundImage(NavigationBackground(UIBarMetrics.LandscapePhone), UIBarMetrics.LandscapePhone);

			appearance.SetTitleTextAttributes(TitleTextAttribsNav);

			var baseTintColor = BaseTintColor;
			if (baseTintColor != null)
			{
				appearance.TintColor = baseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UIBarButtonItem view)
		{
			view.SetTitleTextAttributes(TitleTextAttribsBarButton, UIControlState.Normal);
			view.SetTitleTextAttributes(TitleTextAttribsHighlighted, UIControlState.Highlighted);

			var baseTintColor = BaseTintColor;
			if (baseTintColor != null)
			{
				view.TintColor = baseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UIBarButtonItem.UIBarButtonItemAppearance appearance)
		{
			appearance.SetTitleTextAttributes(TitleTextAttribsBarButton, UIControlState.Normal);
			appearance.SetTitleTextAttributes(TitleTextAttribsHighlighted, UIControlState.Highlighted);

			return this;

		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UIToolbar view)
		{
			view.SetBackgroundImage(ToolbarBackground(UIBarMetrics.Default), UIToolbarPosition.Any, UIBarMetrics.Default);
			view.SetBackgroundImage(ToolbarBackground(UIBarMetrics.LandscapePhone), UIToolbarPosition.Any, UIBarMetrics.LandscapePhone);

			if (BaseTintColor != null)
			{
				view.TintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UIToolbar.UIToolbarAppearance appearance)
		{
			appearance.SetBackgroundImage(ToolbarBackground(UIBarMetrics.Default), UIToolbarPosition.Any, UIBarMetrics.Default);
			appearance.SetBackgroundImage(ToolbarBackground(UIBarMetrics.LandscapePhone), UIToolbarPosition.Any, UIBarMetrics.LandscapePhone);

			if (BaseTintColor != null)
			{
				appearance.TintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UISlider view)
		{
			view.SetThumbImage(SliderThumb(UIControlState.Normal), UIControlState.Normal);
			view.SetThumbImage(SliderThumb(UIControlState.Highlighted), UIControlState.Highlighted);
			view.SetMinTrackImage(SliderMinTrack,UIControlState.Normal);
			view.SetMaxTrackImage(SliderMaxTrack,UIControlState.Normal);

			if (BaseTintColor != null)
			{
				view.ThumbTintColor = BaseTintColor;
				view.MaximumTrackTintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UISlider.UISliderAppearance appearance)
		{
			appearance.SetThumbImage(SliderThumb(UIControlState.Normal), UIControlState.Normal);
			appearance.SetThumbImage(SliderThumb(UIControlState.Highlighted), UIControlState.Highlighted);
			appearance.SetMinTrackImage(SliderMinTrack,UIControlState.Normal);
			appearance.SetMaxTrackImage(SliderMaxTrack,UIControlState.Normal);

			if (BaseTintColor != null)
			{
				appearance.ThumbTintColor = BaseTintColor;
				appearance.MaximumTrackTintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UILabel view)
		{
			view.BackgroundColor = UIColor.Clear;
			view.Font = LabelFont;

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UILabel.UILabelAppearance appearance)
		{
			appearance.BackgroundColor = UIColor.Clear;
			appearance.Font = LabelFont;

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UISegmentedControl view)
		{
			view.SetBackgroundImage(SegmentedBackground(UIControlState.Normal,UIBarMetrics.Default),UIControlState.Normal,UIBarMetrics.Default);
			view.SetBackgroundImage(SegmentedBackground(UIControlState.Selected,UIBarMetrics.Default),UIControlState.Selected,UIBarMetrics.Default);
			view.SetBackgroundImage(SegmentedBackground(UIControlState.Normal,UIBarMetrics.LandscapePhone),UIControlState.Normal,UIBarMetrics.LandscapePhone);
			view.SetBackgroundImage(SegmentedBackground(UIControlState.Selected,UIBarMetrics.LandscapePhone),UIControlState.Selected,UIBarMetrics.LandscapePhone);

			view.SetDividerImage(SegmentedDivider(UIBarMetrics.Default), UIControlState.Normal, UIControlState.Normal, UIBarMetrics.Default);
			view.SetDividerImage(SegmentedDivider(UIBarMetrics.LandscapePhone), UIControlState.Normal, UIControlState.Normal, UIBarMetrics.LandscapePhone);

			view.SetTitleTextAttributes(TitleTextAttribs, UIControlState.Normal);

			if (BaseTintColor != null)
			{
				view.TintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UISegmentedControl.UISegmentedControlAppearance appearance)
		{
			appearance.SetBackgroundImage(SegmentedBackground(UIControlState.Normal,UIBarMetrics.Default),UIControlState.Normal,UIBarMetrics.Default);
			appearance.SetBackgroundImage(SegmentedBackground(UIControlState.Selected,UIBarMetrics.Default),UIControlState.Selected,UIBarMetrics.Default);
			appearance.SetBackgroundImage(SegmentedBackground(UIControlState.Normal,UIBarMetrics.LandscapePhone),UIControlState.Normal,UIBarMetrics.LandscapePhone);
			appearance.SetBackgroundImage(SegmentedBackground(UIControlState.Selected,UIBarMetrics.LandscapePhone),UIControlState.Selected,UIBarMetrics.LandscapePhone);

			appearance.SetDividerImage(SegmentedDivider(UIBarMetrics.Default), UIControlState.Normal, UIControlState.Normal, UIBarMetrics.Default);
			appearance.SetDividerImage(SegmentedDivider(UIBarMetrics.LandscapePhone), UIControlState.Normal, UIControlState.Normal, UIBarMetrics.LandscapePhone);


			//appearance.SetTitleTextAttributes(titleTextAttributesH, UIControlState.Highlighted);
			//appearance.SetTitleTextAttributes(titleTextAttributesH, UIControlState.Selected);

			appearance.SetTitleTextAttributes(TitleTextAttribs, UIControlState.Normal);

			if (BaseTintColor != null)
			{
				appearance.TintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UITextField view)
		{
			view.TintColor = HighlightColor;
			view.BackgroundColor = UIColor.FromWhiteAlpha(1.0f, 0.1f);
			view.BorderStyle = UITextBorderStyle.None;
			var anImage =  TextFieldBackground;
			view.Background = anImage;

			var aView = new UIView(new CGRect(0,0,7f,35));
			aView.BackgroundColor = UIColor.Clear;
			view.LeftView = aView;
			view.LeftViewMode = UITextFieldViewMode.Always;

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UITextField.UITextFieldAppearance appearance)
		{
			appearance.TintColor = HighlightColor;
			//appearance.BackgroundColor = UIColor.FromWhiteAlpha(1.0f, 0.1f);
	
			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="tableview">Tableview.</param>
		public virtual ITheme Apply (UITableView tableview)
		{

			var backgroundImage = TableBackground;
			var backgroundColor = BackgroundColor;

			if (backgroundImage != null) 
			{
				var background = new UIImageView(backgroundImage);

			    tableview.BackgroundView = background;
			} 
		    else if (backgroundColor != null) 
		    {
				tableview.BackgroundView = null;
				tableview.BackgroundColor = backgroundColor;
			}

			return this;
		}
			
		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public virtual ITheme Apply (UIProgressView view)
		{
			view.TrackImage = ProgressTrackImage;
			view.ProgressImage = ProgressProgressImage;
			//view.TrackTintColor = BaseTintColor;

			if (HighlightColor != null)
			{
				view.TintColor = HighlightColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply (UIProgressView.UIProgressViewAppearance appearance)
		{
			appearance.TrackImage = ProgressTrackImage;
			appearance.ProgressImage = ProgressProgressImage;
			//appearance.TrackTintColor = BaseTintColor;

			if (HighlightColor != null)
			{
				appearance.TintColor = HighlightColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="view">View.</param>
		public ITheme Apply(UITabBar view)
		{
			view.BackgroundImage = TabBarBackground;
			view.SelectionIndicatorImage = TabBarSelectionIndicator;

			if (SelectedTabbarItemTintColor != null)
			{
				view.SelectedImageTintColor = SelectedTabbarItemTintColor;
			}

			if (BaseTintColor != null)
			{

				view.TintColor = BaseTintColor;
			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UITabBar.UITabBarAppearance appearance)
		{
			appearance.BackgroundImage = TabBarBackground;
			appearance.SelectionIndicatorImage = TabBarSelectionIndicator;

			if (SelectedTabbarItemTintColor != null)
			{
				appearance.SelectedImageTintColor = SelectedTabbarItemTintColor;
			}

			if (BaseTintColor != null)
			{

				appearance.TintColor = BaseTintColor;
			}

			return this;
		}
			
		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UISearchBar view)
		{
			view.BackgroundImage = SearchBackground;
			view.SetSearchFieldBackgroundImage(SearchFieldImage, UIControlState.Normal);
			view.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			view.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			view.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			view.ScopeBarBackgroundImage = SearchScopeBackground;
			view.SetScopeBarButtonBackgroundImage(SearchScopeButtonBackground(UIControlState.Normal),UIControlState.Normal);
			view.SetScopeBarButtonBackgroundImage(SearchScopeButtonBackground(UIControlState.Selected),UIControlState.Selected);
			view.SetScopeBarButtonDividerImage(SearchScopeButtonDivider,UIControlState.Normal,UIControlState.Normal);

			view.SetScopeBarButtonTitle(TitleTextAttribsBarButton, UIControlState.Normal);
			view.SetScopeBarButtonTitle(TitleTextAttribsHighlighted, UIControlState.Highlighted);

			if (HighlightColor != null)
			{
				view.TintColor = HighlightColor;

			}

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UISearchBar.UISearchBarAppearance appearance)
		{
			appearance.BackgroundImage = SearchBackground;
			appearance.SetSearchFieldBackgroundImage(SearchFieldImage, UIControlState.Normal);
			appearance.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			appearance.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			appearance.SetImageforSearchBarIcon(SearchImage(UISearchBarIcon.Search,UIControlState.Normal),UISearchBarIcon.Search, UIControlState.Normal);
			appearance.ScopeBarBackgroundImage = SearchScopeBackground;
			appearance.SetScopeBarButtonBackgroundImage(SearchScopeButtonBackground(UIControlState.Normal),UIControlState.Normal);
			appearance.SetScopeBarButtonBackgroundImage(SearchScopeButtonBackground(UIControlState.Selected),UIControlState.Selected);
			appearance.SetScopeBarButtonDividerImage(SearchScopeButtonDivider,UIControlState.Normal,UIControlState.Normal);

			appearance.SetScopeBarButtonTitle(TitleTextAttribsBarButton, UIControlState.Normal);
			appearance.SetScopeBarButtonTitle(TitleTextAttribsHighlighted, UIControlState.Highlighted);

			if (HighlightColor != null)
			{
				appearance.TintColor = HighlightColor;

			}

			return this;
		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UISwitch view)
		{
			view.TintColor = BaseTintColor;

			return this;
		}

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UISwitch.UISwitchAppearance appearance)
		{
			appearance.TintColor = BaseTintColor;
			appearance.OnImage = SwitchOnIcon;

			return this;
		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UIButton view)
		{
			view.SetBackgroundImage(ButtonImage, UIControlState.Normal);
			view.SetBackgroundImage(ButtonPressedImage, UIControlState.Highlighted);

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UIButton.UIButtonAppearance appearance)
		{
			appearance.SetBackgroundImage(ButtonImage, UIControlState.Normal);
			appearance.SetBackgroundImage(ButtonPressedImage, UIControlState.Highlighted);

			return this;

		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		public virtual ITheme Apply(UIRefreshControl view)
		{
			return this;
		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="cell">Cell.</param>
		public virtual ITheme Apply(UITableViewCell cell)
		{
			return this;
		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UIRefreshControl.UIRefreshControlAppearance appearance)
		{
			return this;
		}

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		/// <param name="appearance">Appearance.</param>
		public virtual ITheme Apply(UITableView.UITableViewAppearance appearance)
		{

			return this;
		}

		#region ViewController Methods

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="viewController">View controller.</param>
		public ITheme Apply (UITableViewController viewController)
		{
			Apply(viewController.TableView);

			return this;
		}

		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		/// <param name="viewController">View controller.</param>
		public ITheme Apply (UIViewController viewController, UIInterfaceOrientation orientation = UIInterfaceOrientation.Portrait)
		{
			Apply(viewController.View, orientation);

			return this;
		}
		#endregion
        #endregion 

        public static UIStringAttributes GetStringAttributes (UITextAttributes attr)
        {
            return new UIStringAttributes {
                Font = attr.Font,
                ForegroundColor = attr.TextColor,
                Shadow = new NSShadow {
                    ShadowColor = attr.TextShadowColor,
                    ShadowOffset = new CGSize (attr.TextShadowOffset.Horizontal, attr.TextShadowOffset.Vertical)
                }
            };
        }
	}
}

