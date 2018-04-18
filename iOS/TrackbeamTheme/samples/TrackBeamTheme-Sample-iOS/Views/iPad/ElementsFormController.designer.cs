// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TrackBeamTheme_Sample_iOS
{
    [Register ("ElementsFormController")]
    partial class ElementsFormController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton loginButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Xamarin.Themes.TrackBeam.Controls.TBSwitch offSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Xamarin.Themes.TrackBeam.Controls.TBSwitch onSwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView progressView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton registerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider sliderView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField textField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (loginButton != null) {
                loginButton.Dispose ();
                loginButton = null;
            }

            if (offSwitch != null) {
                offSwitch.Dispose ();
                offSwitch = null;
            }

            if (onSwitch != null) {
                onSwitch.Dispose ();
                onSwitch = null;
            }

            if (progressView != null) {
                progressView.Dispose ();
                progressView = null;
            }

            if (registerButton != null) {
                registerButton.Dispose ();
                registerButton = null;
            }

            if (sliderView != null) {
                sliderView.Dispose ();
                sliderView = null;
            }

            if (textField != null) {
                textField.Dispose ();
                textField = null;
            }
        }
    }
}