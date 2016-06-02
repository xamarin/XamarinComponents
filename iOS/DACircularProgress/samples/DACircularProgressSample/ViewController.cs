using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;
using Masonry;

using DACircularProgress;

namespace DACircularProgressSample
{
	public class ViewController : UIViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.Gray;

			var progressView = new CircularProgressView ();
			progressView.TrackTintColor = UIColor.Clear;
			View.AddSubview (progressView);

			var labeledProgressView = new LabeledCircularProgressView ();
			labeledProgressView.RoundedCorners = true;
			View.AddSubview (labeledProgressView);

			var pieProgressView = new CircularProgressView ();
			pieProgressView.ProgressTintColor = UIColor.Yellow;
			pieProgressView.TrackTintColor = UIColor.Black;
			pieProgressView.ThicknessRatio = 1.0f;
			View.AddSubview (pieProgressView);

			var indeterminateProgressView = new CircularProgressView ();
			indeterminateProgressView.Progress = 0.35f;
			indeterminateProgressView.IndeterminateDuration = 1.0f;
			indeterminateProgressView.Indeterminate = true;
			View.AddSubview (indeterminateProgressView);

			// constraints
			progressView.MakeConstraints (make => {
				make.CenterX.EqualTo (View.CenterX ());
				make.Top.EqualTo (this.TopLayoutGuideBottom ()).Offset (12.0f);
				make.Width.EqualTo (NSNumber.FromNFloat (60));
				make.Height.EqualTo (NSNumber.FromNFloat (60));
			});
			labeledProgressView.MakeConstraints (make => {
				make.CenterX.EqualTo (View.CenterX ());
				make.Top.EqualTo (progressView.Bottom ()).Offset (12.0f);
				make.Width.EqualTo (NSNumber.FromNFloat (60));
				make.Height.EqualTo (NSNumber.FromNFloat (60));
			});
			pieProgressView.MakeConstraints (make => {
				make.CenterX.EqualTo (View.CenterX ());
				make.Top.EqualTo (labeledProgressView.Bottom ()).Offset (12.0f);
				make.Width.EqualTo (NSNumber.FromNFloat (60));
				make.Height.EqualTo (NSNumber.FromNFloat (60));
			});
			indeterminateProgressView.MakeConstraints (make => {
				make.CenterX.EqualTo (View.CenterX ());
				make.Top.EqualTo (pieProgressView.Bottom ()).Offset (12.0f);
				make.Width.EqualTo (NSNumber.FromNFloat (60));
				make.Height.EqualTo (NSNumber.FromNFloat (60));
			});

			var progress = -0.1f;
			Task.Run (async () => {
				while (true) {
					if (progress >= 1.0f) {
						progress = 0.0f;
					} else {
						progress += 0.1f;
					}

					InvokeOnMainThread (() => {
						progressView.SetProgress (progress, true);
						pieProgressView.SetProgress (progress, true);

						labeledProgressView.SetProgress (progress, true);
						labeledProgressView.ProgressLabel.Text = progress.ToString("0%");
					});

					if (progress <= 0.0f) {
						await Task.Delay (1500);
					} else {
						await Task.Delay (400);
					}
				}
			});
		}
	}
}
