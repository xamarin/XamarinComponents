using ObjCRuntime;

[assembly: LinkWith (
	"libMobile-Buy-SDK.a", 
	ForceLoad = true,
	Frameworks = "PassKit",
	WeakFrameworks = "",
	IsCxx = true,
	LinkTarget = LinkTarget.Arm64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64)]
