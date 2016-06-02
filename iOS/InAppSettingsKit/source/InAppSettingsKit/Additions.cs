using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

namespace InAppSettingsKit
{
	partial class SettingsStore
	{
		public const string AppSettingChangedNotification = "kAppSettingChanged";
	}

	partial class AppSettingsViewController
	{
		public void SetHiddenKeys (string[] hiddenKeys, bool animated)
		{
			SetHiddenKeys (hiddenKeys == null ? null : new NSSet (NSArray.FromStrings (hiddenKeys)), animated);
		}

		public void SetHiddenKeys (NSString[] hiddenKeys, bool animated)
		{
			SetHiddenKeys (hiddenKeys == null ? null : NSSet.MakeNSObjectSet (hiddenKeys), animated);
		}
	}

	partial class SettingsSpecifier
	{
		public UIUserInterfaceIdiom[] UserInterfaceIdioms {
			get {
				var array = GetUserInterfaceIdioms ();
				if (array == null) {
					return null;
				}
				var idioms = new UIUserInterfaceIdiom[array.Length];
				for (int i = 0; i < array.Length; i++) {
					var l = ((NSNumber)array [i]).Int32Value;
					idioms [i] = (UIUserInterfaceIdiom)l;
				}
				return idioms;
			}
		}
	}
}
