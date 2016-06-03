using System;
using System.Runtime.CompilerServices;

#if __UNIFIED__
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
#endif

namespace SDWebImage
{
	partial class ImageContentType
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSData");
	}

	partial class ForceDecodeUIImage
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIImage");
	}

	partial class AnimatedUIImage
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIImage");
	}

	partial class MultiFormatUIImage
	{
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIImage");
	}

	partial class SDWebImageDownloader
	{
		[Obsolete ("Deprecated. Use SetHttpHeaderValue instead.")]
		public void SetValueforHTTPHeaderField (string value, string field)
		{
			SetHttpHeaderValue (value, field);
		}

		[Obsolete ("Deprecated. Use GetHttpHeaderValue instead.")]
		public string ValueForHTTPHeaderField (string field)
		{
			return GetHttpHeaderValue (field);
		}

		public void SetOperationClass (Type operationClass)
		{
			SetOperationClass (new Class (operationClass));
		}

		public void SetOperationClass<T> ()
			where T : SDWebImageDownloaderOperation
		{
			SetOperationClass (new Class (typeof(T)));
		}
	}

	partial class SDWebImageDownloaderOperation
	{
		[Obsolete ("Deprecated. Use Request instead.")]
		public NSUrlRequest request { 
			get { return request; }
		}
	}

	partial class SDWebImagePrefetcher
	{
		[Obsolete ("Deprecated. Use PrefetchUrls instead.")]
		public void prefetchUrls (NSUrl[] urls)
		{
			PrefetchUrls (urls);
		}

		[Obsolete ("Deprecated. Use PrefetchUrls instead.")]
		public void prefetchURLs (NSUrl[] urls, SDWebImagePrefetcherProgressHandler progressBlock, SDWebImagePrefetcherCompletionHandler completionBlock)
		{
			PrefetchUrls (urls, progressBlock, completionBlock);
		}
	}

}
