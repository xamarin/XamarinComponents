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

	[Introduced (PlatformName.iOS, 12, 5)]
	public enum ENActivityFlags : uint {
		PeriodicRun = 1U << 2
	}

	public delegate void ENActivityHandler (ENActivityFlags activityFlags);

	partial class ENManager {
		[DllImport ("/usr/lib/libobjc.dylib", EntryPoint = "_Block_copy")]
		private static extern IntPtr _Block_copy (ref BlockLiteral block);

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

		public void SetLaunchActivityHandler (ENActivityHandler activityHandler)
		{
			var block = new BlockLiteral ();
			block.SetupBlock (ENActivityHandlerCallbackReference, activityHandler);
			var ptr = _Block_copy (ref block);

			var key = new NSString ("activityHandler");
			void_objc_msgSend_IntPtr_IntPtr (this.Handle, Selector.GetHandle ("setValue:forKey:"), ptr, key.Handle);
		}
	}
}
