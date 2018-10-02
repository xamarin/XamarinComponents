using System;
using ObjCRuntime;

namespace SDWebImage
{
	[Native]
	public enum SDImageCacheType : long
	{
		None = 0,
		Disk,
		Memory
	}

	[Flags]
	[Native]
	public enum SDWebImageDownloaderOptions : ulong
	{
		LowPriority = 1 << 0,
		ProgressiveDownload = 1 << 1,
		UseNSUrlCache = 1 << 2,
		IgnoreCachedResponse = 1 << 3,
		ContinueInBackground = 1 << 4,
		HandleCookies = 1 << 5,
		AllowInvalidSSLCertificates = 1 << 6,
		HighPriority = 1 << 7,
		ScaleDownLargeImages = 1 << 8
	}

	[Native]
	public enum SDWebImageDownloaderExecutionOrder : long
	{
		FirstInFirstOut,
		LastInFirstOut
	}

	[Flags]
	[Native]
	public enum SDWebImageOptions : ulong
	{
		RetryFailed = 1 << 0,
		LowPriority = 1 << 1,
		CacheMemoryOnly = 1 << 2,
		ProgressiveDownload = 1 << 3,
		RefreshCached = 1 << 4,
		ContinueInBackground = 1 << 5,
		HandleCookies = 1 << 6,
		AllowInvalidSSLCertificates = 1 << 7,
		HighPriority = 1 << 8,
		DelayPlaceholder = 1 << 9,
		TransformAnimatedImage = 1 << 10,
		AvoidAutoSetImage = 1 << 11,
		ScaleDownLargeImages = 1 << 12
	}

	[Native]
	public enum SDImageFormat : long
	{
		Undefined = -1,
		Jpeg = 0,
		Png,
		Gif,
		Tiff,
		WebP
	}
}
