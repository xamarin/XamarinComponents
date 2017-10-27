MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild
VERSION=1.3.13

all: Dropbox.CoreApi.iOS-Unified.dll Dropbox.CoreApi.iOS-Classic.dll

Prepare:
	curl -L 'https://www.dropbox.com/developers/downloads/sdks/core/ios/dropbox-ios-sdk-$(VERSION).zip' > dropbox-ios-sdk-$(VERSION).zip
	unzip -p dropbox-ios-sdk-$(VERSION).zip 'dropbox-ios-sdk-$(VERSION)/DropboxSDK.framework/DropboxSDK' > DropboxSDK.a

Dropbox.CoreApi.iOS-Unified.dll: Prepare 
	$(MONOXBUILD) /t:Clean Dropbox.CoreApi.iOS.csproj
	$(MONOXBUILD) /p:Configuration=Release Dropbox.CoreApi.iOS.csproj
	mkdir -p build/unified/
	cp bin/unified/Release/Dropbox.CoreApi.iOS.dll build/unified/Dropbox.CoreApi.iOS.dll

Dropbox.CoreApi.iOS-Classic.dll: Prepare 
	$(MONOXBUILD) /t:Clean Dropbox.CoreApi.iOS-Classic.csproj
	$(MONOXBUILD) /p:Configuration=Release Dropbox.CoreApi.iOS-Classic.csproj
	mkdir -p build/classic/
	cp bin/classic/Release/Dropbox.CoreApi.iOS.dll build/classic/Dropbox.CoreApi.iOS.dll

clean:
	rm -rf bin/ obj/ build/ *.sln *.userprefs *.dll *.zip *.a