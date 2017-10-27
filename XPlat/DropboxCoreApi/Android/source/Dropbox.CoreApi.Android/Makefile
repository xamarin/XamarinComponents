MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild
VERSION=1.6.3

all: DropboxSync.Android.dll

PrepareJar:
	curl -L 'https://www.dropbox.com/developers/downloads/sdks/core/android/dropbox-android-sdk-$(VERSION).zip' > dropbox-android-sdk-$(VERSION).zip
	mkdir -p Jars/
	unzip -p dropbox-android-sdk-$(VERSION).zip 'dropbox-android-sdk-$(VERSION)/lib/dropbox-android-sdk-$(VERSION).jar' > Jars/dropbox-android-sdk-$(VERSION).jar
	unzip -p dropbox-android-sdk-$(VERSION).zip 'dropbox-android-sdk-$(VERSION)/lib/json_simple-1.1.jar' > Jars/json_simple-1.1.jar
	unzip dropbox-android-sdk-$(VERSION).zip

Supportv4:
	cd ../../../../XamAndroidSupportV4/source/binding/ && make

DropboxSync.Android.dll: PrepareJar
	$(MONOXBUILD) /p:Configuration=Release Dropbox.CoreApi.Android.csproj
	cp bin/Release/Dropbox.CoreApi.Android.dll Dropbox.CoreApi.Android.dll

clean:
	rm -rf bin/ obj/ *.sln *.userprefs *.dll *.zip Jars/ dropbox-android-sdk-$(VERSION)/