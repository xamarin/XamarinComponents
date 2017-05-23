#!/bin/sh

mdoc update -o docs output/Xamarin.InAppBilling.dll -r=/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mandroid/platforms/android-18/Mono.Android.dll -r=/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/mono/2.1/Java.Interop.dll -r=/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xbuild-frameworks/MonoAndroid/v1.0/Facades/System.Runtime.dll -import=output/mcsdocs/Xamarin.InAppBilling.xml


