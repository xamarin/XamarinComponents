using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Accounts;
using CoreGraphics;
using Foundation;
using Social;
using Newtonsoft.Json.Linq;

namespace TwitterImagePipelineDemo
{
	public class TwitterApi
	{
		private static Lazy<TwitterApi> sharedInstance = new Lazy<TwitterApi>(() => new TwitterApi());

		private ACAccount account;
		private ACAccountStore accountStore;
		private Task loadingAccount;

		private TwitterApi()
		{
			accountStore = new ACAccountStore();

			loadingAccount = LoadAccountAsync();
		}

		public static TwitterApi SharedInstance => sharedInstance.Value;

		public async Task<List<TweetInfo>> SearchAsync(string term, int count)
		{
			await LoadAccountAsync();

			Debug.WriteLine($"Searching for '{term}'");

			if (account == null)
				throw new Exception("Couldn't open Twitter account!");
			if (term == null)
				throw new Exception("Null search term!");

			// create the Twitter search request
			var requestUrl = new NSUrl($"https://api.twitter.com/1.1/search/tweets.json?count={count}&q={Uri.EscapeUriString(term)}");
			var request = SLRequest.Create(SLServiceKind.Twitter, SLRequestMethod.Get, requestUrl, new NSDictionary());
			request.Account = account;
			var preparedRequest = request.GetPreparedUrlRequest();

			if (preparedRequest == null)
				throw new Exception("Couldn't construct request!");

			var taskRequest = await NSUrlSession.SharedSession.CreateDataTaskAsync(preparedRequest);

			var statusCode = ((NSHttpUrlResponse)taskRequest.Response).StatusCode;
			if (statusCode != 200)
				throw new Exception($"HTTP Code {statusCode}");

			var parsedResponse = ParseResponse(taskRequest.Data);
			if (parsedResponse == null)
				throw new Exception("Failed to parse response!");

			Debug.WriteLine("Search completed!");

			return parsedResponse;
		}

		private List<TweetInfo> ParseResponse(NSData data)
		{
			var tweets = new List<TweetInfo>();

			var json = JObject.Parse(data.ToString());
			foreach (var status in json["statuses"])
			{
				var user = status["user"];
				var handle = (string)user["screen_name"];
				if (!string.IsNullOrEmpty(handle))
				{
					var tweet = new TweetInfo
					{
						Text = (string)status["text"],
						Handle = handle
					};
					tweets.Add(tweet);

					var sensitive = (bool?)status["possibly_sensitive"];
					if (!sensitive.HasValue || !sensitive.Value)
					{
						var entities = status["entities"];
						if (entities != null)
						{
							var media = entities["media"];
							if (media != null)
							{
								foreach (var mediaItem in media)
								{
									var type = (string)mediaItem["type"];
									if (type == "photo")
									{
										var imageUrlString = (string)mediaItem["media_url_https"];
										var format = Path.GetExtension(imageUrlString).Substring(1);
										var sizes = mediaItem["sizes"];
										var largeVariant = sizes["large"];
										var w = (int)largeVariant["w"];
										var h = (int)largeVariant["h"];

										var imageInfo = new TweetImageInfo
										{
											Format = format,
											MediaUrl = imageUrlString,
											MediaUrlWithoutExtension = imageUrlString.Substring(0, imageUrlString.Length - format.Length - 1),
											OriginalDimensions = new CGSize(w, h)
										};
										tweet.Images.Add(imageInfo);
									}
								}
							}
						}
					}
				}
			}

			return (tweets.Count > 0) ? tweets : null;
		}

		private async Task LoadAccountAsync()
		{
			if (account != null)
			{
				return;
			}

			if (loadingAccount != null)
			{
				await Task.WhenAny(loadingAccount);
				return;
			}

			Debug.WriteLine("Accessing Twitter Account...");
			WorkStarted?.Invoke(this, EventArgs.Empty);

			var type = accountStore.FindAccountType(ACAccountType.Twitter);
			var result = await accountStore.RequestAccessAsync(type, null);

			if (result.Item1)
			{
				account = accountStore.Accounts[0];
				Debug.WriteLine($"Access granted: {account.Username}");
			}
			else
			{
				Debug.WriteLine("Access denied!");
			}

			WorkFinished?.Invoke(this, EventArgs.Empty);

			loadingAccount = null;
		}

		public event EventHandler WorkStarted;
		public event EventHandler WorkFinished;
	}

	public class TweetImageInfo
	{
		public string MediaUrl { get; set; }
		public string MediaUrlWithoutExtension { get; set; }
		public string Format { get; set; }
		public CGSize OriginalDimensions { get; set; }
	}

	public class TweetInfo
	{
		public string Handle { get; set; }
		public string Text { get; set; }
		public List<TweetImageInfo> Images { get; set; } = new List<TweetImageInfo>();
	}
}
