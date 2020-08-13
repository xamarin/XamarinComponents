// this file adds some convenience "overloads" to the "IsRunningOn*" set:
//  - IsRunningOnWindows
//  - IsRunningOnMac
//  - IsRunningOnLinux
//  - IsRunningOnUnix

using System.Runtime.InteropServices;

bool IsRunningOnMac () {
	return System.Environment.OSVersion.Platform == PlatformID.MacOSX || MacPlatformDetector.IsMac.Value;
}

bool IsRunningOnLinux () {
	return IsRunningOnUnix () && !IsRunningOnMac ();
}

internal static class MacPlatformDetector {
	internal static readonly Lazy<bool> IsMac = new Lazy<bool> (IsRunningOnMac);

	[DllImport ("libc")]
	static extern int uname (IntPtr buf);

	static bool IsRunningOnMac () {
		IntPtr buf = IntPtr.Zero;
		try {
			buf = Marshal.AllocHGlobal (8192);
			// This is a hacktastic way of getting sysname from uname()
			if (uname (buf) == 0) {
				string os = Marshal.PtrToStringAnsi (buf);
				if (os == "Darwin")
					return true;
			}
		} catch {
		} finally {
			if (buf != IntPtr.Zero)
				Marshal.FreeHGlobal (buf);
		}
		return false;
	}
}
