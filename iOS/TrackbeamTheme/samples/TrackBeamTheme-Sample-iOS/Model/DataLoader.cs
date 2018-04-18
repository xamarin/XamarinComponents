using System;
using System.Collections.Generic;
using UIKit;

namespace TrackBeamTheme_Sample_iOS.Model {
	static class DataLoader {

		public static List<Track> LoadSampleData ()
		{
			var track1 = new Track {
				Name = "Love the way you lie",
				Artist = "Eminem feat. Rihanna",
				Image = UIImage.FromFile("album-small.jpg"),
				LargeImage = UIImage.FromFile("album-large.jpg"),
				Length = "3 mins 46 secs",
				Genre = "Hip Hop"
			};
			var track2 = new Track {
				Name = "The Fame",
				Artist = "Lady Gaga",
				Image = UIImage.FromFile("ipad-album-small-1.png"),
				LargeImage = UIImage.FromFile("ipad-album-large-1.png"),
				Length = "4 mins 20 secs",
				Genre = "Dance"
			};
			var track3 = new Track {
				Name = "Speed of Sound",
				Artist = "Coldplay",
				Image = UIImage.FromFile("ipad-album-small-2.png"),
				LargeImage = UIImage.FromFile("ipad-album-large-2.png"),
				Length = "3 mins 46 secs",
				Genre = "Hip Hop"
			};
			var track4 = new Track {
				Name = "I am Sasha - Fierce",
				Artist = "Beyonce",
				Image = UIImage.FromFile("ipad-album-small-3.png"),
				LargeImage = UIImage.FromFile("ipad-album-large-3.png"),
				Length = "3 mins 35 secs",
				Genre = "Dance"
			};

			return new List<Track> () { track1, track2, track3, track4 };

		}
	}
}

