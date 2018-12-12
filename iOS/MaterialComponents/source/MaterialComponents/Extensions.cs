using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace MaterialComponents {

	public static class ShadowElevations {
		/** The shadow elevation of the app bar. */
		public static readonly nfloat AppBar = (nfloat)4.0;

		/** The shadow elevation of the Bottom App Bar. */
		public static readonly nfloat BottomNavigationBar = (nfloat)8.0;

		/** The shadow elevation of a card in its picked up state. */
		public static readonly nfloat CardPickedUp = (nfloat)8.0;

		/** The shadow elevation of a card in its resting state. */
		public static readonly nfloat CardResting = (nfloat)2.0;

		/** The shadow elevation of dialogs. */
		public static readonly nfloat Dialog = (nfloat)24.0;

		/** The shadow elevation of the floating action button in its pressed state. */
		public static readonly nfloat FloatingActionButtonPressed = (nfloat)12.0;

		/** The shadow elevation of the floating action button in its resting state. */
		public static readonly nfloat FloatingActionButtonResting = (nfloat)6.0;

		/** The shadow elevation of a menu. */
		public static readonly nfloat Menu = (nfloat)8.0;

		/** The shadow elevation of a modal bottom sheet. */
		public static readonly nfloat ModalBottomSheet = (nfloat)16.0;

		/** The shadow elevation of the navigation drawer. */
		public static readonly nfloat NavDrawer = (nfloat)16.0;

		/** No shadow elevation at all. */
		public static readonly nfloat None = (nfloat)0.0;

		/** The shadow elevation of a picker. */
		public static readonly nfloat Picker = (nfloat)24.0;

		/** The shadow elevation of the quick entry in the scrolled state. */
		public static readonly nfloat QuickEntry = (nfloat)3.0;

		/** The shadow elevation of the quick entry in the resting state. */
		public static readonly nfloat QuickEntryResting = (nfloat)2.0;

		/** The shadow elevation of a raised button in the pressed state. */
		public static readonly nfloat RaisedButtonPressed = (nfloat)8.0;

		/** The shadow elevation of a raised button in the resting state. */
		public static readonly nfloat RaisedButtonResting = (nfloat)2.0;

		/** The shadow elevation of a refresh indicator. */
		public static readonly nfloat Refresh = (nfloat)3.0;

		/** The shadow elevation of the right drawer. */
		public static readonly nfloat RightDrawer = (nfloat)16.0;

		/** The shadow elevation of the search bar in the resting state. */
		public static readonly nfloat SearchBarResting = (nfloat)2.0;

		/** The shadow elevation of the search bar in the scrolled state. */
		public static readonly nfloat SearchBarScrolled = (nfloat)3.0;

		/** The shadow elevation of the snackbar. */
		public static readonly nfloat Snackbar = (nfloat)6.0;

		/** The shadow elevation of a sub menu (+1 for each additional sub menu). */
		public static readonly nfloat SubMenu = (nfloat)9.0;

		/** The shadow elevation of a switch. */
		public static readonly nfloat Switch = (nfloat)1.0;
	}

	partial class ShapedView {
		public ShapedView (CGRect frame) : this (frame, null) {
		}
	}

	partial class BottomAppBarColorThemer {
		// renamed/obsolete members
		[Obsolete ("Use ApplySurfaceVariant instead.")]
		public static void ApplySurfaceVariantWithSemanticColorScheme (IColorScheming colorScheme, BottomAppBarView bottomAppBarView) {
			ApplySurfaceVariant (colorScheme, bottomAppBarView);
		}
	}

	partial class TextFieldColorThemer {
		public static void ApplySemanticColorSchemeToAll (IColorScheming colorScheme, Type textInputControllerType) {
			ApplySemanticColorSchemeToAll (colorScheme, new Class (textInputControllerType));
		}
		public static void ApplySemanticColorSchemeToAll<T> (IColorScheming colorScheme) where T : ITextInputController {
			ApplySemanticColorSchemeToAll (colorScheme, typeof (T));
		}

		// renamed/obsolete members
		[Obsolete ("Use ApplySurfaceVariant instead.")]
		public static void ApplySemanticColorSchemeToTextInputController (IColorScheming colorScheme, ITextInputController textInputController) {
			ApplySemanticColorScheme (colorScheme, textInputController);
		}
		[Obsolete ("Use ApplySemanticColorScheme instead.")]
		public static void ApplySemanticColorSchemeToTextInput (IColorScheming colorScheme, ITextInput textInput) {
			ApplySemanticColorScheme (colorScheme, textInput);
		}
	}

	partial class TextFieldFontThemer {
		public static void ApplyFontSchemeToAll (IFontScheme fontScheme, Type textInputControllerType) {
			ApplyFontSchemeToAll (fontScheme, new Class (textInputControllerType));
		}
		public static void ApplyFontSchemeToAll<T> (IFontScheme fontScheme) where T : ITextInputController {
			ApplyFontSchemeToAll (fontScheme, typeof (T));
		}

		// renamed/obsolete members
		[Obsolete ("Use ApplyFontScheme instead.")]
		public static void ApplyFontSchemeToTextInputController (IFontScheme fontScheme, ITextInputController textInputController) {
			ApplyFontScheme (fontScheme, textInputController);
		}
		[Obsolete ("Use ApplyFontScheme instead.")]
		public static void ApplyFontSchemeToTextField (IFontScheme fontScheme, TextField textField) {
			ApplyFontScheme (fontScheme, textField);
		}
	}
	
	partial class TextFieldTypographyThemer {
		public static void ApplyTypographySchemeToAll (ITypographyScheming typographyScheme, Type textInputControllerType) {
			ApplyTypographySchemeToAll (typographyScheme, new Class (textInputControllerType));
		}
		public static void ApplyTypographySchemeToAll<T> (ITypographyScheming typographyScheme) where T : ITextInputController {
			ApplyTypographySchemeToAll (typographyScheme, typeof (T));
		}

		// renamed/obsolete members
		[Obsolete ("Use ApplyTypographyScheme instead.")]
		public static void ApplyTypographySchemeToTextInputController (ITypographyScheming typographyScheme, ITextInputController textInputController) {
			ApplyTypographyScheme (typographyScheme, textInputController);
		}
		[Obsolete ("Use ApplyTypographyScheme instead.")]
		public static void ApplyTypographySchemeToTextInput (ITypographyScheming typographyScheme, ITextInput textInput) {
			ApplyTypographyScheme (typographyScheme, textInput);
		}
	}

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
		public SemanticColorScheme SemanticColorScheme {
			get {
				return Runtime.GetNSObject<SemanticColorScheme> (ColorScheme.Handle, false);
			}
			set {
				ColorScheme = value;
			}
		}
		//public ShapeScheme ShapeScheme {
		//	get {
		//		return Runtime.GetNSObject<ShapeScheme> (ShapeScheme.Handle, false);
		//	}
		//	set {
		//		ShapeScheme = value;
		//	}
		//}
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

	partial class UIViewMaterialRtl {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("UIView");
	}

	partial class NSLocaleMaterialRtl {
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSLocale");
	}
}
