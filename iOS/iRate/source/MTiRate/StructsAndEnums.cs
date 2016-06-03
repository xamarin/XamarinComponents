using System;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

namespace MTiRate
{
	[Native]
	public enum iRateErrorCode : ulong
	{
		BundleIdDoesNotMatchAppStore = 1,
		ApplicationNotFoundOnAppStore,
		ApplicationIsNotLatestVersion,
		CouldNotOpenRatingPageURL
	}
}

