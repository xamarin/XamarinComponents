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

namespace ShimmerSample
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Shimmer.ShimmeringView _shimmeringView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel _valueLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (_shimmeringView != null) {
				_shimmeringView.Dispose ();
				_shimmeringView = null;
			}
			if (_valueLabel != null) {
				_valueLabel.Dispose ();
				_valueLabel = null;
			}
		}
	}
}
