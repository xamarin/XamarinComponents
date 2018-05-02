﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle ("Audio")]
[assembly: AssemblyDescription ("")]
[assembly: AssemblyConfiguration ("")]
[assembly: AssemblyCompany ("Xamarin")]
[assembly: AssemblyProduct ("")]
[assembly: AssemblyCopyright ("Xamarin")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion ("1.0.0")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: Java.Interop.JavaLibraryReference ("classes.jar",
    PackageName = __Consts.PackageName,
    SourceUrl = __GvrConsts.Url,
    EmbeddedArchive = __Consts.AarPath,
    Version = __GvrConsts.Version)]

[assembly: Android.IncludeAndroidResourcesFromAttribute ("./",
    PackageName = __Consts.PackageName,
    SourceUrl = __GvrConsts.Url,
    EmbeddedArchive = __Consts.AarPath,
    Version = __GvrConsts.Version)]

[assembly: Android.NativeLibraryReferenceAttribute ("jni/armeabi-v7a/libvraudio_engine.so",
    SourceUrl = __GvrConsts.Url,
    EmbeddedArchive = __Consts.AarPath,
    Version = __GvrConsts.Version,
    PackageName = __Consts.PackageName)]

[assembly: Android.NativeLibraryReferenceAttribute ("jni/arm64-v8a/libvraudio_engine.so",
	SourceUrl = __GvrConsts.Url,
	EmbeddedArchive = __Consts.AarPath,
	Version = __GvrConsts.Version,
	PackageName = __Consts.PackageName)]

[assembly: Android.NativeLibraryReferenceAttribute ("jni/x86/libvraudio_engine.so",
	SourceUrl = __GvrConsts.Url,
	EmbeddedArchive = __Consts.AarPath,
	Version = __GvrConsts.Version,
	PackageName = __Consts.PackageName)]

static class __Consts
{
    public const string PackageName = "gvr audio";
    public const string AarPath = "gvr-android-sdk-" + __GvrConsts.LongVersion + "/libraries/audio/audio.aar";
}



