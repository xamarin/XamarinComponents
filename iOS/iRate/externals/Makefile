
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
LIPO=lipo

GIT_REPO_VERSION=1.12.2

PROJECT_ROOT=./iRateLib
PROJECT=$(PROJECT_ROOT)/iRateLib.xcodeproj
TARGET=iRateLib

all : iRateLib/lib$(TARGET).a

iRateLib/iRate/README.md : 
	git clone https://github.com/nicklockwood/iRate.git iRateLib/iRate
	cd iRateLib/iRate && git checkout $(GIT_REPO_VERSION)
	touch $@

iRateLib/lib$(TARGET)-i386.a: iRateLib/iRate/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphonesimulator/lib$(TARGET).a $@

iRateLib/lib$(TARGET)-armv7.a: iRateLib/iRate/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

iRateLib/lib$(TARGET)-armv7s.a: iRateLib/iRate/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7s -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

iRateLib/lib$(TARGET)-arm64.a: iRateLib/iRate/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

iRateLib/lib$(TARGET).a : iRateLib/lib$(TARGET)-i386.a iRateLib/lib$(TARGET)-armv7.a iRateLib/lib$(TARGET)-armv7s.a iRateLib/lib$(TARGET)-arm64.a
	$(LIPO) -create -output $@ $^


clean :
	rm -rf iRateLib/iRate iRateLib/*.a iRateLib/build iRate
