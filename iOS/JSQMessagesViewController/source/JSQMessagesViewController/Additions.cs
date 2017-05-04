using System;
using Foundation;
using UIKit;
using ObjCRuntime;
using ApiDefinition;

namespace JSQMessagesViewController
{
	public static partial class UIColorExtensions
	{

		//
		// Static Fields
		//
		private static readonly IntPtr fixed_class_ptr = Class.GetHandle ("UIColor");

		private static object __mt_MessageBubbleRedColor_var_static;

		private static object __mt_MessageBubbleLightGrayColor_var_static;

		private static object __mt_MessageBubbleGreenColor_var_static;

		private static object __mt_MessageBubbleBlueColor_var_static;

		//
		// Static Properties
		//
		public static UIColor MessageBubbleBlueColor {
			[Export ("jsq_messageBubbleBlueColor")]
			get {
				UIColor nSObject = Runtime.GetNSObject<UIColor> (Messaging.IntPtr_objc_msgSend (UIColorExtensions.fixed_class_ptr, Selector.GetHandle ("jsq_messageBubbleBlueColor")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIColorExtensions.__mt_MessageBubbleBlueColor_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIColor MessageBubbleGreenColor {
			[Export ("jsq_messageBubbleGreenColor")]
			get {
				UIColor nSObject = Runtime.GetNSObject<UIColor> (Messaging.IntPtr_objc_msgSend (UIColorExtensions.fixed_class_ptr, Selector.GetHandle ("jsq_messageBubbleGreenColor")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIColorExtensions.__mt_MessageBubbleGreenColor_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIColor MessageBubbleLightGrayColor {
			[Export ("jsq_messageBubbleLightGrayColor")]
			get {
				UIColor nSObject = Runtime.GetNSObject<UIColor> (Messaging.IntPtr_objc_msgSend (UIColorExtensions.fixed_class_ptr, Selector.GetHandle ("jsq_messageBubbleLightGrayColor")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIColorExtensions.__mt_MessageBubbleLightGrayColor_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIColor MessageBubbleRedColor {
			[Export ("jsq_messageBubbleRedColor")]
			get {
				UIColor nSObject = Runtime.GetNSObject<UIColor> (Messaging.IntPtr_objc_msgSend (UIColorExtensions.fixed_class_ptr, Selector.GetHandle ("jsq_messageBubbleRedColor")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIColorExtensions.__mt_MessageBubbleRedColor_var_static = nSObject;
				}
				return nSObject;
			}
		}
	}

	public static partial class UIImageExtensions
	{
		//
		// Static Fields
		//
		private static readonly IntPtr fixed_class_ptr = Class.GetHandle ("UIImage");

		private static object __mt_DefaultTypingIndicatorImage_var_static;

		private static object __mt_DefaultPlayImage_var_static;

		private static object __mt_DefaultPauseImage_var_static;

		private static object __mt_DefaultAccessoryImage_var_static;

		private static object __mt_BubbleRegularTaillessImage_var_static;

		private static object __mt_BubbleRegularStrokedTaillessImage_var_static;

		private static object __mt_BubbleCompactImage_var_static;

		private static object __mt_BubbleCompactTaillessImage_var_static;

		private static object __mt_BubbleRegularImage_var_static;

		private static object __mt_BubbleRegularStrokedImage_var_static;

		//
		// Static Properties
		//
		public static UIImage BubbleCompactImage {
			[Export ("jsq_bubbleCompactImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleCompactImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleCompactImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage BubbleCompactTaillessImage {
			[Export ("jsq_bubbleCompactTaillessImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleCompactTaillessImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleCompactTaillessImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage BubbleRegularImage {
			[Export ("jsq_bubbleRegularImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleRegularImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleRegularImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage BubbleRegularStrokedImage {
			[Export ("jsq_bubbleRegularStrokedImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleRegularStrokedImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleRegularStrokedImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage BubbleRegularStrokedTaillessImage {
			[Export ("jsq_bubbleRegularStrokedTaillessImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleRegularStrokedTaillessImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleRegularStrokedTaillessImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage BubbleRegularTaillessImage {
			[Export ("jsq_bubbleRegularTaillessImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_bubbleRegularTaillessImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_BubbleRegularTaillessImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage DefaultAccessoryImage {
			[Export ("jsq_defaultAccessoryImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_defaultAccessoryImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_DefaultAccessoryImage_var_static = nSObject;
				}
				return nSObject;
			}
		}

		public static UIImage DefaultPlayImage {
			[Export ("jsq_defaultPlayImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_defaultPlayImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_DefaultPlayImage_var_static = nSObject;
				}
				return nSObject;
			}
		}
		
		public static UIImage DefaultPauseImage
		{
			[Export("jsq_defaultPauseImage")]
			get
			{
				UIImage nSObject = Runtime.GetNSObject<UIImage>(Messaging.IntPtr_objc_msgSend(UIImageExtensions.class_ptr, Selector.GetHandle("jsq_defaultPauseImage")));
				if (!NSObject.IsNewRefcountEnabled()) 
				{
					UIImageExtensions.__mt_DefaultPauseImage_var_static = nSObject; 
				}
				return nSObject;
			}
		} 

		public static UIImage DefaultTypingIndicatorImage {
			[Export ("jsq_defaultTypingIndicatorImage")]
			get {
				UIImage nSObject = Runtime.GetNSObject<UIImage> (Messaging.IntPtr_objc_msgSend (UIImageExtensions.class_ptr, Selector.GetHandle ("jsq_defaultTypingIndicatorImage")));
				if (!NSObject.IsNewRefcountEnabled ()) {
					UIImageExtensions.__mt_DefaultTypingIndicatorImage_var_static = nSObject;
				}
				return nSObject;
			}
		}
	}
}

