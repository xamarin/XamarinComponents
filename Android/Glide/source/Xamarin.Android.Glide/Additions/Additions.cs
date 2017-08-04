using System;

namespace Bumptech
{
	public partial class RequestManager
	{
		public DrawableTypeRequest Load(System.Uri uri)
		{
			return Load(Android.Net.Uri.Parse(uri.AbsoluteUri));
		}
	}
}
