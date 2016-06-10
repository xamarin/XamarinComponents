using System;
#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

#if __UNIFIED__
[assembly: LinkWith ("libWindowsAzureMessaging.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, SmartLink=true, ForceLoad = true)]
#else
[assembly: LinkWith ("libWindowsAzureMessaging.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, SmartLink=true, ForceLoad = true)]
#endif