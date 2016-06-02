#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

[assembly: LinkWith (
	"libGPUImage.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	ForceLoad = true,
	Frameworks = "OpenGLES CoreMedia CoreVideo QuartzCore AVFoundation")]
	