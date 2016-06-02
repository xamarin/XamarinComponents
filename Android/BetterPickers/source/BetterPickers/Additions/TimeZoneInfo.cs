
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BetterPickers.TimeZonePickers
{
	public partial class TimeZoneInfo : Java.Lang.Object, Java.Lang.IComparable
	{
		int Java.Lang.IComparable.CompareTo (Java.Lang.Object obj) 
		{
			return CompareTo ((TimeZoneInfo)obj);		
		}
	}
}

