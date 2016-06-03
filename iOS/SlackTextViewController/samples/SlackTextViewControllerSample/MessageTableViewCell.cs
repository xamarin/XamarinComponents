using System;
using Foundation;
using UIKit;

using SlackHQ;

namespace SlackTextViewControllerSample
{
	public class MessageTableViewCell : UITableViewCell
	{
		public static nfloat AvatarHeight = 30.0f;

		public static nfloat MinimumHeight = 50.0f;

		public static nfloat DefaultFontSize {
			get {
				nfloat pointSize = 16.0f;
				var contentSizeCategory = UIApplication.SharedApplication.PreferredContentSizeCategory;
				pointSize += SlackTextView.SlackPointSizeDifference (contentSizeCategory);
				return pointSize;
			}
		}

		protected MessageTableViewCell (IntPtr handle)
			: base(handle)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;
			BackgroundColor = UIColor.White;


			TitleLabel = new UILabel ();
			TitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			TitleLabel.BackgroundColor = UIColor.Clear;
			TitleLabel.UserInteractionEnabled = false;
			TitleLabel.Lines = 0;
			TitleLabel.TextColor = UIColor.Gray;
			TitleLabel.Font = UIFont.BoldSystemFontOfSize (DefaultFontSize);

			BodyLabel = new UILabel ();
			BodyLabel.TranslatesAutoresizingMaskIntoConstraints = false;
			BodyLabel.BackgroundColor = UIColor.Clear;
			BodyLabel.UserInteractionEnabled = false;
			BodyLabel.Lines = 0;
			BodyLabel.TextColor = UIColor.DarkGray;
			BodyLabel.Font = UIFont.SystemFontOfSize (DefaultFontSize);

			ThumbnailView = new UIImageView ();
			ThumbnailView.TranslatesAutoresizingMaskIntoConstraints = false;
			ThumbnailView.UserInteractionEnabled = false;
			ThumbnailView.BackgroundColor = UIColor.FromWhiteAlpha (0.9f, 1.0f);
			ThumbnailView.Layer.CornerRadius = AvatarHeight / 2.0f;
			ThumbnailView.Layer.MasksToBounds = true;


			ContentView.AddSubview (ThumbnailView);
			ContentView.AddSubview (TitleLabel);
			ContentView.AddSubview (BodyLabel);

			var views = NSDictionary.FromObjectsAndKeys (new NSObject[] {
				ThumbnailView,
				TitleLabel,
				BodyLabel
			}, new NSObject[] {
				(NSString)"thumbnailView", 
				(NSString)"titleLabel",
				(NSString)"bodyLabel"
			});

			var metrics = NSDictionary.FromObjectsAndKeys (new [] {
				NSNumber.FromNFloat (AvatarHeight),
				NSNumber.FromNFloat (15f),
				NSNumber.FromNFloat (10f),
				NSNumber.FromNFloat (5f),
				NSNumber.FromNFloat (80f)
			}, new [] {
				"thumbSize",
				"padding",
				"right",
				"left",
				"attchSize"
			});

			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-left-[thumbnailView(thumbSize)]-right-[titleLabel(>=0)]-right-|", 0, metrics, views));
			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("H:|-left-[thumbnailView(thumbSize)]-right-[bodyLabel(>=0)]-right-|", 0, metrics, views));
			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-right-[titleLabel(>=0@999)]-left-[bodyLabel(>=0@999)]-left-|", 0, metrics, views));
			ContentView.AddConstraints (NSLayoutConstraint.FromVisualFormat ("V:|-right-[thumbnailView(thumbSize)]-(>=0)-|", 0, metrics, views));
		}

		public UILabel TitleLabel { get; private set; }

		public UILabel BodyLabel { get; private set; }

		public UIImageView ThumbnailView { get; private set; }


		public NSIndexPath IndexPath { get; set; }

		public bool UsedForMessage { get; set; }


		public override void PrepareForReuse ()
		{
			base.PrepareForReuse ();

			SelectionStyle = UITableViewCellSelectionStyle.None;

			var pointSize = DefaultFontSize;
			TitleLabel.Font = UIFont.BoldSystemFontOfSize (pointSize);
			BodyLabel.Font = UIFont.SystemFontOfSize (pointSize);

			TitleLabel.Text = "";
			BodyLabel.Text = "";
		}
	}
}
