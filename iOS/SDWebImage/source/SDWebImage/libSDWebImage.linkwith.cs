using System;
using System.Reflection;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
#endif

[assembly: LinkerSafe]
[assembly: LinkWith ("libSDWebImage.a", 
#if __UNIFIED__
	LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.Simulator | LinkTarget.Simulator64, 
#else
	LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, 
#endif
	SmartLink = true, 
	ForceLoad = false, 
	LinkerFlags = "-ObjC -fobjc-arc", 
	Frameworks = "CoreGraphics ImageIO",
	WeakFrameworks = "MapKit")]

[assembly: AssemblyVersion ("3.7.0.0")]
[assembly: AssemblyFileVersion ("3.7.3.0")]
