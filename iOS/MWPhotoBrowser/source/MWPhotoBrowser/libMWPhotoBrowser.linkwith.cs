using System.Runtime.CompilerServices;
using ObjCRuntime;

[assembly: LinkWith (
	"libMWPhotoBrowser.a", 
	LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.Arm64,
	ForceLoad = true,
	SmartLink = true,
	Frameworks = "ImageIO QuartzCore AssetsLibrary MediaPlayer CoreGraphics",
	WeakFrameworks = "Photos")]

[assembly: TypeForwardedTo (typeof(DACircularProgress.CircularProgressView))]
[assembly: TypeForwardedTo (typeof(SDWebImage.SDImageCache))]
[assembly: TypeForwardedTo (typeof(SDWebImage.SDWebImageManager))]
[assembly: TypeForwardedTo (typeof(MBProgressHUD.MTMBProgressHUD))]
