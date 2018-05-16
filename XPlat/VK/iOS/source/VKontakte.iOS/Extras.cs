using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Foundation;
using VKontakte.Core;
using VKontakte.API.Models;

namespace VKontakte
{
	partial class VKPermissions
	{
		// extern NSArray * VKParseVkPermissionsFromInteger (NSInteger permissionsValue);
		[DllImport ("__Internal", EntryPoint = "VKParseVkPermissionsFromInteger")]
		static extern string[] ParsePermissions (nint permissionsValue);
	}

	public class VKException : Exception
	{
		public VKException (NSError error)
			: this (error.GetVKError ())
		{
			NSError = error;
		}

		public VKException (VKError error)
			: base (error.ErrorMessage)
		{
			Error = error;
		}

		public VKException (string message, VKError error)
			: base (message)
		{
			Error = error;
		}

		public VKException (VKError error, Exception innerException)
			: base (error.ErrorMessage, innerException)
		{
			Error = error;
		}

		public VKException (string message, VKError error, Exception innerException)
			: base (message, innerException)
		{
			Error = error;
		}

		public VKError Error { get; private set; }

		public NSError NSError { get; private set; }
	}
}

namespace VKontakte
{
	partial class VKBatchRequest
	{
		public Task<VKResponse[]> ExecuteAsync ()
		{
			var tcs = new TaskCompletionSource<VKResponse[]> ();
			Execute (response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)));
			return tcs.Task;
		}
	}
}

namespace VKontakte.API.Models
{
	partial class VKApiObjectArray
	{
		public T ObjectAtIndex<T> (nint index) 
			where T : VKApiObject
		{
			return (T)ObjectAtIndex (index);
		}
	}
}

namespace VKontakte.Core
{
	partial class VKError
	{
		public NSError ToNSError ()
		{
			return NSError_VKError.FromVKError (null, this);
		}
	}

	partial class VKRequest
	{
		public Task<VKResponse> ExecuteAsync ()
		{
			var tcs = new TaskCompletionSource<VKResponse> ();
			Execute (response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)));
			return tcs.Task;
		}

		public Task<VKResponse> ExecuteAfterAsync (VKRequest request)
		{
			var tcs = new TaskCompletionSource<VKResponse> ();
			ExecuteAfter (request, response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)));
			return tcs.Task;
		}

		public static VKRequest Create (string method, NSDictionary parameters, Type modelClass)
		{
			return Create (method, parameters, new ObjCRuntime.Class (modelClass));
		}

		public static VKRequest Create<T> (string method, NSDictionary parameters)
			where T : VKApiObject
		{
			return Create (method, parameters, new ObjCRuntime.Class (typeof(T)));
		}
	}
}
