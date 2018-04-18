SimpleAndroidLibrary
====================

This is a simple starter gradle project that can be used as a starting point
for creating Android aar files.

To build you will first need to set `ANDROID_HOME` to your local Android SDK directory.  
For many Xamarin users this will be 
`~/Library/Developer/Xamarin/android-sdk-macosx`.

Then you can either run `make` to build the aar files and `make clean` to clean up the directory.

You can also call gradlew directly if you wish.  Again you will need to set `ANDROID_HOME`.

`ANDROID_HOME=~/Library/Developer/Xamarin/android-sdk-macosx ./gradlew assembleRelease`

You can also import this directory into Android Studio to work
with the libraries in that IDE.

Please do not commit any changes back to this directory!  Keep this 'template' clean
and simple for the next developer.