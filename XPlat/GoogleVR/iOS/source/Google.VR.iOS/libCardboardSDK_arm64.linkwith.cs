using ObjCRuntime;

[assembly: LinkWith ("libCardboardSDK_arm64.a", LinkTarget.Arm64,LinkerFlags="-ObjC -lc++", Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreMotion CoreText CoreVideo Security GLKit MediaPlayer OpenGLES QuartzCore", SmartLink = true, ForceLoad = false, IsCxx = true)]
