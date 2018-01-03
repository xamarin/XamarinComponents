using System;
using Foundation;
using ObjCRuntime;

namespace SDWebImage
{
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
			get { return Request; }
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

	partial class SDImageCache
	{
		public void ClearDisk ()
		{
			ClearDisk (null);
		}
	}

}
