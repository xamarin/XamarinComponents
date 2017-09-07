using System;

namespace Google.Android.Vending.Licensing
{
	partial class APKExpansionPolicy
	{
		public ExpansionFile GetExpansionFile(ExpansionFileType type)
		{
			var index = (int)type;
			return new ExpansionFile(
				GetExpansionFileName(index),
				GetExpansionFileSize(index),
				GetExpansionURL(index));
		}

		public void SetExpansionFile(ExpansionFileType type, ExpansionFile file)
		{
			var index = (int)type;
			SetExpansionFileName(index, file.FileName);
			SetExpansionFileSize(index, file.FileSize);
			SetExpansionURL(index, file.Url);
		}

		public enum ExpansionFileType
		{
			MainFile = APKExpansionPolicy.MainFileUrlIndex,
			PatchFile = APKExpansionPolicy.PatchFileUrlIndex
		}

		public struct ExpansionFile
		{
			public ExpansionFile(string fileName, long fileSize, string url)
			{
				FileName = fileName;
				FileSize = fileSize;
				Url = url;
			}

			public string FileName { get; set; }

			public long FileSize { get; set; }

			public string Url { get; set; }
		}
	}
}
