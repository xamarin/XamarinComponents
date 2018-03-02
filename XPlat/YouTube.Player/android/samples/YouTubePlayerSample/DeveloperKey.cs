using System;

namespace YouTubePlayerSample
{
	// Static container class for holding a reference to your YouTube Developer Key.
	public static class DeveloperKey
	{
		/// <summary>
		/// Please replace this with a valid API key which is enabled for the
		/// YouTube Data API v3 service. Go to the Google Developers Console
		/// to register a new developer key: 
		///     https://console.developers.google.com/
		/// </summary>
		/// https://www.thewissen.io/embedding-youtube-feed-xamarin-forms/
		public const string Key = "YOUR_API_KEY";

		static DeveloperKey()
		{
			if (Key == "YOUR_API_KEY")
			{
				throw new Exception("You API key has not been set.");
			}
		}
	}
}
