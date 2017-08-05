
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
LIPO=lipo

JSQ_PROJECT_ROOT=./ios/JSQMessagesViewControllerLib
JSQ_PROJECT=$(JSQ_PROJECT_ROOT)/JSQMessagesViewControllerLib.xcodeproj
JSQ_TARGET=JSQMessagesViewControllerLib

all :  $(JSQ_TARGET)/lib$(JSQ_TARGET).a ios/Resources/JSQMessagesViewController.nib ../samples/XamarinChat/Resources/jsq_messages_sample.m4a

../samples/XamarinChat/Resources/jsq_messages_sample.m4a: ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	cp ios/JSQMessagesViewController/JSQMessagesDemo/jsq_messages_sample.m4a ../samples/XamarinChat/Resources/jsq_messages_sample.m4a

ios/JSQSystemSoundPlayer/JSQSystemSoundPlayer/Classes/JSQSystemSoundPlayer.h :
	mkdir -p ios
	cd ios && git clone https://github.com/jessesquires/JSQSystemSoundPlayer.git
	cd ios/JSQSystemSoundPlayer && git checkout 2.0.1

ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h : ios/JSQSystemSoundPlayer/JSQSystemSoundPlayer/Classes/JSQSystemSoundPlayer.h 
	mkdir -p ios
	cd ios && git clone https://github.com/jessesquires/JSQMessagesViewController.git
	cd ios/JSQMessagesViewController && git checkout 7.3.4
	mkdir ios/JSQMessagesViewController/JSQMessagesViewController/JSQSystemSoundPlayer
	cp ios/JSQSystemSoundPlayer/JSQSystemSoundPlayer/Classes/* ios/JSQMessagesViewController/JSQMessagesViewController/JSQSystemSoundPlayer/
	sed -i '' 's/#import <JSQSystemSoundPlayer\/JSQSystemSoundPlayer.h>/#import "JSQSystemSoundPlayer.h"/g' ios/JSQMessagesViewController/JSQMessagesViewController/Categories/JSQSystemSoundPlayer+JSQMessages.h




$(JSQ_TARGET)/lib$(JSQ_TARGET)-i386.a: ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	mkdir -p $(JSQ_TARGET)
	$(XCODEBUILD) -project $(JSQ_PROJECT) -target $(JSQ_TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv $(JSQ_PROJECT_ROOT)/build/Release-iphonesimulator/lib$(JSQ_TARGET).a $@

$(JSQ_TARGET)/lib$(JSQ_TARGET)-armv7.a: ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	mkdir -p $(JSQ_TARGET)
	$(XCODEBUILD) -project $(JSQ_PROJECT) -target $(JSQ_TARGET) -sdk iphoneos -arch armv7 -configuration Release clean build
	-mv $(JSQ_PROJECT_ROOT)/build/Release-iphoneos/lib$(JSQ_TARGET).a $@

$(JSQ_TARGET)/lib$(JSQ_TARGET)-armv7s.a: ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	mkdir -p $(JSQ_TARGET)
	$(XCODEBUILD) -project $(JSQ_PROJECT) -target $(JSQ_TARGET) -sdk iphoneos -arch armv7s -configuration Release clean build
	-mv $(JSQ_PROJECT_ROOT)/build/Release-iphoneos/lib$(JSQ_TARGET).a $@

$(JSQ_TARGET)/lib$(JSQ_TARGET)-arm64.a: ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	mkdir -p $(JSQ_TARGET)
	$(XCODEBUILD) -project $(JSQ_PROJECT) -target $(JSQ_TARGET) -sdk iphoneos -arch arm64 -configuration Release clean build
	-mv $(JSQ_PROJECT_ROOT)/build/Release-iphoneos/lib$(JSQ_TARGET).a $@

$(JSQ_TARGET)/lib$(JSQ_TARGET).a: $(JSQ_TARGET)/lib$(JSQ_TARGET)-i386.a $(JSQ_TARGET)/lib$(JSQ_TARGET)-armv7.a $(JSQ_TARGET)/lib$(JSQ_TARGET)-armv7s.a $(JSQ_TARGET)/lib$(JSQ_TARGET)-arm64.a
	$(LIPO) -create -output $@ $^

ios/Resources/JSQMessagesViewController.nib  : ios/JSQMessagesViewController/JSQMessagesViewController/JSQMessages.h
	mkdir -p iOS/Resources
	for XIBFILE in `find ios/JSQMessagesViewController/JSQMessagesViewController/  -name "*.xib"`; do \
		XIBFILENAMEEXT=$$(basename "$$XIBFILE"); \
		XIBFILENAME="$${XIBFILENAMEEXT%.*}"; \
		echo $$XIBFILENAME; \
		ibtool --output-format human-readable-text --compile iOS/Resources/$$XIBFILENAME.nib $$XIBFILE; \
	done

clean :
	rm -rf ios/JSQMessagesViewController ios/JSQSystemSoundPlayer iOS/Resources JSQMessagesViewControllerLib \
	ios/JSQMessagesViewControllerLib/build ../samples/XamarinChat/Resources/jsq_messages_sample.m4a
