
all: simpleandroidlibrary1.aar simpleandroidlibrary2.aar
	
simpleandroidlibrary1.aar:
	sh gradlew assembleRelease
	cp ./simpleandroidlibrary1/build/outputs/aar/simpleandroidlibrary1-release.aar $@

simpleandroidlibrary2.aar: simpleandroidlibrary1.aar
	cp ./simpleandroidlibrary2/build/outputs/aar/simpleandroidlibrary2-release.aar $@

clean:
	rm -rf simpleandroidlibrary1.aar simpleandroidlibrary2.aar simpleandroidlibrary1/build simpleandroidlibrary2/build