using System;

using Foundation;
using UIKit;

using MaterialComponents;

namespace MaterialSample {
	public partial class MenuCollectionViewCell : UICollectionViewCell {

		#region Cell Identifier

		public static readonly NSString Key = new NSString (nameof (MenuCollectionViewCell));

		#endregion

		#region Class Variables

		UILabel lblSample;

		#endregion

		#region Properties

		public string SampleTitle {
			get => lblSample.Text;
			set => lblSample.Text = value;
		}

		#endregion

		#region Constructors

		protected MenuCollectionViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		#endregion

		#region Cell Life Cycle

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			lblSample = new UILabel {
				Font = Typography.ButtonFont.WithSize (20),
				MinimumScaleFactor = 0.5f,
				TranslatesAutoresizingMaskIntoConstraints = false
			};
			lblSample.SizeToFit ();

			ContentView.AddSubview (lblSample);

			AddConstraints ();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			lblSample.SizeToFit ();
		}

		public override void PrepareForReuse ()
		{
			base.PrepareForReuse ();
			lblSample.Text = "";
		}

		#endregion

		#region Internal Functionality

		void AddConstraints ()
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				lblSample.CenterXAnchor.ConstraintEqualTo (ContentView.CenterXAnchor).Active = true;
				lblSample.CenterYAnchor.ConstraintEqualTo (ContentView.CenterYAnchor).Active = true;
			} else {
				var centerXAnchor = NSLayoutConstraint.Create (lblSample, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterX, 1, 0);
				var centerYAnchor = NSLayoutConstraint.Create (lblSample, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, ContentView, NSLayoutAttribute.CenterY, 1, 0);
				NSLayoutConstraint.ActivateConstraints (new [] { centerXAnchor, centerYAnchor });
			}
		}

		#endregion
	}
}
