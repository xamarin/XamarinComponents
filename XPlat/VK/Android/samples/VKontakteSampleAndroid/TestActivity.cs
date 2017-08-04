using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Path = System.IO.Path;
using Uri = Android.Net.Uri;

using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.API.Photos;
using VKontakte.Payments;
using VKontakte.Dialogs;

namespace VKontakteSampleAndroid
{
	[Activity (Label = "VKontakte Tests")]
	public class TestActivity : AppCompatActivity
	{
		private const int OwnerId = 336001037;
		private const int AlbumId = 224233810;
		private const string WallPostId = "336001037_5";
		private const string PhotoId = "photo336001037_390461056";

		protected override async void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_test);

			if (SupportActionBar != null) {
				SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			}
			if (savedInstanceState == null) {
				SupportFragmentManager
					.BeginTransaction ()
					.Add (Resource.Id.container, new PlaceholderFragment ())
					.Commit ();
			}

			var userIsVk = await VKSdk.RequestUserStateAsync (this);
			Toast.MakeText (this, userIsVk ? "user is vk's" : "user is not vk's", ToastLength.Short).Show ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home) {
				Finish ();
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}

		/// <summary>
		/// A placeholder fragment containing a simple view.
		/// </summary>
		public class PlaceholderFragment : Android.Support.V4.App.Fragment
		{
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				var view = inflater.Inflate (Resource.Layout.fragment_test, container, false);
				view.FindViewById (Resource.Id.test_send_request).Click += SendRequest;
				view.FindViewById (Resource.Id.users_get).Click += GetUsers;
				view.FindViewById (Resource.Id.friends_get).Click += GetFriends;
				view.FindViewById (Resource.Id.messages_get).Click += GetMessages;
				view.FindViewById (Resource.Id.dialogs_get).Click += GetDialogs;
				view.FindViewById (Resource.Id.captcha_force).Click += Captcha;
				view.FindViewById (Resource.Id.upload_photo).Click += UploadSinglePhoto;
				view.FindViewById (Resource.Id.wall_post).Click += WallPost;
				view.FindViewById (Resource.Id.wall_getById).Click += GetWallPost;
				view.FindViewById (Resource.Id.test_validation).Click += Validation;
				view.FindViewById (Resource.Id.test_share).Click += Share;
				view.FindViewById (Resource.Id.upload_photo_to_wall).Click += UploadSinglePhotoToWall;
				view.FindViewById (Resource.Id.upload_doc).Click += UploadDoc;
				view.FindViewById (Resource.Id.upload_several_photos_to_wall).Click += UploadMultiplePhotosToWall;
				return view;
			}

			// text cases

			private void GetFriends (object sender, EventArgs e)
			{
				var request = VKApi.Friends.Get (new VKParameters (new Dictionary<string, Java.Lang.Object> { 
					{ VKApiConst.Fields, "id,first_name,last_name,sex,bdate,city" }
				}));
				StartApiCall (request);
			}

			private void GetUsers (object sender, EventArgs e)
			{
				var request = VKApi.Users.Get (new VKParameters (new Dictionary<string, Java.Lang.Object> { {
						VKApiConst.Fields,
						"id,first_name,last_name,sex,bdate,city,country,photo_50,photo_100,photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig,online,online_mobile,lists,domain,has_mobile,contacts,connections,site,education,universities,schools,can_post,can_see_all_posts,can_see_audio,can_write_private_message,status,last_seen,common_count,relation,relatives,counters"
					}
				}));
				request.Secure = false;
				request.UseSystemLanguage = false;
				StartApiCall (request);
			}

			private void SendRequest (object sender, EventArgs e)
			{
				MakeRequest ();
			}

			private void GetDialogs (object sender, EventArgs e)
			{
				var request = VKApi.Messages.GetDialogs ();
				StartApiCall (request);
			}

			private void GetMessages (object sender, EventArgs e)
			{
				var request = VKApi.Messages.Get ();
				StartApiCall (request);
			}

			private void Captcha (object sender, EventArgs e)
			{
				var request = new VKApiCaptcha ().Force ();
				StartApiCall (request);
			}

			private async void UploadSinglePhoto (object sender, EventArgs e)
			{
				var photo = GetPhoto ();
				var request = VKApi.UploadAlbumPhotoRequest (new VKUploadImage (photo, VKImageParameters.PngImage ()), AlbumId, OwnerId);

				try {
					var response = await request.ExecuteAsync ();
					RecycleBitmap (photo);

					var photoArray = (VKPhotoArray)response.ParsedModel;
					var uri = string.Format ("https://vk.com/photo{0}_{1}", OwnerId, photoArray [0].id);
					var i = new Intent (Intent.ActionView, Uri.Parse (uri));
					StartActivity (i);
				} catch (VKException ex) {
					ShowError (ex.Error);
				}
			}

			private void WallPost (object sender, EventArgs e)
			{
				MakePost ("Hello, friends! (From Xamarin.Android)");
			}

			private void GetWallPost (object sender, EventArgs e)
			{
				var request = VKApi.Wall.GetById (VKParameters.From (VKApiConst.Posts, WallPostId));
				StartApiCall (request);
			}

			private void Validation (object sender, EventArgs e)
			{
				var request = new VKRequest ("account.testValidation");
				StartApiCall (request);
			}

			private void Share (object sender, EventArgs e)
			{
				var photo = GetPhoto ();
				new VKShareDialog ()
					.SetText ("I created this post with VK Xamarin.Android SDK\nSee additional information below\n#vksdk #xamarin #componentstore")
					.SetUploadedPhotos (new VKPhotoArray {
						new VKApiPhoto (PhotoId)
					})
					.SetAttachmentImages (new [] {
						new VKUploadImage (photo, VKImageParameters.PngImage ())
					})
					.SetAttachmentLink ("VK Android SDK information", "https://vk.com/dev/android_sdk")
					.SetShareDialogListener (
						postId => {
							RecycleBitmap (photo);
						},
						() => {
							RecycleBitmap (photo);
						},
						error => {
							RecycleBitmap (photo);
						})
					.Show (Activity.SupportFragmentManager, "VK_SHARE_DIALOG");
			}

			private async void UploadSinglePhotoToWall (object sender, EventArgs e)
			{
				var photo = GetPhoto ();
				var request = VKApi.UploadWallPhotoRequest (new VKUploadImage (photo, VKImageParameters.JpgImage (0.9f)), 0, OwnerId);
				try {
					var response = await request.ExecuteAsync ();
					RecycleBitmap (photo);
					var photoModel = ((VKPhotoArray)response.ParsedModel) [0];
					MakePost ("Photos from Xamarin.Android!", new VKAttachments (photoModel));
				} catch (VKException ex) {
					ShowError (ex.Error);
				}
			}

			private void UploadDoc (object sender, EventArgs e)
			{
				var request = VKApi.Docs.UploadDocRequest (GetFile ());
				StartApiCall (request);
			}

			private async void UploadMultiplePhotosToWall (object sender, EventArgs e)
			{
				var photo = GetPhoto ();
				var request1 = VKApi.UploadWallPhotoRequest (new VKUploadImage (photo, VKImageParameters.JpgImage (0.9f)), 0, OwnerId);
				var request2 = VKApi.UploadWallPhotoRequest (new VKUploadImage (photo, VKImageParameters.JpgImage (0.5f)), 0, OwnerId);
				var request3 = VKApi.UploadWallPhotoRequest (new VKUploadImage (photo, VKImageParameters.JpgImage (0.1f)), 0, OwnerId);
				var request4 = VKApi.UploadWallPhotoRequest (new VKUploadImage (photo, VKImageParameters.PngImage ()), 0, OwnerId);
				
				var batch = new VKBatchRequest (request1, request2, request3, request4);
				var responses = await batch.ExecuteAsync ();
				try {
					RecycleBitmap (photo);
					var resp = responses.Select (r => ((VKPhotoArray)r.ParsedModel) [0]);
					var attachments = new VKAttachments ();
					attachments.AddRange (resp);
					MakePost ("I just uploaded multiple files from the VK Xamarin.Android SDK!", attachments);
				} catch (VKException ex) {
					ShowError (ex.Error);
				}
			}

			// utillities

			private void StartApiCall (VKRequest request)
			{
				var i = new Intent (Activity, typeof(ApiCallActivity));
				i.PutExtra ("request", request.RegisterObject ());
				StartActivity (i);
			}

			private void ShowError (VKError error)
			{
				new AlertDialog.Builder (Activity)
					.SetMessage (error.ToString ())
					.SetPositiveButton ("OK", delegate {
					})
					.Show ();
				
				if (error.HttpError != null) {
					Console.WriteLine ("Error in request or upload: " + error.HttpError);
				}
			}

			private Bitmap GetPhoto ()
			{
				try {
					return BitmapFactory.DecodeStream (Activity.Assets.Open ("android.jpg"));
				} catch (Exception ex) {
					Console.WriteLine (ex);
					return null;
				}
			}

			private void RecycleBitmap (Bitmap bitmap)
			{
				if (bitmap != null) {
					bitmap.Recycle ();
					bitmap.Dispose ();
				}
			}

			private Java.IO.File GetFile ()
			{
				try {
					var filename = "android.jpg";
					var path = Path.Combine (Activity.CacheDir.AbsolutePath, filename);
					using (var inputStream = Activity.Assets.Open (filename))
					using (var outputStream = File.Open (path, FileMode.Create)) {
						inputStream.CopyTo (outputStream);
					}
					return new Java.IO.File (path);
				} catch (Exception ex) {
					Console.WriteLine (ex);
				}
				return null;
			}

			private async void MakeRequest ()
			{
				var request = new VKRequest ("apps.getFriendsList", new VKParameters (new Dictionary<string, Java.Lang.Object> {
					{ VKApiConst.Extended, 1 }, 
					{ "type", "request" }
				}));
				var response = await request.ExecuteAsync ();

				var context = Context;
				if (context == null || !IsAdded) {
					return;
				}

				try {
					var json = JObject.Parse (response.Json.ToString ());
					var jsonArray = (JArray)json ["response"]["items"];
					var ids = jsonArray.Select (user => (int)user ["id"]).ToArray ();
					var users = jsonArray.Select (j => string.Format ("{0} {1}", j ["first_name"], j ["last_name"])).ToArray ();

					new AlertDialog.Builder (context)
							.SetTitle (Resource.String.send_request_title)
							.SetItems (users, (sender, e) => {
								var parameters = new VKParameters (new Dictionary<string, Java.Lang.Object> {
									{ VKApiConst.UserId, ids [e.Which] }, 
									{ "type", "request" }
								});
								StartApiCall (new VKRequest ("apps.sendRequest", parameters));
							})
							.Create ()
							.Show ();
				} catch (Exception ex) {
					Console.WriteLine (ex);
				}
			}

			private async void MakePost (string message, VKAttachments attachments = null)
			{
				var post = VKApi.Wall.Post (new VKParameters (new Dictionary<string, Java.Lang.Object> { 
					{ VKApiConst.OwnerId, OwnerId },
					{ VKApiConst.Attachments, attachments ?? new VKAttachments () },
					{ VKApiConst.Message, message }
				}));
				post.SetModelClass<VKWallPostResult> ();
				try {
					var response = await post.ExecuteAsync ();
					if (IsAdded) {
						var result = (VKWallPostResult)response.ParsedModel;
						var uri = string.Format ("https://vk.com/wall{0}_{1}", OwnerId, result.post_id);
						var i = new Intent (Intent.ActionView, Uri.Parse (uri));
						StartActivity (i);
					}
				} catch (VKException ex) {
					ShowError (ex.Error);
				}
			}
		}
	}
}
