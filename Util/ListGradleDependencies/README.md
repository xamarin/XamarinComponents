List Gradle Dependencies
========================

**Use:** To use simply run the following command.

`./gradlew dependencies`

**Note:**  You will probably have to set the `ANDROID_HOME ` environment variable
in order for this Gradle script to work.

`ANDROID_HOME=~/Library/Developer/Xamarin/android-sdk-macosx ./gradlew dependencies`

Also notice in the build.gradle file there are specific versions of the 
Android SDK and Build Tools that is set.  

	compileSdkVersion 26
	buildToolsVersion "26.0.1"
	
You will need to ensure that you have these versions in the local SDK Manager or change the values in build.gradle to work with your local configuration.

**Output:**

Look for the **compile** section in the output.  This will provide the tree
of dependencies.

	compile - Classpath for compiling the main sources.
	\--- com.android.support:support-vector-drawable:25.4.0
	     +--- com.android.support:support-annotations:25.4.0
	     \--- com.android.support:support-compat:25.4.0
	          \--- com.android.support:support-annotations:25.4.0
	          
**Changing Inputs:**

As with any Android Studio project you change the dependencies section of the
build.gradle file to instruct Gradle as to which packages to process.

	dependencies {
		compile 'com.android.support:support-vector-drawable:25.4.0'
	}