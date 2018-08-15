using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Foundation;
using ObjCRuntime;
using UIKit;

namespace MaterialComponents {
	partial class CAMediaTimingFunctionAnimationTiming {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("CAMediaTimingFunction");

	}

	public static partial class MaterialComponentsConstants {
		static string versionString;
		public static string VersionString {
			get {
				if (versionString == null) {
					IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
					IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "MaterialComponentsVersionString");
					versionString = Marshal.PtrToStringAnsi (ptr);
					Dlfcn.dlclose (RTLD_MAIN_ONLY);
				}

				return versionString;
			}
		}
	}

	public static class MaterialFeatureHighlightStrings {
		public static NSString [] StringTable { get; } = { new NSString ("MaterialFeatureHighlightDismissAccessibilityHint") };
		public static NSString TableName { get; } = new NSString ("MaterialFeatureHighlight");
	}

	public partial class CardScheme {
		SemanticColorScheme semanticColorScheme;
		public SemanticColorScheme SemanticColorScheme {
			get {
				if (semanticColorScheme == null)
					semanticColorScheme = Runtime.GetNSObject<SemanticColorScheme> (ColorScheme.Handle, false);

				return semanticColorScheme;
			}
			set {
				semanticColorScheme?.Dispose ();
				semanticColorScheme = null;
				ColorScheme = value;
			}
		}
	}

	public partial class ChipField {
		static UIEdgeInsets? defaultContentEdgeInsets;
		public static UIEdgeInsets DefaultContentEdgeInsets { 
			get {
				if (defaultContentEdgeInsets == null) {
					IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
					IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "MDCChipFieldDefaultContentEdgeInsets");
					defaultContentEdgeInsets = (UIEdgeInsets)Marshal.PtrToStructure (ptr, typeof (UIEdgeInsets));
					Dlfcn.dlclose (RTLD_MAIN_ONLY);
				}

				return defaultContentEdgeInsets.Value;
			}
		}
	}

	public static class OverlayImplementor {
		public static NSString DidChangeNotification { get; } = new NSString ("MDCOverlayDidChangeNotification");
		public static NSString IdentifierKey { get; } = new NSString ("identifier");
		public static NSString FrameKey { get; } = new NSString ("frame");
		public static NSString TransitionDurationKey { get; } = new NSString ("duration");
		public static NSString TransitionCurveKey { get; } = new NSString ("curve");
		public static NSString TransitionTimingFunctionKey { get; } = new NSString ("timingFunction");
		public static NSString TransitionImmediacyKey { get; } = new NSString ("runImmediately");
	}

	partial class UIApplicationAppExtensions {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIApplication");
	}

	partial class UIFontMaterialTypography {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIFont");
	}

	partial class UIFontDescriptorMaterialTypography {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIFontDescriptor");
	}

	partial class UIViewMDCTimingFunction {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIView");
	}
}
