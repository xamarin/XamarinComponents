using System;

using ObjCRuntime;

[assembly: LinkWith ("DropboxSDK.a", LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64, Frameworks = "QuartzCore Security", SmartLink = true)]