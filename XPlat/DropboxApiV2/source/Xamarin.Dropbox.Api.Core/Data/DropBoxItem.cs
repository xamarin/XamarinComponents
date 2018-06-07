using System;
using Xamarin.Dropbox.Api.Enums;

namespace Xamarin.Dropbox.Api
{
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
