using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Fragment = Android.Support.V4.App.Fragment;
using Uri = Android.Net.Uri;
using Path = System.IO.Path;

using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.API.Photos;
using VKontakte.Payments;
using VKontakte.Dialogs;
using System.Linq;
using Newtonsoft.Json;

namespace VKontakteSampleAndroid
{
	[Activity (Label = "VKontakte API Call Tests")]
	public class ApiCallActivity : AppCompatActivity
	{
		private VKRequest myRequest;

		private const string FragmentTag = "response_view";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_api_call);

			if (SupportActionBar != null) {
				SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			}

			if (savedInstanceState == null) {
				SupportFragmentManager
					.BeginTransaction ()
					.Add (Resource.Id.container, new PlaceholderFragment (), FragmentTag)
					.Commit ();
				ProcessRequestIfRequired ();
			}
		}

		private PlaceholderFragment GetFragment ()
		{
			return (PlaceholderFragment)SupportFragmentManager.FindFragmentByTag (FragmentTag);
		}

		private void ProcessRequestIfRequired ()
		{
			VKRequest request = null;

			if (Intent != null && Intent.Extras != null && Intent.HasExtra ("request")) {
				long requestId = Intent.Extras.GetLong ("request");
				request = VKRequest.GetRegisteredRequest (requestId);
				if (request != null)
					request.UnregisterObject ();
			}

			if (request != null) {
				myRequest = request;
				request.ExecuteWithListener (OnRequestComplete, OnRequestError, OnRequestProgress, OnRequestAttemptFailed);
			}
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			base.OnSaveInstanceState (outState);

			outState.PutString ("response", GetFragment ().TextView.Text);
			if (myRequest != null) {
				outState.PutLong ("request", myRequest.RegisterObject ());
			}
		}

		protected override void OnRestoreInstanceState (Bundle savedInstanceState)
		{
			base.OnRestoreInstanceState (savedInstanceState);
		
			var response = savedInstanceState.GetString ("response");
			SetResponseText (response);

			long requestId = savedInstanceState.GetLong ("request");
			myRequest = VKRequest.GetRegisteredRequest (requestId);
			if (myRequest != null) {
				myRequest.UnregisterObject ();
				myRequest.SetRequestListener (OnRequestComplete, OnRequestError, OnRequestProgress, OnRequestAttemptFailed);
			}
		}

		protected void SetResponseText (string text)
		{
			var fragment = GetFragment ();
			if (fragment != null && fragment.TextView != null) {
				fragment.TextView.Text = text ?? string.Empty;
			}
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			myRequest.Cancel ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home) {
				Finish ();
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}

		private void OnRequestComplete (VKResponse response)
		{
			// apply JSON formatting
			using (var stringReader = new StringReader (response.Json.ToString ()))
			using (var stringWriter = new StringWriter ())
			using (var jsonReader = new JsonTextReader (stringReader))
			using (var jsonWriter = new JsonTextWriter (stringWriter) { Formatting = Formatting.Indented }) {
				jsonWriter.WriteToken (jsonReader);
				SetResponseText (stringWriter.ToString ());
			}
		}

		private void OnRequestError (VKError error)
		{
			SetResponseText (error.ToString ());
		}

		private void OnRequestProgress (VKRequest.VKProgressType progressType, long bytesLoaded, long bytesTotal)
		{
			Console.WriteLine ("Request progress: " + ((double)bytesLoaded / (double)bytesTotal).ToString ("P"));
		}

		private void OnRequestAttemptFailed (VKRequest request, int attemptNumber, int totalAttempts)
		{
			var line = string.Format ("Attempt {0}/{1} failed.\n", attemptNumber, totalAttempts);
			GetFragment ().TextView.Append (line);
		}

		/// <summary>
		/// A placeholder fragment containing a simple view.
		/// </summary>
		private class PlaceholderFragment : Fragment
		{
			public TextView TextView { get; private set; }

			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				var view = inflater.Inflate (Resource.Layout.fragment_api_call, container, false);
				TextView = view.FindViewById<TextView> (Resource.Id.response);
				return view;
			}
		}
	}
}
