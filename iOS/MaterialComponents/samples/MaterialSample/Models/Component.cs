using System;
using Newtonsoft.Json;

namespace MaterialSample {
	public class Component {
		[JsonProperty ("id")]
		public string Id { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }

		[JsonProperty ("description")]
		public string Description { get; set; }

		[JsonProperty ("image_name")]
		public string ImageName { get; set; }

		[JsonProperty ("samples")]
		public Sample [] Samples { get; set; }
	}
}
