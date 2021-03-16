using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;

namespace ExposureNotifications {

	public static class ENAttenuationRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = byte.MaxValue;
	}

	public static class ENRiskLevelRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 7;
	}

	public static class ENRiskLevelValueRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 8;
	}

	public static class ENRiskWeightRange {

		public static byte Default { get; } = 1;
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 100;
	}

	[Flags]
	public enum ENActivityFlags : uint {
		PeriodicRun = 1U << 2
	}

	public delegate void ENActivityHandler (ENActivityFlags activityFlags);

	partial class ENManager {
		[DllImport ("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		private extern static void void_objc_msgSend_IntPtr_IntPtr (IntPtr receiver, IntPtr selector, IntPtr arg1, IntPtr arg2);

		private delegate void ENActivityHandlerCallbackDelegate (IntPtr block, ENActivityFlags activityFlags);

		[MonoPInvokeCallback (typeof (ENActivityHandlerCallbackDelegate))]
		private static unsafe void ENActivityHandlerCallback (IntPtr block, ENActivityFlags activityFlags)
		{
			var descriptor = (BlockLiteral*) block;
			var del = (ENActivityHandler) (descriptor->Target);
			del?.Invoke (activityFlags);
		}

		private static ENActivityHandlerCallbackDelegate ENActivityHandlerCallbackReference = ENActivityHandlerCallback;

		public unsafe void SetLaunchActivityHandler (ENActivityHandler activityHandler)
		{
			var key = new NSString ("activityHandler");
			var sel = Selector.GetHandle ("setValue:forKey:");

			// get old value
			var oldValue = ValueForKey (key);

			// dispose block
			if (oldValue != null) {
				void_objc_msgSend_IntPtr_IntPtr (Handle, sel, IntPtr.Zero, key.Handle);

				var descriptor = (BlockLiteral*) oldValue.Handle;
				descriptor->CleanupBlock ();
			}

			if (activityHandler == null)
				return;

			// create new block
			var block = new BlockLiteral ();
			block.SetupBlock (ENActivityHandlerCallbackReference, activityHandler);

			// assign
			var ptr = &block;
			void_objc_msgSend_IntPtr_IntPtr (Handle, sel, (IntPtr)ptr, key.Handle);
		}
	}
}
