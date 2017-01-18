using System;
using System.Threading.Tasks;

#if __UNIFIED__
using CoreGraphics;
using UIKit;
#else
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
	/// <summary>
	/// The view controller that represents a color picker.
	/// </summary>
	public class ColorPickerViewController : UIViewController
	{
		private ColorPickerView colorPicker;

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerViewController"/> class.
		/// </summary>
		public ColorPickerViewController()
		{
			Initialize(UIColor.FromHSB(0.5984375f, 0.5f, 0.7482993f));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerViewController"/> class 
		/// with the specified initial selected hue, saturation and brightness.
		/// </summary>
		/// <param name="hue">The initial selected hue.</param>
		/// <param name="saturation">The initial selected saturation.</param>
		/// <param name="brightness">The initial selected brightness.</param>
		public ColorPickerViewController(nfloat hue, nfloat saturation, nfloat brightness)
		{
			Initialize(UIColor.FromHSB(hue, saturation, brightness));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerViewController"/> class 
		/// with the specified initial selected color.
		/// </summary>
		/// <param name="color">The initial selected color.</param>
		public ColorPickerViewController(UIColor color)
		{
			Initialize(color);
		}

		/// <summary>
		/// Initializes this instance with the specified color.
		/// </summary>
		/// <param name="color">The initial selected color.</param>
		protected void Initialize(UIColor color)
		{
			colorPicker = new ColorPickerView(color);
		}

		/// <summary>
		/// Gets or sets the selected color.
		/// </summary>
		/// <value>The selected color.</value>
		public UIColor SelectedColor
		{
			get { return colorPicker.SelectedColor; }
			set { colorPicker.SelectedColor = value; }
		}

		/// <summary>
		/// Occurs when the a color is selected.
		/// </summary>
		public event EventHandler<ColorPickedEventArgs> ColorPicked
		{
			add { colorPicker.ColorPicked += value; }
			remove { colorPicker.ColorPicked -= value; }
		}

		/// <summary>
		/// Initializes the <see cref="UIViewController.View"/> property.
		/// </summary>
		public override void LoadView()
		{
			View = colorPicker;
		}

		/// <summary>
		/// The orientations supported by this <see cref="UIViewController"/>.
		/// </summary>
		/// <returns>A <see cref="UIInterfaceOrientationMask"/> of the orientations supported by this <see cref="UIViewController"/>.</returns>
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.All;
		}

		/// <summary>
		/// If the <see cref="UIViewController"/> supports rotation to the specified <see cref="UIInterfaceOrientation"/>.
		/// </summary>
		/// <param name="toInterfaceOrientation">The final <see cref="UIInterfaceOrientation"/>.</param>
		/// <returns><c>true</c> if the <see cref="UIViewController"/> supports rotation to the specified <see cref="UIInterfaceOrientation"/>, <c>false</c> otherwise.</returns>
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}

		/// <summary>
		/// Turns auto-rotation on or off.
		/// </summary>
		/// <returns><c>true</c> if the MonoTouch.UIKit.UIViewController should auto-rotate, <c>false</c> otherwise.</returns>
		public override bool ShouldAutorotate()
		{
			return true;
		}

		/// <summary>
		/// Presents a new color picker.
		/// </summary>
		/// <param name="parent">The parent <see cref="UIViewController"/>.</param>
		/// <param name="title">The picker title.</param>
		/// <param name="initialColor">The initial selected color.</param>
		/// <param name="done">The method invoked when the picker closes.</param>
		public static void Present(UIViewController parent, string title, UIColor initialColor, Action<UIColor> done)
		{
			Present(parent, title, initialColor, done, null);
		}

		/// <summary>
		/// Presents a new color picker.
		/// </summary>
		/// <param name="parent">The parent <see cref="UIViewController"/>.</param>
		/// <param name="title">The picker title.</param>
		/// <param name="initialColor">The initial selected color.</param>
		/// <param name="done">The method invoked when the picker closes.</param>
		/// <param name="colorPicked">The method invoked as colors are picked.</param>
		public static void Present(UIViewController parent, string title, UIColor initialColor, Action<UIColor> done, Action<UIColor> colorPicked)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}

			var picker = new ColorPickerViewController
			{
				Title = title,
				SelectedColor = initialColor
			};
			picker.ColorPicked += (_, args) =>
			{
				if (colorPicked != null)
				{
					colorPicked(args.SelectedColor);
				}
			};

			var pickerNav = new UINavigationController(picker);
			pickerNav.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
			pickerNav.NavigationBar.Translucent = false;

			var doneBtn = new UIBarButtonItem(UIBarButtonSystemItem.Done);
			picker.NavigationItem.RightBarButtonItem = doneBtn;
			doneBtn.Clicked += delegate
			{
				if (done != null)
				{
					done(picker.SelectedColor);
				}

				// hide the picker
				parent.DismissViewController(true, null);
			};

			// show the picker
			parent.PresentViewController(pickerNav, true, null);
		}

		/// <summary>
		/// Presents a new color picker, and waits for the picker to close.
		/// </summary>
		/// <param name="parent">The parent <see cref="UIViewController"/>.</param>
		/// <param name="title">The picker title.</param>
		/// <param name="initialColor">The initial selected color.</param>
		public static Task<UIColor> PresentAsync(UIViewController parent, string title, UIColor initialColor)
		{
			return PresentAsync(parent, title, initialColor, null);
		}

		/// <summary>
		/// Presents a new color picker, and waits for the picker to close.
		/// </summary>
		/// <param name="parent">The parent <see cref="UIViewController"/>.</param>
		/// <param name="title">The picker title.</param>
		/// <param name="initialColor">The initial selected color.</param>
		/// <param name="colorPicked">The method invoked as colors are picked.</param>
		public static Task<UIColor> PresentAsync(UIViewController parent, string title, UIColor initialColor, Action<UIColor> colorPicked)
		{
			var tcs = new TaskCompletionSource<UIColor>();
			Present(parent, title, initialColor, color =>
			{
				tcs.SetResult(color);
			}, colorPicked);
			return tcs.Task;
		}
	}
}
