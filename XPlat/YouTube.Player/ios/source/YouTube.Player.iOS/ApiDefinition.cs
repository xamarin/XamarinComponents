using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace YouTube.Player
{
	interface IPlayerViewDelegate { }

	// @protocol YTPlayerViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name="YTPlayerViewDelegate")]
	interface PlayerViewDelegate
	{
		// @optional -(void)playerViewDidBecomeReady:(YTPlayerView * _Nonnull)playerView;
		[Export ("playerViewDidBecomeReady:")]
		void DidBecomeReady (PlayerView playerView);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didChangeToState:(YTPlayerState)state;
		[Export ("playerView:didChangeToState:")]
		void DidChangeState (PlayerView playerView, PlayerState state);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didChangeToQuality:(YTPlaybackQuality)quality;
		[Export ("playerView:didChangeToQuality:")]
		void DidChangeToQuality (PlayerView playerView, PlaybackQuality quality);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView receivedError:(YTPlayerError)error;
		[Export ("playerView:receivedError:")]
		void ReceivedError (PlayerView playerView, PlayerError error);

		// @optional -(void)playerView:(YTPlayerView * _Nonnull)playerView didPlayTime:(float)playTime;
		[Export ("playerView:didPlayTime:")]
		void DidPlayTime (PlayerView playerView, float playTime);

		// @optional -(UIColor * _Nonnull)playerViewPreferredWebViewBackgroundColor:(YTPlayerView * _Nonnull)playerView;
		[Export ("playerViewPreferredWebViewBackgroundColor:")]
		UIColor PreferredWebViewBackgroundColor (PlayerView playerView);

		// @optional -(UIView * _Nullable)playerViewPreferredInitialLoadingView:(YTPlayerView * _Nonnull)playerView;
		[Export ("playerViewPreferredInitialLoadingView:")]
		[return: NullAllowed]
		UIView PreferredInitialLoadingView (PlayerView playerView);
	}

	// @interface YTPlayerView : UIView <UIWebViewDelegate>
	[BaseType (typeof (UIView), Name="YTPlayerView")]
	interface PlayerView : IUIWebViewDelegate
	{
		// @property (readonly, nonatomic, strong) UIWebView * _Nullable webView;
		[NullAllowed, Export ("webView", ArgumentSemantic.Strong)]
		UIWebView WebView { get; }

		// @property (nonatomic, weak) id<YTPlayerViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPlayerViewDelegate Delegate { get; set; }

		// -(BOOL)loadWithVideoId:(NSString * _Nonnull)videoId;
		[Export ("loadWithVideoId:")]
		bool LoadWithVideoId (string videoId);

		// -(BOOL)loadWithPlaylistId:(NSString * _Nonnull)playlistId;
		[Export ("loadWithPlaylistId:")]
		bool LoadWithPlaylistId (string playlistId);

		// -(BOOL)loadWithVideoId:(NSString * _Nonnull)videoId playerVars:(NSDictionary * _Nullable)playerVars;
		[Export ("loadWithVideoId:playerVars:")]
		bool LoadWithVideoId (string videoId, [NullAllowed] NSDictionary playerVars);

		// -(BOOL)loadWithPlaylistId:(NSString * _Nonnull)playlistId playerVars:(NSDictionary * _Nullable)playerVars;
		[Export ("loadWithPlaylistId:playerVars:")]
		bool LoadWithPlaylistId (string playlistId, [NullAllowed] NSDictionary playerVars);

		// -(BOOL)loadWithPlayerParams:(NSDictionary * _Nullable)additionalPlayerParams;
		[Export ("loadWithPlayerParams:")]
		bool LoadWithPlayerParams ([NullAllowed] NSDictionary additionalPlayerParams);

		// -(void)playVideo;
		[Export ("playVideo")]
		void PlayVideo ();

		// -(void)pauseVideo;
		[Export ("pauseVideo")]
		void PauseVideo ();

		// -(void)stopVideo;
		[Export ("stopVideo")]
		void StopVideo ();

		// -(void)seekToSeconds:(float)seekToSeconds allowSeekAhead:(BOOL)allowSeekAhead;
		[Export ("seekToSeconds:allowSeekAhead:")]
		void SeekToSeconds (float seekToSeconds, bool allowSeekAhead);

		// -(void)cueVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cueVideoById:startSeconds:suggestedQuality:")]
		void CueVideoById (string videoId, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cueVideoById:startSeconds:endSeconds:suggestedQuality:")]
		void CueVideoById (string videoId, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadVideoById:startSeconds:suggestedQuality:")]
		void LoadVideoById (string videoId, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoById:(NSString * _Nonnull)videoId startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadVideoById:startSeconds:endSeconds:suggestedQuality:")]
		void LoadVideoById (string videoId, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cueVideoByURL:startSeconds:suggestedQuality:")]
		void CueVideoByUrl (string videoURL, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cueVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cueVideoByURL:startSeconds:endSeconds:suggestedQuality:")]
		void CueVideoByUrl (string videoURL, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadVideoByURL:startSeconds:suggestedQuality:")]
		void LoadVideoByUrl (string videoURL, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadVideoByURL:(NSString * _Nonnull)videoURL startSeconds:(float)startSeconds endSeconds:(float)endSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadVideoByURL:startSeconds:endSeconds:suggestedQuality:")]
		void LoadVideoByUrl (string videoURL, float startSeconds, float endSeconds, PlaybackQuality suggestedQuality);

		// -(void)cuePlaylistByPlaylistId:(NSString * _Nonnull)playlistId index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cuePlaylistByPlaylistId:index:startSeconds:suggestedQuality:")]
		void CuePlaylistByPlaylistId (string playlistId, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)cuePlaylistByVideos:(NSArray * _Nonnull)videoIds index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("cuePlaylistByVideos:index:startSeconds:suggestedQuality:")]
		//[Verify (StronglyTypedNSArray)] TODO
		void CuePlaylistByVideos (NSObject [] videoIds, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadPlaylistByPlaylistId:(NSString * _Nonnull)playlistId index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadPlaylistByPlaylistId:index:startSeconds:suggestedQuality:")]
		void LoadPlaylistByPlaylistId (string playlistId, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)loadPlaylistByVideos:(NSArray * _Nonnull)videoIds index:(int)index startSeconds:(float)startSeconds suggestedQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("loadPlaylistByVideos:index:startSeconds:suggestedQuality:")]
		//[Verify (StronglyTypedNSArray)] TODO
		void LoadPlaylistByVideos (NSObject [] videoIds, int index, float startSeconds, PlaybackQuality suggestedQuality);

		// -(void)nextVideo;
		[Export ("nextVideo")]
		void NextVideo ();

		// -(void)previousVideo;
		[Export ("previousVideo")]
		void PreviousVideo ();

		// -(void)playVideoAt:(int)index;
		[Export ("playVideoAt:")]
		void PlayVideoAt (int index);

		// -(float)playbackRate;
		// -(void)setPlaybackRate:(float)suggestedRate;
		[Export ("playbackRate")]
		float PlaybackRate { get; set; }

		// -(NSArray * _Nullable)availablePlaybackRates;
		[Export ("availablePlaybackRates")]
		//[Verify (StronglyTypedNSArray)] TODO
		[return: NullAllowed]
		NSObject [] AvailablePlaybackRates ();

		// -(void)setLoop:(BOOL)loop;
		[Export ("setLoop:")]
		void SetLoop (bool loop);

		// -(void)setShuffle:(BOOL)shuffle;
		[Export ("setShuffle:")]
		void SetShuffle (bool shuffle);

		// -(float)videoLoadedFraction;
		[Export ("videoLoadedFraction")]
		float VideoLoadedFraction ();

		// -(YTPlayerState)playerState;
		[Export ("playerState")]
		PlayerState PlayerState ();

		// -(float)currentTime;
		[Export ("currentTime")]
		float CurrentTime ();

		// TODO should this be a prop?
		// -(YTPlaybackQuality)playbackQuality;
		[Export ("playbackQuality")]
		PlaybackQuality PlaybackQuality ();

		// -(void)setPlaybackQuality:(YTPlaybackQuality)suggestedQuality;
		[Export ("setPlaybackQuality:")]
		void SetPlaybackQuality (PlaybackQuality suggestedQuality);

		// -(NSArray * _Nullable)availableQualityLevels;
		[Export ("availableQualityLevels")]
		//[Verify (StronglyTypedNSArray)] TODO
		[return: NullAllowed]
		NSNumber [] AvailableQualityLevels ();

		// -(NSTimeInterval)duration;
		[Export ("duration")]
		double Duration ();

		// -(NSURL * _Nullable)videoUrl;
		[Export ("videoUrl")]
		[return: NullAllowed]
		NSUrl VideoUrl ();

		// -(NSString * _Nullable)videoEmbedCode;
		[Export ("videoEmbedCode")]
		[return: NullAllowed]
		string VideoEmbedCode ();

		// -(NSArray * _Nullable)playlist;
		[Export ("playlist")]
		//[Verify (StronglyTypedNSArray)] TODO
		[return: NullAllowed]
		NSObject [] Playlist ();

		// -(int)playlistIndex;
		[Export ("playlistIndex")]
		int PlaylistIndex ();

		// -(void)removeWebView;
		[Export ("removeWebView")]
		void RemoveWebView ();
	}
}
