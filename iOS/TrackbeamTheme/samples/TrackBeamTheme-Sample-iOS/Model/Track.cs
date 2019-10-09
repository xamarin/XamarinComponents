using System;

using UIKit;

namespace TrackBeamTheme_Sample_iOS.Model {
	sealed class Track {

		public string Name { get; set; }

		public string Artist { get; set; }

		public UIImage Image { get; set; }

		public UIImage LargeImage { get; set; }

		public string Length { get; set; }

		public string Genre { get; set; }
	}
}

