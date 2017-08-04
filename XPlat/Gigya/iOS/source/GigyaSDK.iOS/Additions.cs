using System;
using Foundation;

namespace GigyaSDK
{
	partial class GSObject
	{
		public NSObject this[string key] => GetObject(key);
	}
}
