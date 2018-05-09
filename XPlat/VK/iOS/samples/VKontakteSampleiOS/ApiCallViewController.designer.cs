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

namespace VKontakteSampleiOS
{
    [Register ("ApiCallViewController")]
    partial class ApiCallViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView callResult { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (callResult != null) {
                callResult.Dispose ();
                callResult = null;
            }
        }
    }
}