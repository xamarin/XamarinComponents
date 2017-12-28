using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace YouTube.Player
{
	interface IPlayerViewDelegate { }

	// @protocol YTPlayerViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "YTPlayerViewDelegate")]
	interface PlayerViewDelegate
	{
		// @optional -(void)playerViewDidBecomeReady:(YTPlayerView * _Nonnull)playerView;
		[EventArgs("PlayerViewDidBecomeReady")]
		[EventName("BecameReady")]
		[Export("playerViewDidBecomeReady:")]
		void DidBecomeReady(PlayerView playerView);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didChangeToState:(YTPlayerState)state;
		[EventArgs("PlayerViewStateChanged")]
		[EventName("StateChanged")]
		[Export("playerView:didChangeToState:")]
		void DidChangeToState(PlayerView playerView, PlayerState state);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didChangeToQuality:(YTPlaybackQuality)quality;
		[EventArgs("PlayerViewChangedToQuality")]
		[EventName("QualityChanged")]
		[Export("playerView:didChangeToQuality:")]
		void DidChangeToQuality(PlayerView playerView, PlaybackQuality quality);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView receivedError:(YTPlayerError)error;
		[EventArgs("PlayerViewErrorReceived")]
		[EventName("ErrorReceived")]
		[Export("playerView:receivedError:")]
		void ReceivedError(PlayerView playerView, PlayerError error);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didPlayTime:(float)playTime;
		[EventArgs("PlayerViewTimePlayed")]
		[EventName("TimePlayed")]
		[Export("playerView:didPlayTime:")]
		void DidPlayTime(PlayerView playerView, float playTime);

		// @optional -(UIColor * _Nonnull)playerViewPreferredWebViewBackgroundColor:(YTPlayerView * _Nonnull)playerView;
		[DelegateName("PlayerViewPreferredWebViewBackgroundColor")]
		[NoDefaultValue]
		[Export("playerViewPreferredWebViewBackgroundColor:")]
		UIColor PreferredWebViewBackgroundColor(PlayerView playerView);

		// @optional -(UIView * _Nullable)playerViewPreferredInitialLoadingView:(YTPlayerView * _Nonnull)playerView;
		[DelegateName("PlayerViewPreferredInitialLoadingView")]
		[DefaultValue("null")]
		[Export("playerViewPreferredInitialLoadingView:")]
		[return: NullAllowed]
		UIView PreferredInitialLoadingView(PlayerView playerView);
	}

	// @interface YTPlayerView : UIView <UIWebViewDelegate>
	[BaseType(typeof(UIView),
		   Name = "YTPlayerView",
		   Delegates = new string[] { "Delegate" },
		   Events = new Type[] { typeof(PlayerViewDelegate) })]
	interface PlayerView : IUIWebViewDelegate
	{
		// Base Constructor
		[Export("initWithFrame:")]
		IntPtr Constructor(CGRect frame);

		// @property (readonly, nonatomic, strong) UIWebView * _Nullable webView;
		[NullAllowed, Export("webView", ArgumentSemantic.Strong)]
		UIWebView WebView { get; }

		// @property (nonatomic, weak) id<YTPlayerViewDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		IPlayerViewDelegate Delegate { get; set; }

		// -(BOOL)loadWithVideoId:(NSString * _Nonnull)videoId;
		[Export("loadWithVideoId:")]
		bool LoadVideoById(string videoId);

		// -(BOOL)loadWithPlaylistId:(NSString * _Nonnull)playlistId;
		[Export("loadWithPlaylistId:")]
		bool LoadPlaylistById(string playlistId);

		// -(BOOL)loadWithVideoId:(NSString * _Nonnull)videoId playerVars:(NSDictionary * _Nullable)playerVars;
		[Export("loadWithVideoId:playerVars:")]
		bool LoadVideoById(string videoId, [NullAllowed] NSDictionary playerVars);

		// -(BOOL)loadWithPlaylistId:(NSString * _Nonnull)playlistId playerVars:(NSDictionary * _Nullable)playerVars;
		[Export("loadWithPlaylistId:playerVars:")]
		bool LoadPlaylistById(string playlistId, [NullAllowed] NSDictionary playerVars);

		// -(BOOL)loadWithPlayerParams:(NSDictionary * _Nullable)additionalPlayerParams;
		[Export("loadWithPlayerParams:")]
		bool Load([NullAllowed] NSDictionary additionalPlayerParams);

		// -(void)playVideo;
		[Export("playVideo")]
		void PlayVideo();

		// -(void)pauseVideo;
		[Export("pauseVideo")]
		void PauseVideo();

		// -(void)stopVideo;
		[Export("stopVideo")]
		void StopVideo();

		// -(void)seekToSeconds:(float)seekToSeconds allowSeekAhead:(BOOL)allowSeekAhead;
		[Export("seekToSeconds:allowSeekAhead:")]
		void SeekToSeconds(float seekToSeconds, bool allowSeekAhead);

		// -(void)cueVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cueVideoById:startSeconds:suggestedQuality:")]
		void CueVideoById(string videoId, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cueVideoById:startSeconds:endSeconds:suggestedQuality:")]
		void CueVideoById(string videoId, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadVideoById:startSeconds:suggestedQuality:")]
		void LoadVideoById(string videoId, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadVideoById:startSeconds:endSeconds:suggestedQuality:")]
		void LoadVideoById(string videoId, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cueVideoByURL:startSeconds:suggestedQuality:")]
		void CueVideoByUrl(string videoUrl, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cueVideoByURL:startSeconds:endSeconds:suggestedQuality:")]
		void CueVideoByUrl(string videoUrl, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadVideoByURL:startSeconds:suggestedQuality:")]
		void LoadVideoByUrl(string videoUrl, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadVideoByURL:startSeconds:endSeconds:suggestedQuality:")]
		void LoadVideoByUrl(string videoUrl, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)cuePlaylistByPlaylistId:(NSString * _Nonnull)playlistId index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cuePlaylistByPlaylistId:index:startSeconds:suggestedQuality:")]
		void CuePlaylistById(string playlistId, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cuePlaylistByVideos:(NSArray * _Nonnull)videoIds index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("cuePlaylistByVideos:index:startSeconds:suggestedQuality:")]
		void CuePlaylistByVideos(string[] videoIds, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadPlaylistByPlaylistId:(NSString * _Nonnull)playlistId index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadPlaylistByPlaylistId:index:startSeconds:suggestedQuality:")]
		void LoadPlaylistById(string playlistId, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadPlaylistByVideos:(NSArray * _Nonnull)videoIds index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("loadPlaylistByVideos:index:startSeconds:suggestedQuality:")]
		void LoadPlaylistByVideos(string[] videoIds, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)nextVideo;
		[Export("nextVideo")]
		void NextVideo();

		// -(void)previousVideo;
		[Export("previousVideo")]
		void PreviousVideo();

		// -(void)playVideoAt:(int)index;
		[Export("playVideoAt:")]
		void PlayVideoAt(int index);

		// -(float)playbackRate;
		// -(void)setPlaybackRate:(float)suggestedRate;
		[Export("playbackRate")]
		float PlaybackRate { get; set; }

		// -(NSArray * _Nullable)availablePlaybackRates;
		[Internal]
		[Export("availablePlaybackRates")]
		[return: NullAllowed]
		NSArray _AvailablePlaybackRates();

		// -(void)setLoop:(BOOL)loop;
		[Export("setLoop:")]
		void SetLoop(bool loop);

		// -(void)setShuffle:(BOOL)shuffle;
		[Export("setShuffle:")]
		void SetShuffle(bool shuffle);

		// -(float)videoLoadedFraction;
		[Export("videoLoadedFraction")]
		float VideoLoadedFraction { get; }

		// -(YTPlayerState)playerState;
		[Export("playerState")]
		PlayerState PlayerState { get; }

		// -(float)currentTime;
		[Export("currentTime")]
		float CurrentTime { get; }

		// -(YTPlaybackQuality)playbackQuality;
		// -(void)setPlaybackQuality:(YTPlaybackQuality)suggestedQuality;
		[Export("playbackQuality")]
		PlaybackQuality PlaybackQuality { get; set; }

		// -(NSArray * _Nullable)availableQualityLevels;
		[Internal]
		[Export("availableQualityLevels")]
		[return: NullAllowed]
		NSArray _AvailableQualityLevels();

		// -(NSTimeInterval)duration;
		[Export("duration")]
		double Duration { get; }

		// -(NSURL * _Nullable)videoUrl;
		[Export("videoUrl")]
		NSUrl VideoUrl { get; }

		// -(NSString * _Nullable)videoEmbedCode;
		[Export("videoEmbedCode")]
		string VideoEmbedCode { get; }

		// -(NSArray * _Nullable)playlist;
		[Export("playlist")]
		string[] Playlist { get; }

		// -(int)playlistIndex;
		[Export("playlistIndex")]
		int PlaylistIndex { get; }

		// -(void)removeWebView;
		[Export("removeWebView")]
		void RemoveWebView();
	}
}