using ObjCRuntime;

[assembly: LinkWith ("libCardboardSDK_armv7.a", LinkTarget.ArmV7|LinkTarget.ArmV6,LinkerFlags="-ObjC -lc++", Frameworks="AVFoundation AudioToolbox CoreGraphics CoreMedia CoreMotion CoreText CoreVideo Security GLKit MediaPlayer OpenGLES QuartzCore", SmartLink = true, ForceLoad = false, IsCxx = true)]
