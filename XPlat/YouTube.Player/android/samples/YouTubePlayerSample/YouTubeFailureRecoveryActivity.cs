using Android.App;
using Android.Content;
using Android.Widget;

using YouTube.Player;

namespace YouTubePlayerSample
{
	// An abstract activity which deals with recovering from errors which may occur during API
	// initialization, but can be corrected through user action.
	public abstract class YouTubeFailureRecoveryActivity : YouTubeBaseActivity, IYouTubePlayerOnInitializedListener
	{
		public const int RecoveryDialogRequest = 1;

		public void OnInitializationFailure(IYouTubePlayerProvider provider, YouTubeInitializationResult errorReason)
		{
			if (errorReason.IsUserRecoverableError)
			{
				errorReason.GetErrorDialog(this, RecoveryDialogRequest).Show();
			}
			else
			{
				var errorMessage = string.Format(GetString(Resource.String.error_player), errorReason);
				Toast.MakeText(this, errorMessage, ToastLength.Long).Show();
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == RecoveryDialogRequest)
			{
				// Retry initialization if user performed a recovery action
				YouTubePlayerProvider.Initialize(DeveloperKey.Key, this);
			}
		}

		public abstract void OnInitializationSuccess(IYouTubePlayerProvider provider, IYouTubePlayer player, bool wasRestored);

		protected abstract IYouTubePlayerProvider YouTubePlayerProvider { get; }
	}
}
