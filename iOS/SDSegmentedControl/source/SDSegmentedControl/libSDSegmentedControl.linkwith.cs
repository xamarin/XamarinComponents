using Foundation;
using ObjCRuntime;

[assembly: LinkerSafe]

[assembly: LinkWith(
	"libSDSegmentedControl.a",
	LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64 | LinkTarget.i386 | LinkTarget.x86_64,
	Frameworks = "QuartzCore",
	ForceLoad = true)]
