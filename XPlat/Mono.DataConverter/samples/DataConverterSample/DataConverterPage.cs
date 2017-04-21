using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

using Mono;
using System.Collections;

namespace DataConverterSample
{
	public class DataConverterPage : ContentPage
	{
		public DataConverterPage ()
		{
			Title = "Mono.DataConverter";

			Padding = 12;

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = {
					new ScrollView {
						VerticalOptions = LayoutOptions.FillAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Content = new StackLayout {
							VerticalOptions = LayoutOptions.FillAndExpand,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							Children = {
								new Label {
									VerticalOptions = LayoutOptions.FillAndExpand,
									HorizontalOptions = LayoutOptions.FillAndExpand,
									HorizontalTextAlignment = TextAlignment.Start,
									Text = string.Join (Environment.NewLine, GetText ())
								}
							}
						}
					}
				}
			};
		}

		private IEnumerable<string> GetText ()
		{
			byte[] bytes;
			string byteString;
			IList unpacked;

			bytes = DataConverter.Pack ("CCCC", 65, 66, 67, 68);
			yield return "Pack (\"CCCC\", 65, 66, 67, 68):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("CCCC", bytes, 0);
			yield return "Unpack (\"CCCC\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			bytes = DataConverter.Pack ("4C", 65, 66, 67, 68);
			yield return "Pack (\"4C\", 65, 66, 67, 68):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("4C", bytes, 0);
			yield return "Unpack (\"4C\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			bytes = DataConverter.Pack ("4C", 65, 66, 67, 68, 69, 70);
			yield return "Pack (\"4C\", 65, 66, 67, 68, 69, 70):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("4C", bytes, 0);
			yield return "Unpack (\"4C\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			bytes = DataConverter.Pack ("^ii", 0x1234abcd, 0x7fadb007);
			yield return "Pack (\"^ii\", 0x1234abcd, 0x7fadb007):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("^ii", bytes, 0);
			yield return "Unpack (\"^ii\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			// Encode 3 integers as big-endian, but only provides two as arguments,
			// this defaults to zero for the last value.
			bytes = DataConverter.Pack ("^iii", 0x1234abcd, 0x7fadb007);
			yield return "Pack (\"^iii\", 0x1234abcd, 0x7fadb007):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("^iii", bytes, 0);
			yield return "Unpack (\"^iii\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			// Encode as little endian, pack 1 short, align, 1 int
			bytes = DataConverter.Pack ("_s!i", 0x7b, 0x12345678);
			yield return "Pack (\"_s!i\", 0x7b, 0x12345678):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("_s!i", bytes, 0);
			yield return "Unpack (\"_s!i\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			// Encode a string in utf-8 with a null terminator
			bytes = DataConverter.Pack ("z8", "hello");
			yield return "Pack (\"z8\", \"hello\"):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("z8", bytes, 0);
			yield return "Unpack (\"z8\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;

			yield return string.Empty;

			// Little endian encoding, for Int16, followed by an aligned
			// Int32
			bytes = DataConverter.Pack ("_s!i", 0x7b, 0x12345678);
			yield return "Pack (\"_s!i\", 0x7b, 0x12345678):";
			byteString = GetBytesString (bytes);
			yield return byteString;

			unpacked = DataConverter.Unpack ("_s!i", bytes, 0);
			yield return "Unpack (\"_s!i\", bytes, 0):";
			byteString = GetListString (unpacked);
			yield return byteString;
		}

		private string GetBytesString (byte[] bytes)
		{
			var byteString = string.Join (", ", bytes.Select (b => b.ToString ("x2")));
			return string.Format ("[{0}]", byteString);
		}

		private string GetListString (IList list)
		{
			var byteString = string.Join (", ", list.Cast<object> ().Select (b => b.ToString ()));
			return string.Format ("[{0}]", byteString);
		}
	}
}
