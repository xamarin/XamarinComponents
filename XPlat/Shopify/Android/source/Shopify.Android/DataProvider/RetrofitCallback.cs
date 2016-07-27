using System;
using Android.Runtime;
using Square.Retrofit;
using Square.Retrofit.Client;

namespace Shopify.Buy.DataProvider
{
	internal class RetrofitCallback<T> : Java.Lang.Object, ICallback
		where T : class, IJavaObject
	{
		private readonly Action<T, Response> onSuccess;
		private readonly Action<RetrofitError> onFailure;

		public RetrofitCallback(Action<T, Response> success, Action<RetrofitError> failure)
		{
			onSuccess = success;
			onFailure = failure;
		}

		public void Failure(RetrofitError error)
		{
			onFailure?.Invoke(error);
		}

		public void Success(Java.Lang.Object data, Response response)
		{
			onSuccess?.Invoke(data?.JavaCast<T>(), response);
		}
	}
}
