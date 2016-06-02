using System;
#if __UNIFIED__
using ObjCRuntime;
using Foundation;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
#endif

[assembly: LinkWith (
	"libMBProgressHUD.a", 
	LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64, 
	ForceLoad = true, 
	Frameworks = "CoreGraphics")]