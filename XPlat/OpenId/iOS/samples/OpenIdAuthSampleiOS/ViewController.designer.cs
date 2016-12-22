// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace OpenIdAuthSampleiOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton authAutoButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton authManual { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton clearAuthStateButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton codeExchangeButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIButton userinfoButton { get; set; }

		[Action ("AuthNoCodeExchange:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void AuthNoCodeExchange (UIKit.UIButton sender);

		[Action ("AuthWithAutoCodeExchange:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void AuthWithAutoCodeExchange (UIKit.UIButton sender);

		[Action ("ClearAuthState:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ClearAuthState (UIKit.UIButton sender);

		[Action ("CodeExchange:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void CodeExchange (UIKit.UIButton sender);

		[Action ("Userinfo:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void Userinfo (UIKit.UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (authAutoButton != null) {
				authAutoButton.Dispose ();
				authAutoButton = null;
			}

			if (authManual != null) {
				authManual.Dispose ();
				authManual = null;
			}

			if (clearAuthStateButton != null) {
				clearAuthStateButton.Dispose ();
				clearAuthStateButton = null;
			}

			if (codeExchangeButton != null) {
				codeExchangeButton.Dispose ();
				codeExchangeButton = null;
			}

			if (userinfoButton != null) {
				userinfoButton.Dispose ();
				userinfoButton = null;
			}
		}
	}
}