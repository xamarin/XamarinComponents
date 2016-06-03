using System;  

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
#endif

[assembly: LinkerSafe]
[assembly: LinkWith ("libiRateLib.a",
#if __UNIFIED__
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
#else
	LinkTarget.Simulator | LinkTarget.ArmV7 | LinkTarget.ArmV7s,
#endif
	ForceLoad = true,
	Frameworks = "StoreKit",
	SmartLink=true)]
	
