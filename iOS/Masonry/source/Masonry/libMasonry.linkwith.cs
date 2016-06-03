using System.Reflection;
using System.Runtime.CompilerServices;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

[assembly: AssemblyVersion ("0.6.4.0")]
[assembly: AssemblyFileVersion ("0.6.4.0")]

[assembly: LinkWith (
	"libMasonry.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	ForceLoad = true)]
	