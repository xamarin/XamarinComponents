using System;
using System.ComponentModel;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
using nuint = System.UInt32;
#endif

namespace AMViralSwitch
{
	// @interface AMViralSwitch : UISwitch
	[BaseType (typeof(UISwitch), Name = "AMViralSwitch")]
	interface ViralSwitch
	{
		// extern NSString *const AMElementView;
		[Static]
		[Field ("AMElementView", "__Internal")]
		NSString ElementView { get; }

		// extern NSString *const AMElementKeyPath;
		[Static]
		[Field ("AMElementKeyPath", "__Internal")]
		NSString ElementKeyPath { get; }

		// extern NSString *const AMElementFromValue;
		[Static]
		[Field ("AMElementFromValue", "__Internal")]
		NSString ElementFromValue { get; }

		// extern NSString *const AMElementToValue;
		[Static]
		[Field ("AMElementToValue", "__Internal")]
		NSString ElementToValue { get; }


		// @property (assign, nonatomic) NSTimeInterval animationDuration __attribute__((annotate("ui_appearance_selector")));
		[Export ("animationDuration")]
		double AnimationDuration { get; set; }

		// @property (nonatomic, strong) NSArray * animationElementsOn;
		[Export ("animationElementsOn", ArgumentSemantic.Strong)]
		NSDictionary[] AnimationElementsOn { get; set; }

		// @property (nonatomic, strong) NSArray * animationElementsOff;
		[Export ("animationElementsOff", ArgumentSemantic.Strong)]
		NSDictionary[] AnimationElementsOff { get; set; }

		// @property (copy, nonatomic) void (^completionOn)();
		[Internal]
		[Export ("completionOn", ArgumentSemantic.Copy)]
		Action CompletionOn { get; set; }

		// @property (copy, nonatomic) void (^completionOff)();
		[Internal]
		[Export ("completionOff", ArgumentSemantic.Copy)]
		Action CompletionOff { get; set; }
	}
}
