using System;

namespace DropboxV2ApiSampleiOS.Data
{
	public enum DropBoxItemType
	{
		File,
		Folder,
	}

	public class DropBoxItem
	{
		public DropBoxItemType ItemType
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Path
		{
			get;
			set;
		}
	}
}
