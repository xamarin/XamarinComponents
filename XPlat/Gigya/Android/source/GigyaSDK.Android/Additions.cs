using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;

using GigyaSDK.Socialize;
using GigyaSDK.Socialize.Android;
using GigyaSDK.Socialize.Android.Events;

namespace GigyaSDK.Socialize
{
	partial class GSRequest
	{
		public Task<GSResponse> SendAsync()
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			Send(new GSRespnseActionListener(tcs));
			return tcs.Task;
		}
	}

	internal class GSRespnseActionListener : Java.Lang.Object, IGSResponseListener
	{
		private readonly TaskCompletionSource<GSResponse> tcs;

		public GSRespnseActionListener(TaskCompletionSource<GSResponse> tcs)
		{
			this.tcs = tcs;
		}

		public void OnGSResponse(string method, GSResponse response, Java.Lang.Object context)
		{
			tcs.SetResult(response);
		}
	}

	partial class GSObject
	{
		public Java.Lang.Object this[string key]
		{
			get { return Get(key); }
			set { Put(key, value); }
		}
	}

	partial class GSArray
	{
		public GSObject this[int index]
		{
			get { return GetObject(index); }
		}
	}
}

namespace GigyaSDK.Socialize.Android
{
	partial class GSAPI
	{
		public Task<GSResponse> AddConnectionAsync(Activity activity, GSObject @params)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			AddConnection(activity, @params, new GSRespnseActionListener(tcs), null);
			return tcs.Task;
		}

		public Task<AndroidPermissionsResult> GetAndroidPermissionsAsync()
		{
			var tcs = new TaskCompletionSource<AndroidPermissionsResult>();
			var listener = new GSAndroidPermissionActionListener(tcs);
			listener.RequestCode = GetNextAndroidPermissionsRequestCode(listener);
			return tcs.Task;
		}

		public bool HandleAndroidPermissionsResult(AndroidPermissionsResult result)
		{
			return HandleAndroidPermissionsResult(result.RequestCode, result.Permissions, result.GrantResults);
		}

		public Task<GSResponse> LoginAsync(Activity activity, GSObject @params)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			Login(activity, @params, new GSRespnseActionListener(tcs), null);
			return tcs.Task;
		}

		public Task<GSResponse> LoginAsync(Activity activity, GSObject @params, bool silent)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			Login(activity, @params, new GSRespnseActionListener(tcs), silent, null);
			return tcs.Task;
		}

		public Task<GSResponse> RemoveConnectionAsync(GSObject @params)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			RemoveConnection(@params, new GSRespnseActionListener(tcs), null);
			return tcs.Task;
		}

		public Task<PermissionsResult> RequestNewFacebookPublishPermissionsAsync(IEnumerable<string> permissions)
		{
			var tcs = new TaskCompletionSource<PermissionsResult>();
			RequestNewFacebookPublishPermissions(permissions.ToList(), new GSPermissionResultActionHandler(tcs));
			return tcs.Task;
		}

		public Task<PermissionsResult> RequestNewFacebookReadPermissionsAsync(IEnumerable<string> permissions)
		{
			var tcs = new TaskCompletionSource<PermissionsResult>();
			RequestNewFacebookReadPermissions(permissions.ToList(), new GSPermissionResultActionHandler(tcs));
			return tcs.Task;
		}

		public Task<GSResponse> SendRequestAsync(string method, GSObject @params, int timeoutMS)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			SendRequest(method, @params, new GSRespnseActionListener(tcs), null, timeoutMS);
			return tcs.Task;
		}

		public Task<GSResponse> SendRequestAsync(string method, GSObject @params)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			SendRequest(method, @params, new GSRespnseActionListener(tcs), null);
			return tcs.Task;
		}

		public Task<GSResponse> SendRequestAsync(string method, GSObject @params, bool useHTTPS, int timeoutMS)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			SendRequest(method, @params, useHTTPS, new GSRespnseActionListener(tcs), null, timeoutMS);
			return tcs.Task;
		}

		public Task<GSResponse> SendRequestAsync(string method, GSObject @params, bool useHTTPS)
		{
			var tcs = new TaskCompletionSource<GSResponse>();
			SendRequest(method, @params, useHTTPS, new GSRespnseActionListener(tcs), null);
			return tcs.Task;
		}
	}

	internal class GSPermissionResultActionHandler : Java.Lang.Object, IGSPermissionResultHandler
	{
		private readonly TaskCompletionSource<PermissionsResult> tcs;

		public GSPermissionResultActionHandler(TaskCompletionSource<PermissionsResult> tcs)
		{
			this.tcs = tcs;
		}

		public void OnResult(bool granted, Java.Lang.Exception exception, IList<string> declinedPermissions)
		{
			if (exception != null)
			{
				tcs.SetException(exception);
			}
			else
			{
				var result = new PermissionsResult(granted, declinedPermissions.ToArray());
				tcs.SetResult(result);
			}
		}
	}

	public struct PermissionsResult
	{
		public PermissionsResult(bool granted, string[] declinedPermissions)
		{
			Granted = granted;
			DeclinedPermissions = declinedPermissions;
		}

		public bool Granted { get; private set; }
		public string[] DeclinedPermissions { get; private set; }
	}
}

namespace GigyaSDK.Socialize.Android.Events
{
	internal class GSAndroidPermissionActionListener : Java.Lang.Object, IGSAndroidPermissionListener
	{
		private readonly TaskCompletionSource<AndroidPermissionsResult> tcs;

		public GSAndroidPermissionActionListener(TaskCompletionSource<AndroidPermissionsResult> tcs)
		{
			this.tcs = tcs;
		}

		internal int RequestCode { get; set; }

		public void OnAndroidPermissionsResult(string[] permissions, Permission[] grantResults)
		{
			var result = new AndroidPermissionsResult(RequestCode, permissions, grantResults);
			tcs.SetResult(result);
		}
	}

	public struct AndroidPermissionsResult
	{
		public AndroidPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			RequestCode = requestCode;
			Permissions = permissions;
			GrantResults = grantResults;
		}

		public int RequestCode { get; internal set; }
		public string[] Permissions { get; private set; }
		public Permission[] GrantResults { get; private set; }
	}
}

namespace GigyaSDK.Socialize.Android.Login.UI
{
	[Activity(
		Theme = "@android:style/Theme.Translucent.NoTitleBar",
		ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	partial class HostActivity
	{
	}
}

namespace GigyaSDK.Socialize.Android.Login.Providers
{
	[Activity(
		Theme = "@android:style/Theme.Translucent.NoTitleBar",
		LaunchMode = LaunchMode.SingleTask,
		AllowTaskReparenting = true,
		ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenLayout | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	[IntentFilter(
		new[] { Intent.ActionView },
		Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
		DataScheme = "@PACKAGE_NAME@", DataHost = "gsapi")]
	partial class WebLoginActivity
	{
	}
}
