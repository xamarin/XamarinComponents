using ObjCRuntime;

[assembly: LinkWith ("libRZTransitions.a", 
	LinkTarget = LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	Frameworks = "CoreGraphics Foundation UIKit QuartzCore",
	LinkerFlags = "-ObjC",
	SmartLink = true, 
	ForceLoad = true)]
