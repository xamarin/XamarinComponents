using System;

namespace YouTubePlayerSample
{
	public static class YouTubeApiKey
	{
		// https://www.thewissen.io/embedding-youtube-feed-xamarin-forms/

		// This is not related with YouTube.Player for iOS
		// This is an extra functionality to show a better sample of the component

		// Get your API Key from https://console.developers.google.com/apis/credentials
		// Don't forget to enable YouTube Data API here https://console.developers.google.com/apis/api/youtube.googleapis.com/overview
		public static string ApiKey { get; } = "YOUR_API_KEY";

		static YouTubeApiKey()
		{
			if (ApiKey == "YOUR_API_KEY")
			{
				throw new Exception("You API key has not been set.");
			}
		}
	}
}
