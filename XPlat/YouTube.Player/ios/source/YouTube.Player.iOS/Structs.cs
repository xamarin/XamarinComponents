using System;
using Foundation;
using ObjCRuntime;

namespace YouTube.Player
{
	[Native]
	public enum PlayerState : long
	{
		Unstarted,
		Ended,
		Playing,
		Paused,
		Buffering,
		Queued,
		Unknown
	}

	[Native]
	public enum PlaybackQuality : long
	{
		Small,
		Medium,
		Large,
		Hd720,
		Hd1080,
		HighRes,
		Auto,
		Default,

		[Advice ("This should never be returned. It is here for future proofing.")]
		Unknown
	}

	[Native]
	public enum PlayerError : long
	{
		InvalidParam,
		HTML5Error,
		VideoNotFound,
		NotEmbeddable,
		Unknown
	}
}
