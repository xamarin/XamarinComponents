MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild
#VERSION=1.0.4

all: UrlImageViewHelper

#PrepareJar:
#	curl -L -G -o Jars/urlimageviewhelper-$(VERSION).jar -d "r=central-proxy&g=com.koushikdutta.urlimageviewhelper&a=urlimageviewhelper&v=$(VERSION)" http://repository.sonatype.org/service/local/artifact/maven/redirect
	
#UrlImageViewHelper: PrepareJar
UrlImageViewHelper:
	$(MONOXBUILD) /p:Configuration=Release UrlImageViewHelper.csproj
	cp bin/Release/UrlImageViewHelper.dll UrlImageViewHelper.dll

clean:
	-rm -rf bin/ obj/ *.sln *.userprefs *.dll Jars/*.jar