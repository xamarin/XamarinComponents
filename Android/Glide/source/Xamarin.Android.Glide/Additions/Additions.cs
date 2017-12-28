namespace Bumptech.Glide.Request.Transition
{
	partial class BitmapTransitionFactory
	{
		protected override Android.Graphics.Bitmap GetBitmap(Java.Lang.Object p0)
		{
			return GetBitmap((Android.Graphics.Bitmap)p0);
		}
	}
}

namespace Bumptech.Glide.Load.Data
{
	partial class FileDescriptorAssetPathFetcher
	{
		protected override Java.Lang.Object LoadResource(Android.Content.Res.AssetManager p0, string p1)
		{
			return LoadFile(p0, p1);
		}

		protected override unsafe void Close(Java.Lang.Object p0)
		{
			Close((Android.OS.ParcelFileDescriptor)p0);
		}
	}

	partial class FileDescriptorLocalUriFetcher
	{
		protected override unsafe void Close(global::Java.Lang.Object p0)
		{
			Close((Android.OS.ParcelFileDescriptor)p0);
		}

		protected override unsafe Java.Lang.Object LoadResource(global::Android.Net.Uri p0, global::Android.Content.ContentResolver p1)
		{
			return LoadFile(p0, p1);
		}

	}

	partial class StreamAssetPathFetcher
	{
		protected override unsafe void Close(global::Java.Lang.Object p0)
		{
			Close((Android.OS.ParcelFileDescriptor)p0);
		}

		protected override Java.Lang.Object LoadResource(Android.Content.Res.AssetManager p0, string p1)
		{
			var handle = Android.Runtime.InputStreamAdapter.ToLocalJniHandle(LoadFile(p0, p1));

			try
			{
				return new Java.Lang.Object(handle, Android.Runtime.JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
				Android.Runtime.JNIEnv.DeleteLocalRef(handle);
			}
		}
	}

	partial class StreamLocalUriFetcher
	{
		protected override unsafe void Close(global::Java.Lang.Object p0)
		{
			Close((Android.OS.ParcelFileDescriptor)p0);
		}

		protected override unsafe Java.Lang.Object LoadResource(global::Android.Net.Uri p0, global::Android.Content.ContentResolver p1)
		{
			var handle = Android.Runtime.InputStreamAdapter.ToLocalJniHandle(LoadFile(p0, p1));

			try
			{
				return new Java.Lang.Object(handle, Android.Runtime.JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
				Android.Runtime.JNIEnv.DeleteLocalRef(handle);
			}
		}

	}
}

namespace Bumptech.Glide.Load.Model
{
	public partial class AssetUriLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData((Android.Net.Uri)model, width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles((global::Android.Net.Uri)model);
		}
	}

	partial class ByteArrayLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData(model?.ToArray<byte>(), width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles(model?.ToArray<byte>());
		}
	}
	partial class DataUriLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData(model?.ToString(), width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles(model?.ToString());
		}
	}
	partial class DataUrlLoader
	{
		public unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData(model?.ToString(), width, height, options);
		}

		public unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles(model?.ToString());
		}
	}
	partial class StringLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData(model?.ToString(), width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles(model?.ToString());
		}
	}
	partial class FileLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData((Java.IO.File)model, width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles((Java.IO.File)model);
		}
	}
	partial class UriLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData((Android.Net.Uri)model, width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles((Android.Net.Uri)model);
		}
	}
	partial class UrlUriLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData((Android.Net.Uri)model, width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles((Android.Net.Uri)model);
		}
	}
	partial class ResourceLoader
	{
		public virtual unsafe Model.ModelLoaderLoadData BuildLoadData(Java.Lang.Object model, int width, int height, Load.Options options)
		{
			return BuildLoadData((Java.Lang.Integer)model, width, height, options);
		}

		public virtual unsafe bool Handles(Java.Lang.Object model)
		{
			return Handles((Java.Lang.Integer)model);
		}
	}
}

namespace Bumptech.Glide.Load.Resource.Bitmap
{
	partial class BitmapDrawableEncoder
	{
		public virtual unsafe bool Encode(Java.Lang.Object data, Java.IO.File file, Load.Options options)
		{
			return Encode((Load.Engine.IResource)data, file, options);
		}
	}

	partial class BitmapEncoder
	{
		public virtual unsafe bool Encode(Java.Lang.Object data, Java.IO.File file, Load.Options options)
		{
			return Encode((Load.Engine.IResource)data, file, options);
		}
	}

	partial class StreamBitmapDecoder : Load.IResourceDecoder
	{
		Engine.IResource Load.IResourceDecoder.Decode(Java.Lang.Object p0, int p1, int p2, Load.Options p3)
		{
			var stream = Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);

			return Java.Interop.JavaObjectExtensions.JavaCast<Engine.IResource>(Decode(stream, p1, p2, p3));
		}

		bool Load.IResourceDecoder.Handles(global::Java.Lang.Object p0, Load.Options p1)
		{
			var stream = Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);

			return Handles(stream, p1);
		}
	}
}


namespace Bumptech.Glide.Load.Resource.Gif
{
	partial class GifDrawableEncoder
	{
		public virtual unsafe bool Encode(Java.Lang.Object data, Java.IO.File file, Load.Options options)
		{
			return Encode((Load.Engine.IResource)data, file, options);
		}
	}
}

namespace Bumptech.Glide.Request.Target
{
	partial class AppWidgetTarget
	{
		public override unsafe void OnResourceReady(Java.Lang.Object resource, Transition.ITransition transition)
		{
			OnResourceReady((Android.Graphics.Bitmap)resource, transition);
		}
	}

	partial class BitmapImageViewTarget
	{
		protected override unsafe void SetResource(Java.Lang.Object resource)
		{
			SetResource((Android.Graphics.Bitmap)resource);
		}
	}

	partial class DrawableThumbnailImageViewTarget
	{
		protected override unsafe Android.Graphics.Drawables.Drawable GetDrawable(Java.Lang.Object resource)
		{
			return GetDrawable((Android.Graphics.Drawables.Drawable)resource);
		}
	}

	partial class BitmapThumbnailImageViewTarget
	{
		protected override unsafe Android.Graphics.Drawables.Drawable GetDrawable(Java.Lang.Object resource)
		{
			return GetDrawable((Android.Graphics.Drawables.Drawable)resource);
		}
	}

	partial class DrawableImageViewTarget
	{
		protected override unsafe void SetResource(Java.Lang.Object resource)
		{
			SetResource((Android.Graphics.Bitmap)resource);
		}
	}

	partial class NotificationTarget
	{
		public override unsafe void OnResourceReady(Java.Lang.Object resource, Transition.ITransition transition)
		{
			OnResourceReady((Android.Graphics.Bitmap)resource, transition);
		}
	}
}

namespace Bumptech.Glide.Load.Engine.Bitmap_recycle
{
	partial class SizeConfigStrategy
	{
		partial class KeyPool
		{
			protected override unsafe Java.Lang.Object Create()
			{
				return CreateKey();
			}
		}
	}
}

namespace Bumptech.Glide.Load.Data
{
	partial class InputStreamRewinder : Data.IDataRewinder
	{
		Java.Lang.Object Data.IDataRewinder.RewindAndGet()
		{
			var handle = Android.Runtime.InputStreamAdapter.ToLocalJniHandle(RewindAndGet());

			try
			{
				return new Java.Lang.Object(handle, Android.Runtime.JniHandleOwnership.TransferLocalRef);
			}
			finally
			{
				Android.Runtime.JNIEnv.DeleteLocalRef(handle);
			}
		}

		partial class Factory : Data.IDataRewinderFactory
		{
			Data.IDataRewinder Data.IDataRewinderFactory.Build(Java.Lang.Object p0)
			{
				var obj = Build(Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer));

				return global::Java.Interop.JavaObjectExtensions.JavaCast<Data.IDataRewinder>(obj);
			}
		}
	}
}

namespace Bumptech.Glide.Load.Model
{
	partial class StreamEncoder : Load.IEncoder
	{
		bool Load.IEncoder.Encode(Java.Lang.Object p0, Java.IO.File p1, Load.Options p2)
		{
			var stream = Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);
			return Encode(stream, p1, p2);
		}
	}
}

namespace Bumptech.Glide.Load.Resource.Bytes
{
	partial class BytesResource : Engine.IResource
	{
		Java.Lang.Object Engine.IResource.Get()
		{
			var bytes = Get();

			return Android.Runtime.JavaArray<byte>.FromArray<byte>(bytes);
		}
	}
}

namespace Bumptech.Glide.Load.Engine.Bitmap_recycle
{
	partial class IntegerArrayAdapter : IArrayAdapterInterface
	{
		int IArrayAdapterInterface.GetArrayLength(Java.Lang.Object p0)
		{
			return GetArrayLength(p0.ToArray<int>());
		}

		Java.Lang.Object IArrayAdapterInterface.NewArray(int p0)
		{
			var ints = NewArray(p0);
			return Android.Runtime.JavaArray<int>.FromArray<int>(ints);
		}
	}

	partial class ByteArrayAdapter : IArrayAdapterInterface
	{
		int IArrayAdapterInterface.GetArrayLength(Java.Lang.Object p0)
		{
			return GetArrayLength(p0.ToArray<byte>());
		}

		Java.Lang.Object IArrayAdapterInterface.NewArray(int p0)
		{
			var bytes = NewArray(p0);
			return Android.Runtime.JavaArray<byte>.FromArray<byte>(bytes);
		}
	}
}

namespace Bumptech.Glide.Load.Resource.Gif
{
	partial class StreamGifDecoder : Load.IResourceDecoder
	{
		Engine.IResource Load.IResourceDecoder.Decode(Java.Lang.Object p0, int p1, int p2, Load.Options p3)
		{
			var stream = Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);

			return Java.Interop.JavaObjectExtensions.JavaCast<Engine.IResource>(Decode(stream, p1, p2, p3));
		}

		bool Load.IResourceDecoder.Handles(global::Java.Lang.Object p0, Load.Options p1)
		{
			var stream = Android.Runtime.InputStreamInvoker.FromJniHandle(p0.Handle, Android.Runtime.JniHandleOwnership.DoNotTransfer);

			return Handles(stream, p1);
		}
	}
}