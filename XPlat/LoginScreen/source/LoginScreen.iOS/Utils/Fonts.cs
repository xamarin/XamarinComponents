using System;
using System.Collections.Concurrent;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace LoginScreen.Utils
{
	static class Fonts
	{
		static readonly ConcurrentDictionary<Tuple<string, float>, UIFont> cache = new ConcurrentDictionary<Tuple<string, float>, UIFont>();

		public static UIFont HelveticaNeueMedium(float size)
		{
			return Font ("HelveticaNeue-Medium", size);
		}

		private static UIFont Font(string name, float size)
		{
			var key = new Tuple<string, float> (name, size);
			
			return cache.GetOrAdd(key, UIFont.FromName(name, size));
		}
	}
}

