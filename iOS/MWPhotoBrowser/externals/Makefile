
POD=pod
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild

TARGET=MWPhotoBrowser
LIB_NAME=MWPhotoBrowser
LIB_OUTPUT=lib$(LIB_NAME).a
PROJ=Pods/Pods.xcodeproj

all: lib$(LIB_NAME).a Resources/ImageError.png

Podfile.lock :
	$(POD) install

lib$(LIB_NAME)-i386.a: Podfile.lock
	$(XCODEBUILD) -project $(PROJ) -target $(TARGET) -sdk iphonesimulator -arch i386 -configuration Release clean build
	mv build/Release-iphonesimulator/MWPhotoBrowser/$(LIB_OUTPUT) $@

lib$(LIB_NAME)-x86_64.a: Podfile.lock
	$(XCODEBUILD) -project $(PROJ) -target $(TARGET) -sdk iphonesimulator -arch x86_64 -configuration Release clean build
	mv build/Release-iphonesimulator/MWPhotoBrowser/$(LIB_OUTPUT) $@

lib$(LIB_NAME)-armv7.a: Podfile.lock
	$(XCODEBUILD) -project $(PROJ) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	mv build/Release-iphoneos/MWPhotoBrowser/$(LIB_OUTPUT) $@

lib$(LIB_NAME)-armv7s.a: Podfile.lock
	$(XCODEBUILD) -project $(PROJ) -target $(TARGET) -sdk iphoneos -arch armv7s -configuration Release clean build
	mv build/Release-iphoneos/MWPhotoBrowser/$(LIB_OUTPUT) $@

lib$(LIB_NAME)-arm64.a: Podfile.lock
	$(XCODEBUILD) -project $(PROJ) -target $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	mv build/Release-iphoneos/MWPhotoBrowser/$(LIB_OUTPUT) $@

lib$(LIB_NAME).a: lib$(LIB_NAME)-i386.a lib$(LIB_NAME)-x86_64.a lib$(LIB_NAME)-armv7.a lib$(LIB_NAME)-armv7s.a lib$(LIB_NAME)-arm64.a 
	lipo -create -output $@ $^

Resources/ImageError.png:
	mkdir -p Resources
	cp Pods/MWPhotoBrowser/Pod/Assets/* Resources/

clean:
	rm -rf Podfile.lock Pods *.a build/ Resources/

.PHONY: all clean

