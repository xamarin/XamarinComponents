using System;
using System.IO;
using CoreGraphics;
using Foundation;
using UIKit;

using GPUImage.Filters.Effects;
using GPUImage.Outputs;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class SimpVideoFilterViewController : UIViewController
	{
		private GPUImagePixellateFilter filter;

		public SimpVideoFilterViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var sampleURL = NSBundle.MainBundle.GetUrlForResource ("sample_iPod", "m4v");

			var movieFile = new GPUImageMovie (sampleURL);
			movieFile.RunBenchmark = true;
			movieFile.PlayAtActualSpeed = false;

			filter = new GPUImagePixellateFilter ();
			movieFile.AddTarget (filter);

			// Only rotate the video for display, leave orientation the same for recording
			filter.AddTarget (imageView);

			// In addition to displaying to the screen, write out a processed version of the movie to disk
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var pathToMovie = Path.Combine (documents, "Movie.m4v");

			// If a file already exists, AVAssetWriter won't let you record new frames, so delete the old movie
			if (File.Exists (pathToMovie)) {
				File.Delete (pathToMovie);
			}
			var movieURL = new NSUrl (pathToMovie, false);

			var movieWriter = new GPUImageMovieWriter (movieURL, new CGSize (640.0f, 480.0f));
			filter.AddTarget (movieWriter);

			// Configure this for video from the movie file, where we want to preserve all video frames and audio samples
			movieWriter.ShouldPassthroughAudio = true;
			movieFile.AudioEncodingTarget = movieWriter;
			movieFile.EnableSynchronizedEncoding (movieWriter);

			var timer = NSTimer.CreateRepeatingScheduledTimer (0.3, _ => {
				progressLabel.Text = movieFile.Progress.ToString ("P0");
			});

			movieWriter.CompletionHandler = async () => {
				filter.RemoveTarget (movieWriter);
				await movieWriter.FinishRecordingAsync ();

				InvokeOnMainThread (() => {
					timer.Invalidate ();
					progressLabel.Text = 1.ToString ("P0");
				});
			};

			OnPixelWidthChanged (pixelWidthSlider);

			movieWriter.StartRecording ();
			movieFile.StartProcessing ();
		}

		partial void OnPixelWidthChanged (UISlider sender)
		{
			filter.FractionalWidthOfAPixel = sender.Value;
		}
	}
}
