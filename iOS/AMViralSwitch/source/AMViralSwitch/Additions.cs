using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Generic;

#if __UNIFIED__
using CoreAnimation;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
#endif

namespace AMViralSwitch
{
	partial class ViralSwitch
	{
		// convert the CompletionOn action to an event
		private event EventHandler onCompleted;
		public event EventHandler OnCompleted {
			add {
				onCompleted += value;
				CompletionOn = OnOnCompleted;
			}
			remove {
				onCompleted -= value;
			}
		}
		protected virtual void OnOnCompleted ()
		{
			var handler = onCompleted;
			if (handler != null) {
				handler (this, EventArgs.Empty);
			}
		}

		// convert the CompletionOff action to an event
		private event EventHandler offCompleted;
		public event EventHandler OffCompleted {
			add {
				offCompleted += value;
				CompletionOff = OnOffCompleted;
			}
			remove {
				offCompleted -= value;
			}
		}
		protected virtual void OnOffCompleted ()
		{
			var handler = offCompleted;
			if (handler != null) {
				handler (this, EventArgs.Empty);
			}
		}

		// wrap the AnimationElements properties
		public void SetAnimationElementsOn (params AnimationElement[] elements)
		{
			AnimationElementsOn = GetAnimationElements (elements);
		}

		public void SetAnimationElementsOff (params AnimationElement[] elements)
		{
			AnimationElementsOff = GetAnimationElements (elements);
		}

		private static NSDictionary[] GetAnimationElements (AnimationElement[] elements)
		{
			var dictionaryList = new NSDictionary[elements.Length];
			for (int i = 0; i < elements.Length; i++) {
				var element = elements [i];
				var objects = new List<INativeObject> (4);
				var keys = new List<INativeObject> (4);
				if (element.View != null) {
					objects.Add (element.View);
					keys.Add (ElementView);
				}
				if (element.KeyPath != null) {
					objects.Add ((NSString)element.KeyPath);
					keys.Add (ElementKeyPath);
				}
				if (element.FromValue != null) {
					objects.Add (element.FromValue);
					keys.Add (ElementFromValue);
				}
				if (element.ToValue != null) {
					objects.Add (element.ToValue);
					keys.Add (ElementToValue);
				}
				dictionaryList [i] = NSDictionary.FromObjectsAndKeys (objects.ToArray (), keys.ToArray ());
			}
			return dictionaryList;
		}
	}

	public class AnimationElement
	{
		public INativeObject View { get; private set; }

		public string KeyPath { get; private set; }

		public INativeObject FromValue { get; private set; }

		public INativeObject ToValue { get; private set; }

		public static AnimationElement TextColor (UILabel view, INativeObject to)
		{
			return TextColor (view, null, to);
		}

		public static AnimationElement TextColor (UILabel view, INativeObject from, INativeObject to)
		{
			return new AnimationElement {
				View = view,
				KeyPath = "textColor",
				FromValue = from,
				ToValue = to
			};
		}

		public static AnimationElement TintColor (UIButton view, INativeObject to)
		{
			return TintColor (view, null, to);
		}

		public static AnimationElement TintColor (UIButton view, INativeObject from, INativeObject to)
		{
			return new AnimationElement {
				View = view,
				KeyPath = "tintColor",
				FromValue = from,
				ToValue = to
			};
		}

		public static AnimationElement Layer (CALayer viewLayer, string keyPath, INativeObject to)
		{
			return Layer (viewLayer, keyPath, null, to);
		}

		public static AnimationElement Layer (CALayer viewLayer, string keyPath, INativeObject from, INativeObject to)
		{
			return new AnimationElement {
				View = viewLayer,
				KeyPath = keyPath,
				FromValue = from,
				ToValue = to
			};
		}
	}
}
