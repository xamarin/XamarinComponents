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
		// This is the value of Intent.EXTRA_LOCAL_ONLY for API level 11 and above.
		private const string EXTRA_LOCAL_ONLY = "android.intent.extra.LOCAL_ONLY";
		private const string VIDEO_ID = "-Uwjt32NvVA";
		private const string PLAYLIST_ID = "PLF3DFB800F05F551A";
		private const string USER_ID = "Google";
		private const string CHANNEL_ID = "UCVHFbqXqoYvEWM1Ddxl0QDg";
		private const int SELECT_VIDEO_REQUEST = 1000;

		private List<DemoListViewItem> intentItems;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.intents_demo);

			intentItems = new List<DemoListViewItem>
			{
				new IntentItem(this, "Play Video", IntentType.PLAY_VIDEO),
				new IntentItem(this, "Open Playlist", IntentType.OPEN_PLAYLIST),
				new IntentItem(this, "Play Playlist", IntentType.PLAY_PLAYLIST),
				new IntentItem(this, "Open User", IntentType.OPEN_USER),
				new IntentItem(this, "Open Channel", IntentType.OPEN_CHANNEL),
				new IntentItem(this, "Open Search Results", IntentType.OPEN_SEARCH),
				new IntentItem(this, "Upload Video", IntentType.UPLOAD_VIDEO),
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
				case IntentType.PLAY_VIDEO:
					intent = YouTubeIntents.CreatePlayVideoIntentWithOptions(this, VIDEO_ID, true, false);
					StartActivity(intent);
					break;
				case IntentType.OPEN_PLAYLIST:
					intent = YouTubeIntents.CreateOpenPlaylistIntent(this, PLAYLIST_ID);
					StartActivity(intent);
					break;
				case IntentType.PLAY_PLAYLIST:
					intent = YouTubeIntents.CreatePlayPlaylistIntent(this, PLAYLIST_ID);
					StartActivity(intent);
					break;
				case IntentType.OPEN_SEARCH:
					intent = YouTubeIntents.CreateSearchIntent(this, USER_ID);
					StartActivity(intent);
					break;
				case IntentType.OPEN_USER:
					intent = YouTubeIntents.CreateUserIntent(this, USER_ID);
					StartActivity(intent);
					break;
				case IntentType.OPEN_CHANNEL:
					intent = YouTubeIntents.CreateChannelIntent(this, CHANNEL_ID);
					StartActivity(intent);
					break;
				case IntentType.UPLOAD_VIDEO:
					// This will load a picker view in the users' gallery.
					// The upload activity is started in the function onActivityResult.
					intent = new Intent(Intent.ActionPick, null).SetType("video/*");
					intent.PutExtra(EXTRA_LOCAL_ONLY, true);
					StartActivityForResult(intent, SELECT_VIDEO_REQUEST);
					break;
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (resultCode == Result.Ok)
			{
				switch (requestCode)
				{
					case SELECT_VIDEO_REQUEST:
						Intent intent = YouTubeIntents.CreateUploadIntent(this, data.Data);
						StartActivity(intent);
						break;
				}
			}
			base.OnActivityResult(requestCode, resultCode, data);
		}

		private enum IntentType
		{
			PLAY_VIDEO,
			OPEN_PLAYLIST,
			PLAY_PLAYLIST,
			OPEN_USER,
			OPEN_CHANNEL,
			OPEN_SEARCH,
			UPLOAD_VIDEO
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
					case IntentType.PLAY_VIDEO:
						return YouTubeIntents.CanResolvePlayVideoIntent(context);
					case IntentType.OPEN_PLAYLIST:
						return YouTubeIntents.CanResolveOpenPlaylistIntent(context);
					case IntentType.PLAY_PLAYLIST:
						return YouTubeIntents.CanResolvePlayPlaylistIntent(context);
					case IntentType.OPEN_SEARCH:
						return YouTubeIntents.CanResolveSearchIntent(context);
					case IntentType.OPEN_USER:
						return YouTubeIntents.CanResolveUserIntent(context);
					case IntentType.OPEN_CHANNEL:
						return YouTubeIntents.CanResolveChannelIntent(context);
					case IntentType.UPLOAD_VIDEO:
						return YouTubeIntents.CanResolveUploadIntent(context);
				}

				return false;
			}
		}
	}
}
