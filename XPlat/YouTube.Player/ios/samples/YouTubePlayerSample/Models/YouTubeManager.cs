using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using UIKit;

namespace YouTubePlayerSample
{
	public class YouTubeManager
	{
		static readonly Lazy<YouTubeManager> lazy = new Lazy<YouTubeManager>(() => new YouTubeManager());

		long maxResults = 50;
		YouTubeService youtubeService;
		HttpClient httpClient;

		public static YouTubeManager SharedInstance => lazy.Value;

		public static UIColor YouTubeColor { get; } = UIColor.FromRGBA(199, 11, 0, 255);
		public static UIColor BackgroundYouTubeColor { get; } = UIColor.FromRGBA(150, 7, 0, 255);
		public static UIColor DisabledColor { get; } = UIColor.FromWhiteAlpha(.5f, .5f);

		public long MaxResults
		{
			get { return maxResults; }
			set
			{
				if (maxResults < 0 || maxResults > 50)
					value = 50;

				maxResults = value;
			}
		}

		YouTubeManager()
		{
			youtubeService = new YouTubeService(new BaseClientService.Initializer
			{
				ApiKey = YouTubeApiKey.ApiKey,
				ApplicationName = "YouTubePlayerSample"
			});

			httpClient = new HttpClient();
		}

		public async Task<List<Video>> GetVideos(Search search)
		{
			var searchRequest = youtubeService.Search.List("snippet");
			searchRequest.Q = search.Tags;
			searchRequest.PageToken = search.NextPageToken;
			searchRequest.MaxResults = maxResults;

			var searchResponse = await searchRequest.ExecuteAsync();

			var videos = new List<Video>();

			search.NextPageToken = searchResponse.NextPageToken;
			foreach (var searchResult in searchResponse.Items)
			{
				if (searchResult.Id.Kind != "youtube#video")
					continue;

				videos.Add(new Video
				{
					Id = searchResult.Id.VideoId,
					Title = searchResult.Snippet.Title,
					ThumbnailUrl = searchResult.Snippet.Thumbnails.Default__.Url
				});
			}

			return videos;
		}

		public async Task<List<Video>> GetVideos(Playlist playlist)
		{
			var playlistRequest = youtubeService.PlaylistItems.List("snippet");
			playlistRequest.PlaylistId = playlist.Id;
			playlistRequest.PageToken = playlist.NextPageToken;
			playlistRequest.MaxResults = maxResults;

			var playlistResponse = await playlistRequest.ExecuteAsync();

			var videos = new List<Video>();

			playlist.NextPageToken = playlistResponse.NextPageToken;
			foreach (var searchResult in playlistResponse.Items)
				videos.Add(new Video
				{
					Id = searchResult.Snippet.ResourceId.VideoId,
					Title = searchResult.Snippet.Title,
					ThumbnailUrl = searchResult.Snippet.Thumbnails?.Default__.Url
				});

			return videos;
		}

		public async Task<string> DownloadThumbnail(Video video, CancellationToken ct)
		{
			ct.ThrowIfCancellationRequested();

			var imageName = $"{video.Id}.jpg";
			var imagePath = Path.Combine(Path.GetTempPath(), imageName);

			if (File.Exists(imagePath))
			{
				ct.ThrowIfCancellationRequested();
				return imagePath;
			}

			ct.ThrowIfCancellationRequested();

			var bytes = await httpClient.GetByteArrayAsync(video.ThumbnailUrl);

			ct.ThrowIfCancellationRequested();

			var fileStream = new FileStream(imagePath, FileMode.OpenOrCreate);
			await fileStream.WriteAsync(bytes, 0, bytes.Length);
			fileStream.Close();

			ct.ThrowIfCancellationRequested();

			return imagePath;
		}
	}
}
