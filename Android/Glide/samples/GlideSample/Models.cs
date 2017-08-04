using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GlideSample
{
	[XmlRoot(ElementName = "image")]
	public class Image
	{
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
		[XmlElement(ElementName = "id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "source_url")]
		public string Source_url { get; set; }
	}

	[XmlRoot(ElementName = "images")]
	public class Images
	{
		[XmlElement(ElementName = "image")]
		public List<Image> Image { get; set; }
	}

	[XmlRoot(ElementName = "data")]
	public class Data
	{
		[XmlElement(ElementName = "images")]
		public Images Images { get; set; }
	}

	[XmlRoot(ElementName = "response")]
	public class Response
	{
		[XmlElement(ElementName = "data")]
		public Data Data { get; set; }
	}
}
