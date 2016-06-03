using System;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

[assembly: LinkWith (
	"libSWTableViewCell.a", 
	LinkTarget.Arm64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64, 
	ForceLoad = true, 
	Frameworks = "CoreGraphics QuartzCore UIKit")]
