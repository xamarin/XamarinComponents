using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Net;

namespace Google.Android.Vending.Licensing
{
	public static class PolicyExtensions
	{
		public static readonly long MillisPerMinute = (long)TimeSpan.FromMinutes(1).TotalMilliseconds;

		private const char NameValueSeparator = '=';
		private const char ParameterSeparator = '&';

		private static readonly DateTime Jan1St970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public static Dictionary<string, string> DecodeExtras(string extras)
		{
			var results = new Dictionary<string, string>();

			IEnumerable<KeyValuePair<string, string>> parameters = GetParameters(extras);
			foreach (KeyValuePair<string, string> item in parameters)
			{
				int count = results.Keys.Count(x => x == item.Key);
				string name = item.Key + (count != 0 ? count.ToString() : string.Empty);
				results.Add(name, item.Value);
			}

			return results;
		}

		public static long GetCurrentMilliseconds() => (long)(DateTime.UtcNow - Jan1St970).TotalMilliseconds;

		public static bool TryDecodeExtras(string rawData, out Dictionary<string, string> extras)
		{
			bool result = false;
			try
			{
				extras = DecodeExtras(rawData);
				result = true;
			}
			catch
			{
				extras = new Dictionary<string, string>();
			}
			return result;
		}

		private static IEnumerable<KeyValuePair<string, string>> GetParameters(string uri)
		{
			return GetParameters(uri, Encoding.UTF8);
		}

		private static IEnumerable<KeyValuePair<string, string>> GetParameters(string uri, Encoding encoding)
		{
			var result = new List<KeyValuePair<string, string>>();

			IEnumerable<string[]> parameters = uri.Split(ParameterSeparator).Select(p => p.Split(NameValueSeparator));
			foreach (string[] nameValue in parameters)
			{
				if (nameValue.Length < 1 || nameValue.Length > 2)
				{
					throw new ArgumentException("uri");
				}

				string name = URLDecoder.Decode(nameValue[0], encoding.WebName);
				string value = nameValue.Length == 2 ? URLDecoder.Decode(nameValue[1], encoding.WebName) : string.Empty;

				result.Add(new KeyValuePair<string, string>(name, value));
			}

			return result;
		}
	}
}
