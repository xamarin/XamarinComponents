using System.Linq;

namespace YouTube.Player
{
	partial class PlayerView
	{
		public PlaybackQuality [] AvailableQualityLevels
		{
			get
			{
				var internalVals = AvailableQualityLevelsInternal;
				return internalVals?.Select (q => (PlaybackQuality) q.Int64Value)?.ToArray ();
			}
		}
	}
}
