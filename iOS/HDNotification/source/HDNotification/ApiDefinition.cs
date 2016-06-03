using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace HDNotification
{
	// @interface HDNotificationView : UIToolbar
	[BaseType (typeof(UIToolbar))]
	interface HDNotificationView
	{
		// +(void)showNotificationViewWithImage:(UIImage *)image title:(NSString *)title message:(NSString *)message;
		[Static]
		[Export ("showNotificationViewWithImage:title:message:")]
		void ShowNotification (UIImage image, string title, string message);

		// +(void)showNotificationViewWithImage:(UIImage *)image title:(NSString *)title message:(NSString *)message isAutoHide:(BOOL)isAutoHide;
		[Static]
		[Export ("showNotificationViewWithImage:title:message:isAutoHide:")]
		void ShowNotification (UIImage image, string title, string message, bool isAutoHide);

		// +(void)showNotificationViewWithImage:(UIImage *)image title:(NSString *)title message:(NSString *)message isAutoHide:(BOOL)isAutoHide onTouch:(void (^)())onTouch;
		[Static]
		[Export ("showNotificationViewWithImage:title:message:isAutoHide:onTouch:")]
		void ShowNotification (UIImage image, string title, string message, bool isAutoHide, [NullAllowed]Action onTouch);

		// +(void)hideNotificationView;
		[Static]
		[Export ("hideNotificationView")]
		void HideNotification ();

		// +(void)hideNotificationViewOnComplete:(void (^)())onComplete;
		[Static]
		[Export ("hideNotificationViewOnComplete:")]
		void HideNotification ([NullAllowed] Action onComplete);
	}

}

