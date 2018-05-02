using ObjCRuntime;

[assembly: LinkWith ("libCardboardSDK_i386.a", LinkTarget.Simulator,LinkerFlags="-ObjC -lc++", Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreMotion CoreText CoreVideo Security GLKit MediaPlayer OpenGLES QuartzCore", SmartLink = true, ForceLoad = false, IsCxx = true)]
