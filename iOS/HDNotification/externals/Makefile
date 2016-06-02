
POD=pod
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
LIPO=lipo

PROJECT_ROOT=./HDNotificationViewLib
PROJECT=$(PROJECT_ROOT)/HDNotificationView.xcodeproj
TARGET=HDNotificationView

all: HDNotificationViewLib/lib$(TARGET).a

HDNotificationViewLib/lib$(TARGET)-i386.a: HDNotificationView/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphonesimulator/lib$(TARGET).a $@

HDNotificationViewLib/lib$(TARGET)-armv7.a: HDNotificationView/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

HDNotificationViewLib/lib$(TARGET)-arm64.a: HDNotificationView/README.md
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	-mv $(PROJECT_ROOT)/build/Release-iphoneos/lib$(TARGET).a $@

HDNotificationViewLib/lib$(TARGET).a: HDNotificationViewLib/lib$(TARGET)-i386.a HDNotificationViewLib/lib$(TARGET)-armv7.a HDNotificationViewLib/lib$(TARGET)-arm64.a
	$(LIPO) -create -output $@ $^


HDNotificationView/README.md:
	git clone git@github.com:nhdang103/HDNotificationView.git
	cd HDNotificationView; git checkout 2aa88dd65a2e7e59f55fa148fa2dfb25e1aed2f6;
	touch $@

clean:
	rm -rf HDNotificationView HDNotificationViewLib/libHDNotificationView*.a HDNotificationViewLib/build
