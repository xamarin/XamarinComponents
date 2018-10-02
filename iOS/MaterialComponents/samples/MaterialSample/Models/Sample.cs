using System;
using Newtonsoft.Json;
namespace MaterialSample {
	public class Sample {
		[JsonProperty ("id")]
		public string Id { get; set; }

		[JsonProperty ("title")]
		public string Title { get; set; }
	}
}
