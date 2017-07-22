// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace YouTubePlayerSample
{
    [Register ("PlaylistViewController")]
    partial class PlaylistViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton nextVideoButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton pauseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton playButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        YouTube.Player.PlayerView playerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton previousVideoButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView statusTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton stopButton { get; set; }

        [Action ("buttonPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void buttonPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (nextVideoButton != null) {
                nextVideoButton.Dispose ();
                nextVideoButton = null;
            }

            if (pauseButton != null) {
                pauseButton.Dispose ();
                pauseButton = null;
            }

            if (playButton != null) {
                playButton.Dispose ();
                playButton = null;
            }

            if (playerView != null) {
                playerView.Dispose ();
                playerView = null;
            }

            if (previousVideoButton != null) {
                previousVideoButton.Dispose ();
                previousVideoButton = null;
            }

            if (statusTextView != null) {
                statusTextView.Dispose ();
                statusTextView = null;
            }

            if (stopButton != null) {
                stopButton.Dispose ();
                stopButton = null;
            }
        }
    }
}