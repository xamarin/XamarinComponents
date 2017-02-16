//using System;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.IO;
//using System.Text;
//using System.Net.Http.Headers;

//namespace Xamarin.Build.Download
//{
//	static class HttpMultipartContentParser
//	{
//		public static async Task<List<Part>> ReadAsMultiPartContentAsync (this HttpContent content, int bufferSize = 2048)
//		{
//			var encoding = Encoding.UTF8;
//			var parsedParts = new List<Part> ();

//			// Find the boundary
//			var boundary = content?.Headers?.ContentType?.Parameters.FirstOrDefault (
//							   p => p.Name.Equals ("boundary", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;

//			var boundaryBytes = encoding.GetBytes ("--" + boundary + "\r\n");
//			var lastBoundaryBytes = encoding.GetBytes ("--" + boundary + "--\r\n");
//			//var newlineBytes = encoding.GetBytes ("\r\n");
//			var doubleNewlineBytes = encoding.GetBytes ("\r\n\r\n");

//			var contentData = new List<byte> ();
//			var part = new Part ();
//			var buffer = new byte [bufferSize];
//			var inputStream = await content.ReadAsStreamAsync ();
//			var data = new List<byte> ();

//			var parseState = ParseState.Initiating;

//			while (true) {
//				var read = await inputStream.ReadAsync (buffer, 0, buffer.Length);

//				// Exit loop once done reading stream
//				if (read <= 0)
//					break;

//				// Add the data read into our overall buffer
//				data.AddRange (buffer.Take (read));

//				while (true) {
//					if (parseState == ParseState.Initiating) {

//						var boundaryIndex = data.IndexOfPattern (boundaryBytes);

//						// Not enough data in the buffer, keep reading and try again
//						if (boundaryIndex < 0)
//							break;

//						// Remove everything up to and including the first boundary
//						data.RemoveRange (0, boundaryIndex + boundaryBytes.Length);

//						parseState = ParseState.ReadingHeaders;
//						continue; // Check state again

//					} else if (parseState == ParseState.ReadingHeaders) {

//						var dblNlIndex = data.IndexOfPattern (doubleNewlineBytes);

//						// Still receiving headers, keep reading
//						if (dblNlIndex < 0)
//							break;

//						var hdata = data.Take (dblNlIndex + 1).ToArray ();
//						var headersData = encoding.GetString (hdata, 0, hdata.Length);

//						// Parse out the headers
//						var headers = headersData.Split (new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
//						foreach (var header in headers) {
//							var headerParts = header.Split (new [] { ':' }, 2);
//							if (headerParts != null && headerParts.Length == 2)
//								part.Headers.Add (headerParts [0].Trim (), headerParts [1].Trim ());
//						}

//						//isAudioPart = part.ContentType.Equals("audio/mpeg", StringComparison.OrdinalIgnoreCase);

//						// Remove everything up to the end of the headers (including the newlines)
//						data.RemoveRange (0, dblNlIndex + doubleNewlineBytes.Length);

//						parseState = ParseState.ReadingStream;
//						continue;

//					} else if (parseState == ParseState.ReadingStream) {

//						var wasLastBoundary = false;
//						var boundaryIndex = data.IndexOfPattern (boundaryBytes);
//						if (boundaryIndex < 0) {
//							boundaryIndex = data.IndexOfPattern (lastBoundaryBytes);
//							wasLastBoundary = true;
//						}

//						byte [] d;

//						// still receiving stream data, keep going
//						if (boundaryIndex < 0) {
//							// Grab all available data
//							d = data.ToArray ();
//							data.Clear ();
//						} else {
//							// Grab only the data up until the boundary we found
//							d = data.Take (boundaryIndex).ToArray ();
//							data.RemoveRange (0, boundaryIndex);
//						}

//						contentData.AddRange (d); // Add the data to another buffer

//						if (boundaryIndex < 0) {
//							// Still no boundary found, keep reading
//							break;
//						} else {
//							// Found our boundary, continue on checking the buffer
//							// TODO: wrap up the part
//							part.Data = contentData.ToArray ();

//							// Fire the event
//							parsedParts.Add (part);

//							// Reset
//							parseState = ParseState.Initiating;
//							part = new Part ();
//							contentData.Clear ();

//							if (wasLastBoundary) {
//								parseState = ParseState.Closed;
//								break;
//							} else
//								continue;
//						}

//					}

//				}

//				if (parseState == ParseState.Closed)
//					break;
//			}

//			return parsedParts;
//		}
//	}

//	enum ParseState
//	{
//		Initiating,
//		ReadingHeaders,
//		ReadingStream,
//		Closed
//	}

//	public class Part
//	{
//		public Part ()
//		{
//			Headers = new Dictionary<string, string> ();
//		}

//		public byte [] Data { get; set; }

//		public Dictionary<string, string> Headers { get; set; }

//		public string ContentType {
//			get { return GetHeaderValue ("Content-Type"); }
//		}

//		public string ContentDisposition {
//			get { return GetHeaderValue ("Content-Disposition"); }
//		}

//		public string ContentID {
//			get { return GetHeaderValue ("Content-ID"); }
//		}

//		public System.Net.Http.Headers.ContentRangeHeaderValue ContentRange {
//			get {
//				var result = new ContentRangeHeaderValue (0L);
//				var cr = GetHeaderValue ("Content-Range");
//				if (!string.IsNullOrEmpty (cr))
//					ContentRangeHeaderValue.TryParse (cr, out result);
//				return result;
//			}
//		}

//		string GetHeaderValue (string name)
//		{
//			if (Headers == null || !Headers.ContainsKey (name))
//				return string.Empty;

//			return Headers [name].Trim ();
//		}
//	}

//	public static class ByteExtensionMethods
//	{
//		public static int IndexOfPattern (this List<byte> source, byte [] searchPattern)
//		{
//			var indices = IndexesOfPattern (source, searchPattern);

//			return indices.Count <= 0 ? -1 : indices [0];
//		}

//		public static List<int> IndexesOfPattern (this List<byte> source, byte [] searchPattern)
//		{
//			var list = new List<int> ();

//			if (IsEmptyLocate (source, searchPattern))
//				return list;

//			for (int i = 0; i < source.Count; i++) {
//				if (!IsMatch (source, i, searchPattern))
//					continue;

//				list.Add (i);
//			}

//			return list;
//		}

//		static bool IsMatch (List<byte> source, int position, byte [] candidate)
//		{
//			if (candidate.Length > (source.Count - position))
//				return false;

//			for (int i = 0; i < candidate.Length; i++)
//				if (source [position + i] != candidate [i])
//					return false;

//			return true;
//		}

//		static bool IsEmptyLocate (List<byte> array, byte [] candidate)
//		{
//			return array == null
//				|| candidate == null
//				|| array.Count <= 0
//				|| candidate.Length == 0
//				|| candidate.Length > array.Count;
//		}

//	}
//}
