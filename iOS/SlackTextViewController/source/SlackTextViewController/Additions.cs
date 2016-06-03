using System;

#if __UNIFIED__
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
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
}
