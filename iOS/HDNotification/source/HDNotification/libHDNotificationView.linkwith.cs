using ObjCRuntime;

[assembly: LinkWith ("libHDNotificationView.a", 
	LinkTarget = LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.Arm64,
	SmartLink = true, 
	ForceLoad = true)]
