using System;
using System.Runtime.InteropServices;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace VKontakte
{
	//	static class CFunctions
	//	{
	//		// extern NSArray * VKParseVkPermissionsFromInteger (NSInteger permissionsValue);
	//		[DllImport ("__Internal")]
	//		[Verify (PlatformInvoke), Verify (StronglyTypedNSArray)]
	//		static extern NSObject[] VKParseVkPermissionsFromInteger (nint permissionsValue);
	//
	//		// NSNumber * VK_ENSURE_NUM (id obj);
	//		[DllImport ("__Internal")]
	//		[Verify (PlatformInvoke)]
	//		static extern NSNumber VK_ENSURE_NUM (NSObject obj);
	//
	//		// NSDictionary * VK_ENSURE_DICT (id data);
	//		[DllImport ("__Internal")]
	//		[Verify (PlatformInvoke)]
	//		static extern NSDictionary VK_ENSURE_DICT (NSObject data);
	//
	//		// NSArray * VK_ENSURE_ARRAY (id data);
	//		[DllImport ("__Internal")]
	//		[Verify (PlatformInvoke), Verify (StronglyTypedNSArray)]
	//		static extern NSObject[] VK_ENSURE_ARRAY (NSObject data);
	//	}
}

namespace VKontakte
{
	[Native]
	public enum VKAuthorizationOptions : ulong
	{
		UnlimitedToken = 1 << 0,
		DisableSafariController = 1 << 1
	}

	[Native]
	public enum VKAuthorizationState : ulong
	{
		Unknown,
		Initialized,
		Pending,
		External,
		SafariInApp,
		Webview,
		Authorized,
		Error
	}
}

namespace VKontakte.Core
{
	public enum VKOperationState
	{
		Paused = -1,
		Ready = 1,
		Executing = 2,
		Finished = 3
	}
}

namespace VKontakte.API.Methods
{
	[Native]
	public enum VKProgressType : long
	{
		Upload,
		Download
	}
}

namespace VKontakte.Image
{
	public enum VKImageType : uint
	{
		Jpg,
		Png
	}
}

namespace VKontakte.Views
{
	[Native]
	public enum VKShareDialogControllerResult : long
	{
		Cancelled,
		Done
	}

	[Native]
	public enum VKAuthorizationType : ulong
	{
		WebView,
		Safari,
		App
	}
}
