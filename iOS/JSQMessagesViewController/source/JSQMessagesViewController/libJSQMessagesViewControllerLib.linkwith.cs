using System;
using ObjCRuntime;

[assembly: LinkWith ("libJSQMessagesViewControllerLib.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	LinkerFlags = "-ObjC",
	SmartLink = true, 
	ForceLoad = true)]
