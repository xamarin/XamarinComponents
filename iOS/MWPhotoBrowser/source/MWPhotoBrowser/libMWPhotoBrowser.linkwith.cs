using System.Runtime.CompilerServices;

#if __UNIFIED__
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
#endif

[assembly: LinkWith (
	"libMWPhotoBrowser.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Arm64,
	ForceLoad = true,
	Frameworks = "ImageIO QuartzCore AssetsLibrary MediaPlayer CoreGraphics",
	WeakFrameworks = "Photos")]
	
[assembly: TypeForwardedTo (typeof(DACircularProgress.CircularProgressView))]
[assembly: TypeForwardedTo (typeof(SDWebImage.SDImageCache))]
[assembly: TypeForwardedTo (typeof(SDWebImage.SDWebImageManager))]
[assembly: TypeForwardedTo (typeof(MBProgressHUD.MTMBProgressHUD))]
