using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Build.Download;
using Xunit;

namespace NativeLibraryDownloaderTests
{
	public class HashTests
	{
		[Theory]
		[InlineData("C:\\path\\to\\file.aar")]
		[InlineData ("/path/to/file.aar")]
		public void Test_CRC64_Safety(string value)
		{
			var crc = DownloadUtils.Crc64(value);

			Assert.Matches("^[0-9a-zA-Z]+$", crc);
		}
		
	}
}
