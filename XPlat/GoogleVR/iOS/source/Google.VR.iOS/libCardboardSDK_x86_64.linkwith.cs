using ObjCRuntime;

[assembly: LinkWith ("libCardboardSDK_x86_64.a", LinkTarget.Simulator64,LinkerFlags="-ObjC -lc++", Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreMotion CoreText CoreVideo Security GLKit MediaPlayer OpenGLES QuartzCore", SmartLink = true, ForceLoad = false, IsCxx = true)]
