using System;
using Foundation;

namespace GigyaSDK
{
	partial class GSObject
	{
		public NSObject this[string key] => GetObject(key);
	}

	public static class GSObjectExtensions
	{
		public static GSAccount ToAccount (this GSObject This)
		{
			var account = new GSAccount ();
			account.InternalDictionary = This.InternalDictionary;

			return account;
		}
	}
}
