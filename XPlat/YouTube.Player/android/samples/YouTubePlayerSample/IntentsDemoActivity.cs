using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using YouTube.Player;

using YouTubePlayerSample.Adapters;

namespace YouTubePlayerSample
{
	// A sample activity which shows how to use the YouTubeIntents static methods to create
	// Intents that navigate the user to Activities within the main YouTube application.
	[Activity(Label = "@string/intents_demo_name")]
	[MetaData("@string/minVersion", Value = "8")]
	[MetaData("@string/isLaunchableActivity", Value = "true")]
	public class IntentsDemoActivity : Activity
	{
		private const string VideoId = "-Uwjt32NvVA";
		private const string PlaylistId = "PLF3DFB800F05F551A";
		private const string UserId = "Google";
		private const string ChannelId = "UCVHFbqXqoYvEWM1Ddxl0QDg";

		private const int SelectVideoRequest = 1000;

		private List<DemoListViewItem> intentItems;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.intents_demo);

			intentItems = new List<DemoListViewItem>
			{
				new IntentItem(this, "Play Video", IntentType.PlayVideo),
				new IntentItem(this, "Open Playlist", IntentType.OpenPlaylist),
				new IntentItem(this, "Play Playlist", IntentType.PlayPlaylist),
				new IntentItem(this, "Open User", IntentType.OpenUser),
				new IntentItem(this, "Open Channel", IntentType.OpenChannel),
				new IntentItem(this, "Open Search Results", IntentType.OpenSearch),
				new IntentItem(this, "Upload Video", IntentType.UploadVideo),
			};

			var listView = FindViewById<ListView>(Resource.Id.intent_list);
			var adapter = new DemoArrayAdapter(this, Resource.Layout.list_item, intentItems);
			listView.Adapter = adapter;
			listView.ItemClick += OnItemClick;

			var youTubeVersionText = FindViewById<TextView>(Resource.Id.youtube_version_text);
			var version = YouTubeIntents.GetInstalledYouTubeVersionName(this);
			if (version != null)
			{
				var text = string.Format(GetString(Resource.String.youtube_currently_installed), version);
				youTubeVersionText.Text = text;
			}
			else
			{
				youTubeVersionText.Text = GetString(Resource.String.youtube_not_installed);
			}
		}

		private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedIntentItem = (IntentItem)intentItems[e.Position];

			Intent intent;
			switch (clickedIntentItem.Type)
			{
				case IntentType.PlayVideo:
					intent = YouTubeIntents.CreatePlayVideoIntentWithOptions(this, VideoId, true, false);
					StartActivity(intent);
					break;
				case IntentType.OpenPlaylist:
					intent = YouTubeIntents.CreateOpenPlaylistIntent(this, PlaylistId);
					StartActivity(intent);
					break;
				case IntentType.PlayPlaylist:
					intent = YouTubeIntents.CreatePlayPlaylistIntent(this, PlaylistId);
					StartActivity(intent);
					break;
				case IntentType.OpenSearch:
					intent = YouTubeIntents.CreateSearchIntent(this, UserId);
					StartActivity(intent);
					break;
				case IntentType.OpenUser:
					intent = YouTubeIntents.CreateUserIntent(this, UserId);
					StartActivity(intent);
					break;
				case IntentType.OpenChannel:
					intent = YouTubeIntents.CreateChannelIntent(this, ChannelId);
					StartActivity(intent);
					break;
				case IntentType.UploadVideo:
					// This will load a picker view in the users' gallery.
					// The upload activity is started in the function onActivityResult.
					intent = new Intent(Intent.ActionPick, null).SetType("video/*");
					intent.PutExtra(Intent.ExtraLocalOnly, true);
					StartActivityForResult(intent, SelectVideoRequest);
					break;
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (resultCode == Result.Ok)
			{
				if (requestCode == SelectVideoRequest)
				{
					Intent intent = YouTubeIntents.CreateUploadIntent(this, data.Data);
					StartActivity(intent);
				}
			}

			base.OnActivityResult(requestCode, resultCode, data);
		}

		private enum IntentType
		{
			PlayVideo,
			OpenPlaylist,
			PlayPlaylist,
			OpenUser,
			OpenChannel,
			OpenSearch,
			UploadVideo
		}

		private class IntentItem : DemoListViewItem
		{
			private readonly Context context;
			private readonly string title;
			private readonly IntentType type;

			public IntentItem(Context context, string title, IntentType type)
			{
				this.context = context;
				this.title = title;
				this.type = type;
			}

			public IntentType Type => type;

			public override string Title => title;

			public override bool IsEnabled => IsIntentTypeEnabled(type);

			public override string DisabledText => context.GetString(Resource.String.intent_disabled);

			public bool IsIntentTypeEnabled(IntentType type)
			{
				switch (type)
				{
					case IntentType.PlayVideo:
						return YouTubeIntents.CanResolvePlayVideoIntent(context);
					case IntentType.OpenPlaylist:
						return YouTubeIntents.CanResolveOpenPlaylistIntent(context);
					case IntentType.PlayPlaylist:
						return YouTubeIntents.CanResolvePlayPlaylistIntent(context);
					case IntentType.OpenSearch:
						return YouTubeIntents.CanResolveSearchIntent(context);
					case IntentType.OpenUser:
						return YouTubeIntents.CanResolveUserIntent(context);
					case IntentType.OpenChannel:
						return YouTubeIntents.CanResolveChannelIntent(context);
					case IntentType.UploadVideo:
						return YouTubeIntents.CanResolveUploadIntent(context);
				}

				return false;
			}
		}
	}
}
