
POD=pod
XCODEBUILD=$(shell xcrun --sdk iphoneos --find xcodebuild)

PROJECT_ROOT=./Pods
PROJECT=$(PROJECT_ROOT)/Pods.xcodeproj
TARGETS=OfficeUIFabric

all: Pods $(TARGETS)

Pods:
	$(POD) install

$(TARGETS):
	$(MAKE) $@-iphoneos
	$(MAKE) $@-iphonesimulator
	lipo -create -output $@.framework/$@ $@.framework/$@ $@-iphonesimulator.framework/$@
	rm -rf $@-iphonesimulator.framework

%-iphoneos:
	$(eval currentTarget=$(subst -iphoneos,,$@))
	$(XCODEBUILD) -project $(PROJECT) -target $(currentTarget) -sdk iphoneos -arch arm64 -configuration Release build
	find ./build/Release-iphoneos/$(currentTarget) -type d -name "*.framework" | xargs -I % mv % ./

%-iphonesimulator:
	$(eval currentTarget=$(subst -iphonesimulator,,$@))
	$(XCODEBUILD) -project $(PROJECT) -target $(currentTarget) -sdk iphonesimulator -arch x86_64 -configuration Release build
	find ./build/Release-iphonesimulator/$(currentTarget) -type d -name "*.framework" | xargs -I % mv % $@.framework

clean:
	rm -rf Pods build *.framework

.PHONY: all clean