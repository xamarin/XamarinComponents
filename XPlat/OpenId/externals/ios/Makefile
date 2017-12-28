LIPO=lipo
POD=pod
XCODEBUILD=xcrun xcodebuild

all: libAppAuth.a

build/Release-iphonesimulator/AppAuth/libAppAuth.a:
	$(XCODEBUILD) -project Pods/Pods.xcodeproj  -target AppAuth  -sdk iphonesimulator -configuration Release GCC_TREAT_WARNINGS_AS_ERRORS=No clean build

build/Release-iphoneos/AppAuth/libAppAuth.a:
	$(XCODEBUILD) -project Pods/Pods.xcodeproj  -target AppAuth  -sdk iphoneos -configuration Release GCC_TREAT_WARNINGS_AS_ERRORS=No clean build

libAppAuth.a: build/Release-iphoneos/AppAuth/libAppAuth.a build/Release-iphonesimulator/AppAuth/libAppAuth.a
	$(LIPO) -create -output $@ $^

clean:
	rm -rf Podfile Podfile.lock	Pods build libAppAuth.a

.PHONY: all clean


