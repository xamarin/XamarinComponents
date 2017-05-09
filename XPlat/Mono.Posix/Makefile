MONO_VERSION=5.0.0.94
MONO_GIT_REPO=bholmes/mono
# MONOGIT_VERSION=mono-$(MONO_VERSION)
MONOGIT_VERSION=25baeed742592c3536c2f2897c1f689a5b3f828b
MONOPOSIX_NUGET_VERSION=5.0.0.94-alpha1
MONOPOSIXHELPER_JENKINSBUILD=9

DOCKER_PULL_TARGET=microsoft/dotnet-nightly@sha256:5f3394a34f719508eeaa399617508757ba263121324ac621ce5fd1cc4eba1d79
DOCKER_RUN_IMAGE=microsoft/dotnet-nightly:1a40a56d3491

$(eval DOCKER_BUILD_CONTAINER_NAME=MonoPosixBuild-$(shell uuidgen))
$(eval DOCKER_TEST_CONTAINER_NAME=MonoPosixBuild-$(shell uuidgen))

DISTRO_LIST=debian-7-arm debian-7-armel debian-7-x64 debian-7-x86 debian-8-arm debian-8-arm64 debian-8-armel debian-8-ppc64el \
 debian-8-x64 debian-8-x86 raspbian-8-arm ubuntu-12.04-arm ubuntu-12.04-x64 ubuntu-12.04-x86 ubuntu-14.04-arm \
 ubuntu-14.04-arm64 ubuntu-14.04-ppc64el ubuntu-14.04-x64 ubuntu-14.04-x86 ubuntu-16.04-arm ubuntu-16.04-arm64 \
 ubuntu-16.04-ppc64el ubuntu-16.04-x64 ubuntu-16.04-x86

all: build/Mono.Posix.Core.$(MONOPOSIX_NUGET_VERSION).nupkg

# Fetch mono from Github using tagged version
# Only extract what is needed
mono-source.zip:
	curl -L -o $@ https://github.com/$(MONO_GIT_REPO)/archive/$(MONOGIT_VERSION).zip
	touch $@

monorepo/mcs/class/Mono.Posix/Makefile: mono-source.zip
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/class/Mono.Posix/*'
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/class/mono.pub'
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/class/mono.snk'
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/build/common/*'
	mv mono-$(MONOGIT_VERSION) monorepo
	sed 's/@MONO_VERSION@/$(MONO_VERSION)/g'  monorepo/mcs/build/common/Consts.cs.in | sed 's/@MONO_CORLIB_VERSION@/0/g' > monorepo/mcs/build/common/Consts.cs
	touch $@

# using the nightly dotnetcore build for 2.0 preview to make Mono.Posix for netstandard 2.0
build/unix/Mono.Posix.dll build/any/Mono.Posix.dll: monorepo/mcs/class/Mono.Posix/Makefile
	mkdir -p build/any
	mkdir -p build/unix
	docker pull $(DOCKER_PULL_TARGET)
	docker run --name $(DOCKER_BUILD_CONTAINER_NAME) -v `pwd`:/hostdir  $(DOCKER_RUN_IMAGE) sh /hostdir/dockerbuild.sh || (docker rm $(DOCKER_BUILD_CONTAINER_NAME) && false)
	docker rm $(DOCKER_BUILD_CONTAINER_NAME)
	cp monorepo/mcs/class/Mono.Posix/build/any/Mono.Posix.dll build/any/Mono.Posix.dll
	cp monorepo/mcs/class/Mono.Posix/build/unix/Mono.Posix.dll build/unix/Mono.Posix.dll
	sn -R build/any/Mono.Posix.dll monorepo/mcs/class/mono.snk
	sn -R build/unix/Mono.Posix.dll monorepo/mcs/class/mono.snk

# Fetch all of the libMonoPosixHelper.so files
# Will be easier when this is all in Jenkins
MonoPosixHelper/%/libMonoPosixHelper.so: DISTRO_NAME = $(@:MonoPosixHelper/%/libMonoPosixHelper.so=%)
MonoPosixHelper/%/libMonoPosixHelper.so:
	mkdir -p MonoPosixHelper/$(DISTRO_NAME)
	curl -o MonoPosixHelper/$(DISTRO_NAME)/libMonoPosixHelper.so -L https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/$(MONOPOSIXHELPER_JENKINSBUILD)/results/$(DISTRO_NAME)/libMonoPosixHelper.so

# Special Case the OSX fetch
# Again will be easier when this is all in Jenkins
MonoPosixHelper/osx-10.7-universal/libMonoPosixHelper.dylib:
	mkdir -p MonoPosixHelper/osx-10.7-universal
	curl -o MonoPosixHelper/osx-10.7-universal/libMonoPosixHelper.dylib -L https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/$(MONOPOSIXHELPER_JENKINSBUILD)/results/osx-10.7-universal/libMonoPosixHelper.dylib

# Fetch a specific version of NuGet client
tools/nuget.exe:
	mkdir tools
	curl -L -o $@ https://dist.nuget.org/win-x86-commandline/v4.1.0/nuget.exe

# Pack the nuget
build/Mono.Posix.Core.$(MONOPOSIX_NUGET_VERSION).nupkg: tools/nuget.exe \
 build/any/Mono.Posix.dll \
 build/unix/Mono.Posix.dll \
 $(addprefix MonoPosixHelper/, $(addsuffix /libMonoPosixHelper.so,$(DISTRO_LIST))) \
 MonoPosixHelper/osx-10.7-universal/libMonoPosixHelper.dylib \
 Mono.Posix.Core.nuspec
	mono tools/nuget.exe pack -Version $(MONOPOSIX_NUGET_VERSION) -OutputDirectory build/  Mono.Posix.Core.nuspec

test: all
	rm -rf Test/packagesource/
	mkdir -p Test/packagesource
	mono tools/nuget.exe add -Source Test/packagesource build/Mono.Posix.Core.$(MONOPOSIX_NUGET_VERSION).nupkg
	docker pull $(DOCKER_PULL_TARGET)
	docker run --name $(DOCKER_TEST_CONTAINER_NAME) -v `pwd`:/hostdir  $(DOCKER_RUN_IMAGE) sh /hostdir/dockertest.sh $(MONOPOSIX_NUGET_VERSION) || (docker rm $(DOCKER_TEST_CONTAINER_NAME) && false)
	docker rm $(DOCKER_TEST_CONTAINER_NAME)

clean: 
	rm -rf mono-source.zip monorepo build MonoPosixHelper tools Test/packagesource Test/TestResult.xml Test/bin Test/obj \
	Mono.Posix.Test-netstandard_2_0.copy.csproj

.PHONY: clean test all

