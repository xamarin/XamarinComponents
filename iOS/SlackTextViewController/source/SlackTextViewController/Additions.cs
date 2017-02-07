using System;
using System.Runtime.InteropServices;

#if __UNIFIED__
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using nint = System.Int32;
#endif

namespace SlackHQ
{
	partial class SlackTextViewController
	{
		public void RegisterClassForTextView (Type type)
		{
			RegisterClassForTextView (new Class (type));
		}

		public void RegisterClassForTextView<T> ()
			where T : SlackTextView
		{
			RegisterClassForTextView (typeof(T));
		}

		public void RegisterClassForTypingIndicatorView (Type type)
		{
			RegisterClassForTypingIndicatorView (new Class (type));
		}

		public void RegisterClassForTypingIndicatorView<T> ()
			where T : SlackTypingIndicatorView
		{
			RegisterClassForTypingIndicatorView (typeof(T));
		}
	}

	partial class SlackTextView
	{
		public static nfloat SlackPointSizeDifference (string category)
		{
			switch (UIContentSizeCategoryExtensions.GetValue((NSString)category))
			{
				case UIContentSizeCategory.ExtraSmall: return -3.0f;
				case UIContentSizeCategory.Small: return -2.0f;
				case UIContentSizeCategory.Medium: return -1.0f;
				case UIContentSizeCategory.Large: return 0.0f;
				case UIContentSizeCategory.ExtraLarge: return 2.0f;
				case UIContentSizeCategory.ExtraExtraLarge: return 4.0f;
				case UIContentSizeCategory.ExtraExtraExtraLarge: return 6.0f;
				case UIContentSizeCategory.AccessibilityMedium: return 8.0f;
				case UIContentSizeCategory.AccessibilityLarge: return 10.0f;
				case UIContentSizeCategory.AccessibilityExtraLarge: return 11.0f;
				case UIContentSizeCategory.AccessibilityExtraExtraLarge: return 12.0f;
				case UIContentSizeCategory.AccessibilityExtraExtraExtraLarge: return 13.0f;
					
				case UIContentSizeCategory.Unspecified:
				default:
					return 0;
			}
		}
	}
}
