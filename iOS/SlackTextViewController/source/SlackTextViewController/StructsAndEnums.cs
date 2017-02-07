using System;

#if __UNIFIED__
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace SlackHQ
{
	[Native]
	public enum CounterStyle : ulong
	{
		None,
		Split,
		Countdown,
		CountdownReversed
	}

	[Native]
	public enum CounterPosition : ulong
	{
		Top,
		Bottom
	}

	[Native]
	public enum PastableMediaType : ulong
	{
		None = 0,

		Png = 1 << 0,
		Jpeg = 1 << 1,
		Tiff = 1 << 2,
		Gif = 1 << 3,
		Mov = 1 << 4,
		Passbook = 1 << 5,

		Images = Png | Jpeg | Tiff | Gif,
		Videos = Mov,
		All = Images | Mov
	}

//	static class CFunctions
//	{
//		// CGFloat SLKPointSizeDifferenceForCategory (NSString *category) __attribute__((unused));
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern nfloat SLKPointSizeDifferenceForCategory (NSString category);
//
//		// CGRect SLKKeyWindowBounds ();
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern CGRect SLKKeyWindowBounds ();
//
//		// CGRect SLKRectInvert (CGRect rect);
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern CGRect SLKRectInvert (CGRect rect);
//	}

	[Native]
	public enum KeyboardStatus : ulong
	{
		DidHide,
		WillShow,
		DidShow,
		WillHide
	}
}
