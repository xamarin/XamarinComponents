using System;
using ObjCRuntime;

[assembly: LinkWith ("libpop.a",
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.Arm64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s, 
	IsCxx = true, SmartLink = true, ForceLoad = true, LinkerFlags = "-Objc -lc++ -all_load", Frameworks = "Foundation CoreImage")]
