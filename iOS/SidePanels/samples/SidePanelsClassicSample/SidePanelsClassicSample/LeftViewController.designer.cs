// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using System;
using System.CodeDom.Compiler;
using MonoTouch.UIKit;

namespace SidePanelsClassicSample
{
    [Register("LeftViewController")]
    partial class LeftViewController
    {
        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIButton switchCenter { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIButton toggleCenter { get; set; }

        [Outlet]
        [GeneratedCode("iOS Designer", "1.0")]
        UIButton toggleRight { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (switchCenter != null)
            {
                switchCenter.Dispose();
                switchCenter = null;
            }
            if (toggleCenter != null)
            {
                toggleCenter.Dispose();
                toggleCenter = null;
            }
            if (toggleRight != null)
            {
                toggleRight.Dispose();
                toggleRight = null;
            }
        }
    }
}
