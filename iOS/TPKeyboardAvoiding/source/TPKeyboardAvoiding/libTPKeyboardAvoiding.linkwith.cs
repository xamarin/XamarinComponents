using System.Reflection;
using System.Runtime.CompilerServices;
#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

[assembly: AssemblyVersion ("1.2.11.0")]
[assembly: AssemblyFileVersion ("1.2.11.0")]

[assembly: LinkWith (
	"libTPKeyboardAvoiding.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	ForceLoad = true)]
	