using System;
using System.ComponentModel;
using System.Threading.Tasks;

#if __UNIFIED__
using AVFoundation;
using AVKit;
using CoreGraphics;
using CoreMedia;
using Foundation;
using UIKit;
#else
using MonoTouch.AVFoundation;
using MonoTouch.AVKit;
using MonoTouch.CoreMedia;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace VideoSplash
{
    public enum ScalingMode
    {
        Resize = AVLayerVideoGravity.Resize,
        ResizeAspect = AVLayerVideoGravity.ResizeAspect,
        ResizeAspectFill = AVLayerVideoGravity.ResizeAspectFill
    }

    [DesignTimeVisible(true), Category("Controllers & Objects")]
    [Register("VideoViewController")]
    public class VideoViewController : UIViewController
    {
        private readonly AVPlayerViewController moviePlayer = new AVPlayerViewController();

        private float moviePlayerSoundLevel;
        private NSUrl videoUrl;
        private bool sound;
        private IDisposable playObserver;
        private NSObject repeatObserver;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewController"/> class.
        /// </summary>
        public VideoViewController()
        {
            Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewController"/> class when creating managed 
        /// representations of unmanaged objects. Called by the runtime.
        /// </summary>
        /// <param name="handle">Pointer (handle) to the unmanaged object.</param>
        public VideoViewController(IntPtr handle)
            : base(handle)
        {
            Setup();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewController"/> class from the data stored in 
        /// the unarchiver object.
        /// </summary>
        /// <param name="coder">The unarchiver object.</param>
        public VideoViewController(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        private void Setup()
        {
            FillMode = ScalingMode.ResizeAspectFill;
            BackgroundColor = UIColor.Black;
            StartTime = 0.0f;
            Duration = 0.0f;
            Repeat = true;
            Sound = true;
        }

        [Browsable(true)]
        [Export("VideoUrl")]
        public NSUrl VideoUrl
        {
            get { return videoUrl; }
            set
            {
                videoUrl = value;
                SetMoviePlayer(value);
            }
        }
        
        [Browsable(true)]
        [Export("StartTime")]
        public nfloat StartTime { get; set; }

        [Browsable(true)]
        [Export("Duration")]
        public nfloat Duration { get; set; }

        [Browsable(true)]
        [Export("BackgroundColor")]
        public UIColor BackgroundColor
        {
            get { return View.BackgroundColor; }
            set { View.BackgroundColor = value; }
        }

        [Browsable(true)]
        [Export("Sound")]
        public bool Sound
        {
            get { return sound; }
            set
            {
                sound = value;
                moviePlayerSoundLevel = value ? 1.0f : 0.0f;
            }
        }

        [Browsable(true)]
        [Export("Alpha")]
        public nfloat Alpha
        {
            get { return moviePlayer.View.Alpha; }
            set { moviePlayer.View.Alpha = value; }
        }

        [Browsable(true)]
        [Export("Repeat")]
        public bool Repeat { get; set; }

        [Browsable(true)]
        [Export("FillMode")]
        public ScalingMode FillMode
        {
            get { return (ScalingMode)moviePlayer.VideoGravity; }
            set { moviePlayer.VideoGravity = (AVLayerVideoGravity)value; }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            moviePlayer.ShowsPlaybackControls = false;
            moviePlayer.View.UserInteractionEnabled = false;
            View.AddSubview(moviePlayer.View);
            View.SendSubviewToBack(moviePlayer.View);
        }

        public override void ViewDidLayoutSubviews()
        {
            moviePlayer.View.Frame = View.Frame;

            base.ViewDidLayoutSubviews();
        }

        private async Task SetMoviePlayer(NSUrl url)
        {
            var path = await VideoCutter.CropVideoAsync(url, StartTime, Duration);
            if (path != null)
            {
                moviePlayer.Player = new AVPlayer(path);

                if (repeatObserver != null)
                {
                    NSNotificationCenter.DefaultCenter.RemoveObserver(repeatObserver);
                }
                repeatObserver = NSNotificationCenter.DefaultCenter.AddObserver(
                    AVPlayerItem.DidPlayToEndTimeNotification,
                    n => OnVideoComplete(),
                    moviePlayer.Player.CurrentItem);

                if (playObserver != null)
                {
                    playObserver.Dispose();
                }
                playObserver = moviePlayer.Player.AddObserver("status", NSKeyValueObservingOptions.New, v =>
                {
                    if (moviePlayer.Player.Status == AVPlayerStatus.ReadyToPlay)
                    {
                        OnVideoReady();
                    }
                });

                moviePlayer.Player.Play();
                moviePlayer.Player.Volume = moviePlayerSoundLevel;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (playObserver != null)
            {
                playObserver.Dispose();
                playObserver = null;
            }

            if (repeatObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(repeatObserver);
            }

            base.Dispose(disposing);
        }

        public virtual void OnVideoReady()
        {
        }

        public virtual void OnVideoComplete()
        {
            if (Repeat && moviePlayer.Player != null)
            {
                moviePlayer.Player.Seek(CMTime.Zero);
                moviePlayer.Player.Play();
            }
        }

        public virtual void PlayVideo()
        {
            if (moviePlayer.Player != null)
            {
                moviePlayer.Player.Play();
            }
        }

        public virtual void PauseVideo()
        {
            if (moviePlayer.Player != null)
            {
                moviePlayer.Player.Pause();
            }
        }        
    }
}
