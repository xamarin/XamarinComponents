using System;
using CoreGraphics;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class SettingsViewController : UIViewController
	{
		public SettingsViewController()
		{
			NavigationItem.Title = "Settings";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var scrollView = new UIScrollView(View.Bounds);
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			scrollView.BackgroundColor = UIColor.White;
			scrollView.ContentSize = View.Bounds.Size;
			View.AddSubview(scrollView);

			var viewBounds = View.Bounds;

			nfloat margin = 12;
			nfloat spacing = 6;

			nfloat yProgress = margin;
			var sliderLabel = new UILabel(new CGRect(margin, yProgress, viewBounds.Width - margin - margin, 30));
			sliderLabel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			sliderLabel.Text = $"Search Count ({AppDelegate.Current.SearchCount})";
			scrollView.AddSubview(sliderLabel);
			yProgress += sliderLabel.Frame.Height + (spacing / 2);

			yProgress += spacing;
			var slider = new UISlider(new CGRect(margin, yProgress, viewBounds.Size.Width - margin - margin, 56));
			slider.MinValue = 10;
			slider.MaxValue = 200;
			slider.ValueChanged += delegate
			{
				AppDelegate.Current.SearchCount = (int)slider.Value;
				sliderLabel.Text = $"Search Count ({AppDelegate.Current.SearchCount})";
			};
			slider.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			slider.TintColor = View.TintColor;
			slider.MinimumTrackTintColor = View.TintColor;
			slider.Value = Math.Min(Math.Max(10, AppDelegate.Current.SearchCount), 1000);
			scrollView.AddSubview(slider);
			yProgress += slider.Frame.Height;

			yProgress += spacing;
			var label = new UILabel(new CGRect(margin, yProgress, viewBounds.Width - margin - margin, 30));
			label.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			label.Text = "Debug Overlay";
			scrollView.AddSubview(label);
			var debugSwitch = new UISwitch();
			debugSwitch.On = TIPImageViewFetchHelper.IsDebugInfoVisible;
			CGRect debugFrame = debugSwitch.Frame;
			debugFrame.Y = yProgress;
			debugFrame.X = (viewBounds.Width - margin) - debugFrame.Width;
			debugSwitch.Frame = debugFrame;
			debugSwitch.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			debugSwitch.ValueChanged += delegate
			{
				TIPImageViewFetchHelper.IsDebugInfoVisible = debugSwitch.On;
			};
			scrollView.AddSubview(debugSwitch);
			yProgress += label.Frame.Height;

			yProgress += spacing;
			label = new UILabel(new CGRect(margin, yProgress, viewBounds.Width - margin - margin, 30));
			label.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			label.Text = "Placeholder Images";
			scrollView.AddSubview(label);
			var placeholderSwitch = new UISwitch();
			placeholderSwitch.On = AppDelegate.Current.UsePlaceholder;
			var placeholderFrame = placeholderSwitch.Frame;
			placeholderFrame.Y = yProgress;
			placeholderFrame.X = (viewBounds.Width - margin) - placeholderFrame.Width;
			placeholderSwitch.Frame = placeholderFrame;
			placeholderSwitch.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
			placeholderSwitch.ValueChanged += delegate
			{
				AppDelegate.Current.UsePlaceholder = placeholderSwitch.On;
			};
			scrollView.AddSubview(placeholderSwitch);
			yProgress += label.Frame.Height;
		}
	}
}
