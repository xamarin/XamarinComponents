# The mono version should come from an official release.
# Checking your About box in VS for Mac will give you an idea
# of what is released to stable.
# https://github.com/mono/mono/releases
MONO_VERSION=5.8.0.127
MONO_GIT_REPO=mono/mono
MONOGIT_VERSION=mono-$(MONO_VERSION)
# MONOGIT_VERSION=76f0a58429b4cfb47c77935cfca6ce88ce9be53e
MONOPOSIX_NUGET_VERSION=1.0.0
# We fetch the native posix helper code from existing Mono build artifacts
# To update the version a parameterized build needs to be run with the Mono verison
# specified above.
# https://jenkins.mono-project.com/view/All/job/ng-extract-libmonoposixhelper/
# For branch use stable, beta or alpha.  Check these urls to see what exists
# https://download.mono-project.com/repo/centos-stable/m/mono-core/
# https://download.mono-project.com/repo/centos-beta/m/mono-core/
# https://download.mono-project.com/repo/centos-alpha/m/mono-core/
MONOPOSIXHELPER_JENKINSBUILD=46

# docker images --digests
# https://hub.docker.com/r/microsoft/dotnet/
# https://hub.docker.com/r/microsoft/dotnet-nightly/
DOCKER_PULL_TARGET=microsoft/dotnet@sha256:83ea086418516a9b6311acf393f1474d241015902b380ac7c3dfe90f32b72dad
# DOCKER_RUN_IMAGE=microsoft/dotnet:411cc86cfa2e
DOCKER_RUN_IMAGE=411cc86cfa2e

$(eval DOCKER_BUILD_CONTAINER_NAME=MonoPosixBuild-$(shell uuidgen))
$(eval DOCKER_TEST_CONTAINER_NAME=MonoPosixBuild-$(shell uuidgen))

DISTRO_LIST=centos-6-x86 centos-6-x64 debian-8-arm debian-8-armel debian-8-arm64
WINDOWS_LIST=win-7-x64 win-7-x86

all: build/Mono.Posix.NETStandard.$(MONOPOSIX_NUGET_VERSION).nupkg

# Fetch mono from Github using tagged version
# Only extract what is needed
mono-source.zip:
	curl -L -o $@ https://github.com/$(MONO_GIT_REPO)/archive/$(MONOGIT_VERSION).zip
	touch $@

monorepo/mcs/class/Mono.Posix/Makefile: mono-source.zip
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/class/Mono.Posix/*'
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/class/Open.snk'
	unzip  mono-source.zip 'mono-$(MONOGIT_VERSION)/mcs/build/common/*'
	mv mono-$(MONOGIT_VERSION) monorepo
	sed 's/@MONO_VERSION@/$(MONO_VERSION)/g'  monorepo/mcs/build/common/Consts.cs.in | sed 's/@MONO_CORLIB_VERSION@/0/g' > monorepo/mcs/build/common/Consts.cs
	# Apply patch from Miguel https://github.com/mono/mono/pull/7024
	patch monorepo/mcs/class/Mono.Posix/Mono.Unix.Native/Syscall.cs PR-7024.patch
	touch $@

# using the nightly dotnetcore build for 2.0 preview to make Mono.Posix for netstandard 2.0
build/unix/Mono.Posix.NETStandard.dll build/any/Mono.Posix.NETStandard.dll: monorepo/mcs/class/Mono.Posix/Makefile
	mkdir -p build/any
	mkdir -p build/unix
	docker pull $(DOCKER_PULL_TARGET)
	docker run --name $(DOCKER_BUILD_CONTAINER_NAME) -v `pwd`:/hostdir  $(DOCKER_RUN_IMAGE) sh /hostdir/dockerbuild.sh || (docker rm $(DOCKER_BUILD_CONTAINER_NAME) && false)
	docker rm $(DOCKER_BUILD_CONTAINER_NAME)
	cp monorepo/mcs/class/Mono.Posix/build/any/Mono.Posix.NETStandard.dll build/any/Mono.Posix.NETStandard.dll
	cp monorepo/mcs/class/Mono.Posix/build/unix/Mono.Posix.NETStandard.dll build/unix/Mono.Posix.NETStandard.dll
	sn -R build/any/Mono.Posix.NETStandard.dll monorepo/mcs/class/Open.snk
	sn -R build/unix/Mono.Posix.NETStandard.dll monorepo/mcs/class/Open.snk

build/net40/Mono.Posix.NETStandard.dll: build/net40/Mono.Posix.generated.cs
	cd monorepo/mcs/class/Mono.Posix && \
		mcs -t:library -out:$(PWD)/$@ -r:Mono.Posix \
		-keyfile:$(PWD)/monorepo/mcs/class/Open.snk \
		"-d:MONO_POSIX_NETSTANDARD_BUILD;NETSTANDARD2_0" \
		$(PWD)/build/net40/Mono.Posix.generated.cs \
		$(PWD)/monorepo/mcs/class/Mono.Posix/Assembly/AssemblyInfo.cs

build/net40/Mono.Posix.generated.cs: tools/Microsoft.DotNet.BuildTools.GenAPI/tools/GenAPI.exe
	mkdir -p build/net40
	mono tools/Microsoft.DotNet.BuildTools.GenAPI/tools/GenAPI.exe -w:TypeForwards build/any/Mono.Posix.NETStandard.dll -out $@

tools/Microsoft.DotNet.BuildTools.GenAPI/tools/GenAPI.exe:
	mkdir -p tools
	curl -L -o tools/Microsoft.DotNet.BuildTools.GenAPI.nupkg https://www.nuget.org/api/v2/package/Microsoft.DotNet.BuildTools.GenAPI/1.0.0-beta-00081
	unzip tools/Microsoft.DotNet.BuildTools.GenAPI.nupkg -d tools/Microsoft.DotNet.BuildTools.GenAPI
	touch $@

# Fetch all of the libMonoPosixHelper.so files
# Will be easier when this is all in Jenkins
MonoPosixHelper/%/libMonoPosixHelper.so: DISTRO_NAME = $(@:MonoPosixHelper/%/libMonoPosixHelper.so=%)
MonoPosixHelper/%/libMonoPosixHelper.so:
	mkdir -p MonoPosixHelper/$(DISTRO_NAME)
	curl -o MonoPosixHelper/$(DISTRO_NAME)/libMonoPosixHelper.so -L https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/$(MONOPOSIXHELPER_JENKINSBUILD)/results/$(DISTRO_NAME)/libMonoPosixHelper.so

MonoPosixHelper/%/libMonoPosixHelper.dll: DISTRO_NAME = $(@:MonoPosixHelper/%/libMonoPosixHelper.dll=%)
MonoPosixHelper/%/libMonoPosixHelper.dll:
	mkdir -p MonoPosixHelper/$(DISTRO_NAME)
	curl -o MonoPosixHelper/$(DISTRO_NAME)/libMonoPosixHelper.dll -L https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/$(MONOPOSIXHELPER_JENKINSBUILD)/results/$(DISTRO_NAME)/libMonoPosixHelper.dll

MonoPosixHelper/%/MonoPosixHelper.dll: DISTRO_NAME = $(@:MonoPosixHelper/%/MonoPosixHelper.dll=%)
MonoPosixHelper/%/MonoPosixHelper.dll:
	mkdir -p MonoPosixHelper/$(DISTRO_NAME)
	curl -o MonoPosixHelper/$(DISTRO_NAME)/MonoPosixHelper.dll -L https://xamjenkinsartifact.blob.core.windows.net/ng-extract-libmonoposixhelper/$(MONOPOSIXHELPER_JENKINSBUILD)/results/$(DISTRO_NAME)/MonoPosixHelper.dll

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
build/Mono.Posix.NETStandard.$(MONOPOSIX_NUGET_VERSION).nupkg: tools/nuget.exe \
 build/any/Mono.Posix.NETStandard.dll \
 build/unix/Mono.Posix.NETStandard.dll \
 build/net40/Mono.Posix.NETStandard.dll \
 $(addprefix MonoPosixHelper/, $(addsuffix /libMonoPosixHelper.so,$(DISTRO_LIST))) \
 $(addprefix MonoPosixHelper/, $(addsuffix /libMonoPosixHelper.dll,$(WINDOWS_LIST))) \
 $(addprefix MonoPosixHelper/, $(addsuffix /MonoPosixHelper.dll,$(WINDOWS_LIST))) \
 MonoPosixHelper/osx-10.7-universal/libMonoPosixHelper.dylib \
 Mono.Posix.NETStandard.nuspec
	mono tools/nuget.exe pack -Version $(MONOPOSIX_NUGET_VERSION) -OutputDirectory build/  Mono.Posix.NETStandard.nuspec

test: all
	rm -rf Test/packagesource/
	mkdir -p Test/packagesource
	mono tools/nuget.exe add -Source Test/packagesource build/Mono.Posix.NETStandard.$(MONOPOSIX_NUGET_VERSION).nupkg
	docker pull $(DOCKER_PULL_TARGET)
	docker run --name $(DOCKER_TEST_CONTAINER_NAME) -v `pwd`:/hostdir  $(DOCKER_RUN_IMAGE) sh /hostdir/dockertest.sh $(MONOPOSIX_NUGET_VERSION) || (docker rm $(DOCKER_TEST_CONTAINER_NAME) && false)
	docker rm $(DOCKER_TEST_CONTAINER_NAME)

clean: 
	rm -rf mono-source.zip monorepo build MonoPosixHelper tools Test/packagesource Test/TestResult.xml Test/TestResult-v2.xml \
	Test/bin Test/obj Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj stdlibs

.PHONY: clean test all

