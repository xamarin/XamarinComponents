using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VKontakte.Core;
using VKontakte.API.Models;
using VKontakte.Views;
using VKontakte.Image;

namespace VKontakteSampleiOS
{
	partial class TestViewController : UITableViewController
	{
		private const int OwnerId = 336001037;
		private const int FriendId = 336317257;
		private const int AlbumId = 224233810;
		private const string WallPostId = "336001037_5";
		private const int FollowersId = 43194319;
		private const string PhotoId = "336001037_390461056";
		private const string PhotoId2 = "336001037_390871182";
		private const string PhotoId3 = "336001037_390597162";

		private const string AllUserFields = "id,first_name,last_name,sex,bdate,city,country,photo_50,photo_100,photo_200_orig,photo_200,photo_400_orig,photo_max,photo_max_orig,online,online_mobile,lists,domain,has_mobile,contacts,connections,site,education,universities,schools,can_post,can_see_all_posts,can_see_audio,can_write_private_message,status,last_seen,common_count,relation,relatives,counters";

		private VKRequest callingRequest;

		public TestViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.HidesBackButton = true;

			TableView.TableFooterView = new UIView ();
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);

			switch (indexPath.Row) {
			case 0:
				// users.get
				CallMethod (VKApi.Users.Get (new NSMutableDictionary<NSString, NSObject> {
					{ VKApiConst.Fields, (NSString)"first_name, last_name, uid, photo_100" }, 
					{ VKApiConst.UserId, (NSNumber)OwnerId }
				}));
				break;
			case 1:
				// friends.get
				CallMethod (VKApi.Friends.Get ());
				break;
			case 2:
				// friends.get with fields
				var friendsRequest = VKApi.Friends.Get (new NSMutableDictionary<NSString, NSObject> {
					{ VKApiConst.Fields , (NSString)AllUserFields }
				});
				CallMethod (friendsRequest);
				break;
			case 3:
				// subscribers
				CallMethod (VKRequest.Create<VKUsersArray> ("users.getFollowers", new NSMutableDictionary<NSString, NSObject> {
					{ VKApiConst.UserId, (NSNumber)FollowersId }, 
					{ VKApiConst.Count, (NSNumber)100 }, 
					{ VKApiConst.Fields, (NSString)AllUserFields } 
				}));
				break;
			case 4:
				// Upload photo to wall
				UploadWallPhoto ();
				break;
			case 5:
				// Upload photo to album
				UploadAlbumPhoto ();
				break;
			case 6:
				// Upload several photos to wall
				UploadSeveralWallPhotos ();
				break;
			case 7:
				// Test captcha
				var request = new VKApiCaptcha ().Force ();
				request.Execute (
					resp => Console.WriteLine ("Result: " + resp), 
					error => Console.WriteLine ("Error: " + error));
				break;
			case 8:
				// Call unknown method
				CallMethod (VKRequest.Create ("I.am.Lord.Voldemort", null));
				break;
			case 9:
				// Test validation
				CallMethod (VKRequest.Create ("account.testValidation", null));
				break;
			case 10:
				// Test share dialog
				var shareDialog = new VKShareDialogController ();
				shareDialog.Text = "This post made with #vksdk #xamarin #ios";
				shareDialog.Images = new [] { PhotoId, PhotoId2, PhotoId3 };
				shareDialog.ShareLink = new VKShareLink ("Super puper link, but nobody knows", new NSUrl ("https://vk.com/dev/ios_sdk"));
				shareDialog.DismissAutomatically = true;
				PresentViewController (shareDialog, true, null);
				break;
			case 11:
				// Test VKActivity
				var items = new NSObject [] {
					UIImage.FromBundle ("apple"), 
					(NSString)"This post made with #vksdk activity #xamarin #ios", 
					new NSUrl ("https://vk.com/dev/ios_sdk")
				};
				var activityViewController = new UIActivityViewController (items, new []{ new VKActivity () });
				activityViewController.SetValueForKey ((NSString)"VK SDK", (NSString)"subject");
				activityViewController.CompletionHandler = null;
				if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
					var popover = activityViewController.PopoverPresentationController;
					if (popover != null) {
						popover.SourceView = View;
						popover.SourceRect = tableView.RectForRowAtIndexPath (indexPath);
					}
				}
				PresentViewController (activityViewController, false, null);
				break;
			case 12:
				// Test app request
				CallMethod (VKRequest.Create ("apps.sendRequest", new NSMutableDictionary<NSString, NSObject> {
					{ (NSString)"user_id", (NSNumber)FriendId }, 
					{ (NSString)"text", (NSString)"Yo ho ho" }, 
					{ (NSString)"type", (NSString)"request" }, 
					{ (NSString)"name", (NSString)"I need more gold" }, 
					{ (NSString)"key", (NSString)"more_gold" }
				}));
				break;
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "ApiCall") {
				var vc = (ApiCallViewController)segue.DestinationViewController;
				vc.CallingRequest = callingRequest;
				callingRequest = null;
			}
		}

		private void CallMethod(VKRequest method)
		{
			callingRequest = method;
			PerformSegue ("ApiCall", this);
		}

		private async void UploadWallPhoto ()
		{
			var request = VKApi.UploadWallPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), 0, 60479154);
			try {
				// upload
				var response = await request.ExecuteAsync ();
				Console.WriteLine ("Photo: " + response.Json);

				// post photo
				var photoInfo = ((VKPhotoArray)response.ParsedModel).ObjectAtIndex<VKPhoto> (0);
				var photoAttachment = string.Format ("photo{0}_{1}", photoInfo.owner_id, photoInfo.id);
				var post = VKApi.Wall.Post (new NSMutableDictionary<NSString, NSObject> {
					{ VKApiConst.Attachment, (NSString)photoAttachment }, 
					{ VKApiConst.OwnerId, (NSNumber)OwnerId }
				});
				var postResponse = await post.ExecuteAsync ();
				Console.WriteLine ("Result: " + postResponse.Json);

				// open link
				var postId = ((NSDictionary)postResponse.Json) ["post_id"];
				UIApplication.SharedApplication.OpenUrl (new NSUrl (string.Format ("http://vk.com/wall{0}_{1}", OwnerId, postId)));
			} catch (VKException ex) {
				Console.WriteLine ("Error: " + ex.Error);
			}
		}

		private async void UploadAlbumPhoto ()
		{
			var request = VKApi.UploadAlbumPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), AlbumId, OwnerId);
			try {
				var response = await request.ExecuteAsync ();
				Console.WriteLine ("Result: " + response.Json);

				var photoInfo = ((VKPhotoArray)response.ParsedModel).ObjectAtIndex<VKPhoto> (0);
				UIApplication.SharedApplication.OpenUrl (new NSUrl (string.Format ("http://vk.com/photo{0}_{1}", OwnerId, photoInfo.id)));
			} catch (VKException ex) {
				Console.WriteLine ("Error: " + ex.Error);
			}
		}

		private async void UploadSeveralWallPhotos ()
		{
			var batch = new VKBatchRequest (new [] {
				VKApi.UploadWallPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), 0, OwnerId),
				VKApi.UploadWallPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), 0, OwnerId),
				VKApi.UploadWallPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), 0, OwnerId),
				VKApi.UploadWallPhotoRequest (UIImage.FromBundle ("apple"), VKImageParameters.PngImage (), 0, OwnerId)
			});

			try {
				// upload
				var responses = await batch.ExecuteAsync ();
				Console.WriteLine ("Photos: " + string.Concat (responses.Select (r => r.Json)));

				// create attachments
				var attachments = new List<string> ();
				foreach (var response in responses) {
					var photoInfo = ((VKPhotoArray)response.ParsedModel).ObjectAtIndex<VKPhoto> (0);
					var attachment = string.Format ("photo{0}_{1}", photoInfo.owner_id, photoInfo.id);
					attachments.Add (attachment);
				}

				// post photos
				var post = VKApi.Wall.Post (new NSMutableDictionary<NSString, NSObject> {
					{ VKApiConst.Attachments, (NSString)string.Join (",", attachments) }, 
					{ VKApiConst.OwnerId, (NSNumber)OwnerId }
				});
				var postResponse = await post.ExecuteAsync ();
				Console.WriteLine ("Result: " + postResponse.Json);

				// open link
				var postId = ((NSDictionary)postResponse.Json) ["post_id"];
				UIApplication.SharedApplication.OpenUrl (new NSUrl (string.Format ("http://vk.com/wall{0}_{1}", OwnerId, postId)));
			} catch (VKException ex) {
				Console.WriteLine ("Error: " + ex.Error);
			}
		}

		partial void OnLogout (UIBarButtonItem sender)
		{
			VKSdk.ForceLogout ();
			NavigationController.PopToRootViewController (true);
		}
	}
}
