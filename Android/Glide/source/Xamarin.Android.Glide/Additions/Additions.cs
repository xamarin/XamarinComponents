using System.Collections;
using System.Linq;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Bumptech.Glide.Load;
using Bumptech.Glide.Load.Engine;
using Bumptech.Glide.Load.Resource.Bitmap;
using Bumptech.Glide.Request;
using Bumptech.Glide.Request.Transition;
using Java.IO;
using Java.Lang;
using Java.Util.Concurrent;

namespace Bumptech.Glide
{
    public partial class RequestBuilder
    {
        public virtual RequestBuilder Apply(BaseRequestOptions o) => (RequestBuilder)Apply_T(o);

        public virtual RequestBuilder AutoClone() => (RequestBuilder)AutoClone_T();

        public virtual RequestBuilder CenterCrop() => (RequestBuilder)CenterCrop_T();

        public virtual RequestBuilder CenterInside() => (RequestBuilder)CenterInside_T();

        public virtual RequestBuilder CircleCrop() => (RequestBuilder)CircleCrop_T();

        public new virtual RequestBuilder Clone() => (RequestBuilder)Clone_T();

        public virtual RequestBuilder Decode(Class resourceClass) => (RequestBuilder)Decode_T(resourceClass);

        public virtual RequestBuilder DisallowHardwareConfig() => (RequestBuilder)DisallowHardwareConfig_T();
        
        public virtual RequestBuilder Downsample(DownsampleStrategy strategy) => (RequestBuilder)Downsample_T(strategy);

        public virtual RequestBuilder EncodeFormat(Bitmap.CompressFormat format) => (RequestBuilder)EncodeFormat_T(format);

        public virtual RequestBuilder EncodeQuality(int quality) => (RequestBuilder)EncodeQuality_T(quality);
	    
        public virtual RequestBuilder Error(Drawable drawable) => (RequestBuilder)Error_T(drawable);

        public virtual RequestBuilder Error(int resourceId) => (RequestBuilder)Error_T(resourceId);

        public virtual RequestBuilder Fallback(Drawable drawable) => (RequestBuilder)Fallback_T(drawable);

        public virtual RequestBuilder Fallback(int resourceId) => (RequestBuilder)Fallback_T(resourceId);

        public virtual RequestBuilder FitCenter() => (RequestBuilder)FitCenter_T();

        public virtual RequestBuilder Format(DecodeFormat format) => (RequestBuilder)Format_T(format);

        public virtual RequestBuilder Frame(long frameTimeMicros) => (RequestBuilder)Frame_T(frameTimeMicros);

        public virtual RequestBuilder Lock() => (RequestBuilder)Lock_T();
        
        public virtual RequestBuilder OptionalCenterCrop() => (RequestBuilder)OptionalCenterCrop_T();

        public virtual RequestBuilder OptionalCenterInside() => (RequestBuilder)OptionalCenterInside_T();

        public virtual RequestBuilder OptionalCircleCrop() => (RequestBuilder)OptionalCircleCrop_T();

        public virtual RequestBuilder OptionalFitCenter() => (RequestBuilder)OptionalFitCenter_T();

        public virtual RequestBuilder OptionalTransform(Class resourceClass, ITransformation transformation) => (RequestBuilder)OptionalTransform_T(resourceClass, transformation);

        public virtual RequestBuilder OptionalTransform(ITransformation transformation) => (RequestBuilder)OptionalTransform_T(transformation);

        public virtual RequestBuilder Override(int width, int height) => (RequestBuilder)Override_T(width, height);

        public virtual RequestBuilder Override(int size) => (RequestBuilder)Override_T(size);

        public virtual RequestBuilder Placeholder(Drawable drawable) => (RequestBuilder)Placeholder_T(drawable);

        public virtual RequestBuilder Placeholder(int resourceId) => (RequestBuilder)Placeholder_T(resourceId);
        
        public virtual RequestBuilder Set(Option option, Object value) => (RequestBuilder)Set_T(option, value);

        public virtual RequestBuilder SetDiskCacheStrategy(DiskCacheStrategy strategy) => (RequestBuilder)DiskCacheStrategy_T(strategy);

        public virtual RequestBuilder SetOnlyRetrieveFromCache(bool flag) => (RequestBuilder)OnlyRetrieveFromCache_T(flag);

        public virtual RequestBuilder SetPriority(Priority priority) => (RequestBuilder)Priority_T(priority);

        public virtual RequestBuilder SetSignature(IKey signature) => (RequestBuilder)Signature_T(signature);

        public virtual RequestBuilder SetSizeMultiplier(float sizeMultiplier) => (RequestBuilder)SizeMultiplier_T(sizeMultiplier);

        public virtual RequestBuilder SetTheme(Resources.Theme theme) => (RequestBuilder)Theme_T(theme);

        public virtual RequestBuilder SetUseAnimationPool(bool flag) => (RequestBuilder)UseAnimationPool_T(flag);

        public virtual RequestBuilder SetUseUnlimitedSourceGeneratorsPool(bool flag) => (RequestBuilder)UseUnlimitedSourceGeneratorsPool_T(flag);

        public virtual RequestBuilder SkipMemoryCache(bool skip) => (RequestBuilder)SkipMemoryCache_T(skip);

        public virtual RequestBuilder Timeout(int timeoutMs) => (RequestBuilder)Timeout_T(timeoutMs);

        public virtual RequestBuilder Transform(Class resourceClass, ITransformation transformation) => (RequestBuilder)Transform_T(resourceClass, transformation);

        public virtual RequestBuilder Transform(params ITransformation[] transformations) => (RequestBuilder)Transform_T(transformations);

        public virtual RequestBuilder Transform(ITransformation transformation) => (RequestBuilder)Transform_T(transformation);
    }
}

namespace Bumptech.Glide.Load.Data
{
    public partial class AssetFileDescriptorLocalUriFetcher
    {
        protected override void Close(Object data) => Close((AssetFileDescriptor)data);

        protected override Object LoadResource(Uri uri, ContentResolver contentResolver) => LoadResource_T(uri, contentResolver);
    }

    public partial class FileDescriptorAssetPathFetcher
	{
        protected override void Close(Object data) => Close((ParcelFileDescriptor)data);

        protected override Object LoadResource(AssetManager assetManager, string path) => LoadResource_T(assetManager, path);
    }

	public partial class FileDescriptorLocalUriFetcher
	{
		protected override void Close(Object data) => Close((ParcelFileDescriptor)data);

        protected override Object LoadResource(Uri uri, ContentResolver contentResolver) => LoadResource_T(uri, contentResolver);
    }

    public partial class StreamAssetPathFetcher
	{
		protected override void Close(Object data)
        {
            var stream = InputStreamInvoker.FromJniHandle(((InputStream)data).Handle, JniHandleOwnership.DoNotTransfer);

            Close(stream);
        }

        protected override Object LoadResource(AssetManager assetManager, string path)
		{
			var handle = InputStreamAdapter.ToLocalJniHandle(LoadResource_T(assetManager, path));

			try
			{
				return new Object(handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
				JNIEnv.DeleteLocalRef(handle);
			}
		}
	}

    public partial class StreamLocalUriFetcher
	{
		protected override void Close(Object data)
        {
            var stream = InputStreamInvoker.FromJniHandle(((InputStream)data).Handle, JniHandleOwnership.DoNotTransfer);

            Close(stream);
        }

        protected override Object LoadResource(Uri uri, ContentResolver contentResolver)
		{
			var handle = InputStreamAdapter.ToLocalJniHandle(LoadResource_T(uri, contentResolver));

			try
			{
				return new Object(handle, JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
				JNIEnv.DeleteLocalRef(handle);
			}
		}
	}
}

namespace Bumptech.Glide.Load.Engine.Executor
{
    public partial class GlideExecutor
    {
        IList IExecutorService.InvokeAll(ICollection tasks) => InvokeAll(tasks.Cast<ICallable>().ToList()).ToList();

        IList IExecutorService.InvokeAll(ICollection tasks, long timeout, TimeUnit unit) => InvokeAll(tasks.Cast<ICallable>().ToList(), timeout, unit).ToList();

        Object IExecutorService.InvokeAny(ICollection tasks) => InvokeAny(tasks.Cast<ICallable>().ToList());

        Object IExecutorService.InvokeAny(ICollection tasks, long timeout, TimeUnit unit) => InvokeAny(tasks.Cast<ICallable>().ToList(), timeout, unit);
    }
}

namespace Bumptech.Glide.Load.Model
{
	public partial class AssetUriLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData((Uri)model, width, height, options);

        public virtual bool Handles(Object model) => Handles((Uri)model);
    }

    public partial class ByteArrayLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData(model?.ToArray<byte>(), width, height, options);

        public virtual bool Handles(Object model) => Handles(model?.ToArray<byte>());
    }

	public partial class StringLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData(model?.ToString(), width, height, options);

        public virtual bool Handles(Object model) => Handles(model?.ToString());
    }
    public partial class FileLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData((File)model, width, height, options);

        public virtual bool Handles(Object model) => Handles((File)model);
    }

    public partial class ResourceLoader
    {
        public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData((Integer)model, width, height, options);

        public virtual bool Handles(Object model) => Handles((Integer)model);
    }

    public partial class UriLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData((Uri)model, width, height, options);

        public virtual bool Handles(Object model) => Handles((Uri)model);
    }

    public partial class UrlUriLoader
	{
		public virtual ModelLoaderLoadData BuildLoadData(Object model, int width, int height, Options options) =>
            BuildLoadData((Uri)model, width, height, options);

        public virtual bool Handles(Object model) => Handles((Uri)model);
    }
}

namespace Bumptech.Glide.Load.Model.Stream
{
    partial class QMediaStoreUriLoader
    {
        public bool Handles(Java.Lang.Object uri)
            => Handles((Uri)uri);

        public ModelLoaderLoadData BuildLoadData(Object uri, int width, int height, Options options)
            => BuildLoadData((Uri)uri, width, height, options);
    }
}


namespace Bumptech.Glide.Load.Resource
{
    partial class ImageDecoderResourceDecoder
    {
        public IResource Decode(Object source, int requestedWidth, int requestedHeight, Options options)
            => Decode((ImageDecoder.Source)source, requestedWidth, requestedHeight, options);

        public bool Handles(Object source, Options options)
            => Handles((ImageDecoder.Source)source, options);
    }
}

namespace Bumptech.Glide.Load.Resource.Gif
{
    public partial class GifDrawableEncoder
	{
        public virtual unsafe bool Encode(Object data, global::Java.IO.File file, global::Bumptech.Glide.Load.Options options)
            => Encode((IResource)data, file, options);
    }
}

namespace Bumptech.Glide.Load.Resource.Bitmap
{
	public partial class BitmapDrawableEncoder
	{
		public virtual bool Encode(Object data, Java.IO.File file, Options options) => Encode((IResource)data, file, options);
    }

    public partial class BitmapEncoder
	{
		public virtual bool Encode(Object data, Java.IO.File file, Options options) => Encode((IResource)data, file, options);
    }

    public partial class StreamBitmapDecoder : IResourceDecoder
	{
		IResource IResourceDecoder.Decode(Object source, int width, int height, Options options)
		{
			var stream = InputStreamInvoker.FromJniHandle(source.Handle, JniHandleOwnership.DoNotTransfer);

			return Decode(stream, width, height, options);
		}

		bool IResourceDecoder.Handles(Object source, Options options)
		{
			var stream = InputStreamInvoker.FromJniHandle(source.Handle, JniHandleOwnership.DoNotTransfer);

			return Handles(stream, options);
		}
	}
}

namespace Bumptech.Glide.Request
{
    public partial class RequestOptions
    {
        public virtual RequestOptions Apply(BaseRequestOptions o) => (RequestOptions)Apply_T(o);

        public virtual RequestOptions AutoClone() => (RequestOptions)AutoClone_T();

        public virtual RequestOptions CenterCrop() => (RequestOptions)CenterCrop_T();

        public virtual RequestOptions CenterInside() => (RequestOptions)CenterInside_T();

        public virtual RequestOptions CircleCrop() => (RequestOptions)CircleCrop_T();

        public new virtual RequestOptions Clone() => (RequestOptions)Clone_T();

        public virtual RequestOptions Decode(Class resourceClass) => (RequestOptions)Decode_T(resourceClass);

        public virtual RequestOptions DisallowHardwareConfig() => (RequestOptions)DisallowHardwareConfig_T();

        public virtual RequestOptions Downsample(DownsampleStrategy strategy) => (RequestOptions)Downsample_T(strategy);

        public virtual RequestOptions EncodeFormat(Bitmap.CompressFormat format) => (RequestOptions)EncodeFormat_T(format);

        public virtual RequestOptions EncodeQuality(int quality) => (RequestOptions)EncodeQuality_T(quality);
	    
        public virtual RequestBuilder Error(Drawable drawable) => (RequestBuilder)Error_T(drawable);

        public virtual RequestOptions Error(int resourceId) => (RequestOptions)Error_T(resourceId);

        public virtual RequestOptions Fallback(Drawable drawable) => (RequestOptions)Fallback_T(drawable);

        public virtual RequestOptions Fallback(int resourceId) => (RequestOptions)Fallback_T(resourceId);

        public virtual RequestOptions FitCenter() => (RequestOptions)FitCenter_T();

        public virtual RequestOptions Format(DecodeFormat format) => (RequestOptions)Format_T(format);

        public virtual RequestOptions Frame(long frameTimeMicros) => (RequestOptions)Frame_T(frameTimeMicros);

        public virtual RequestOptions Lock() => (RequestOptions)Lock_T();

        public virtual RequestOptions OptionalCenterCrop() => (RequestOptions)OptionalCenterCrop_T();

        public virtual RequestOptions OptionalCenterInside() => (RequestOptions)OptionalCenterInside_T();

        public virtual RequestOptions OptionalCircleCrop() => (RequestOptions)OptionalCircleCrop_T();

        public virtual RequestOptions OptionalFitCenter() => (RequestOptions)OptionalFitCenter_T();

        public virtual RequestOptions OptionalTransform(Class resourceClass, ITransformation transformation) => (RequestOptions)OptionalTransform_T(resourceClass, transformation);

        public virtual RequestOptions OptionalTransform(ITransformation transformation) => (RequestOptions)OptionalTransform_T(transformation);

        public virtual RequestOptions Override(int width, int height) => (RequestOptions)Override_T(width, height);

        public virtual RequestOptions Override(int size) => (RequestOptions)Override_T(size);

        public virtual RequestOptions Placeholder(Drawable drawable) => (RequestOptions)Placeholder_T(drawable);

        public virtual RequestOptions Placeholder(int resourceId) => (RequestOptions)Placeholder_T(resourceId);

        public virtual RequestOptions Set(Option option, Object value) => (RequestOptions)Set_T(option, value);

        public virtual RequestOptions SetDiskCacheStrategy(DiskCacheStrategy strategy) => (RequestOptions)DiskCacheStrategy_T(strategy);

        public virtual RequestOptions SetOnlyRetrieveFromCache(bool flag) => (RequestOptions)OnlyRetrieveFromCache_T(flag);

        public virtual RequestOptions SetPriority(Priority priority) => (RequestOptions)Priority_T(priority);

        public virtual RequestOptions SetSignature(IKey signature) => (RequestOptions)Signature_T(signature);

        public virtual RequestOptions SetSizeMultiplier(float sizeMultiplier) => (RequestOptions)SizeMultiplier_T(sizeMultiplier);

        public virtual RequestOptions SetTheme(Resources.Theme theme) => (RequestOptions)Theme_T(theme);

        public virtual RequestOptions SetUseAnimationPool(bool flag) => (RequestOptions)UseAnimationPool_T(flag);

        public virtual RequestOptions SetUseUnlimitedSourceGeneratorsPool(bool flag) => (RequestOptions)UseUnlimitedSourceGeneratorsPool_T(flag);

        public virtual RequestOptions SkipMemoryCache(bool skip) => (RequestOptions)SkipMemoryCache_T(skip);

        public virtual RequestOptions Timeout(int timeoutMs) => (RequestOptions)Timeout_T(timeoutMs);

        public virtual RequestOptions Transform(Class resourceClass, ITransformation transformation) => (RequestOptions)Transform_T(resourceClass, transformation);

        public virtual RequestOptions Transform(params ITransformation[] transformations) => (RequestOptions)Transform_T(transformations);

        public virtual RequestOptions Transform(ITransformation transformation) => (RequestOptions)Transform_T(transformation);
    }
}

namespace Bumptech.Glide.Request.Target
{
    public partial class AppWidgetTarget
    {
        public override void OnResourceReady(Object resource, ITransition transition) => OnResourceReady((Bitmap)resource, transition);
    }

    public partial class BitmapImageViewTarget
    {
        protected override void SetResource(Object resource) => SetResource((Bitmap)resource);
    }

    public partial class BitmapThumbnailImageViewTarget
    {
        protected override Drawable GetDrawable(Object resource) => GetDrawable((Bitmap)resource);
    }

    public partial class DrawableImageViewTarget
    {
        protected override void SetResource(Object resource) => SetResource((Drawable)resource);
    }

    public partial class DrawableThumbnailImageViewTarget
    {
        protected override Drawable GetDrawable(Object resource) => GetDrawable((Drawable)resource);
    }

    public partial class NotificationTarget
    {
        public override void OnResourceReady(Object resource, ITransition transition) => OnResourceReady((Bitmap)resource, transition);
    }
}

namespace Bumptech.Glide.Request.Transition
{
    public partial class BitmapTransitionFactory
    {
        protected override Bitmap GetBitmap(Object current) => GetBitmap((Bitmap)current);
    }
}
