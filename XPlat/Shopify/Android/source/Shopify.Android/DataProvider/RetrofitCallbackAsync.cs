using System;
using System.Threading.Tasks;
using Android.Runtime;
using Square.Retrofit;
using Square.Retrofit.Client;

namespace Shopify.Buy.DataProvider
{
	internal class RetrofitCallbackAsync<T> : Java.Lang.Object, ICallback
		where T : class, IJavaObject
	{
		private readonly TaskCompletionSource<T> tcs;

		public Task<T> Task { get { return tcs.Task; } }

		public RetrofitCallbackAsync()
		{
			tcs = new TaskCompletionSource<T>();
		}

		public void Failure(RetrofitError error)
		{
			tcs.SetException(new ShopifyException(error));
		}

		public void Success(Java.Lang.Object data, Response response)
		{
			tcs.SetResult(data?.JavaCast<T>());
		}
	}

	internal class RetrofitCallbackAsync<T, U> : Java.Lang.Object, ICallback
		where T : class, IJavaObject
	{
		private readonly TaskCompletionSource<U> tcs;
		private readonly Func<T, U> converter;

		public Task<U> Task { get { return tcs.Task; } }

		public RetrofitCallbackAsync(Func<T, U> converter)
		{
			tcs = new TaskCompletionSource<U>();
			this.converter = converter;
		}

		public void Failure(RetrofitError error)
		{
			tcs.SetException(new ShopifyException(error));
		}

		public void Success(Java.Lang.Object data, Response response)
		{
			tcs.SetResult(converter(data?.JavaCast<T>()));
		}
	}
}
