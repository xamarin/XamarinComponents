Mono.Posix for .Net Standard
========================

[![Components-Mono.Posix][7]][8]

The purpose of this folder project is to provide build scripts and NuGet spec files for Mono.Posix
for .Net Standard.

Building the Mono.Posix.NETStandard NuGet
----------------------------------

**Prerequisites**

- MacOS or Linux
- [make][1]
- [Docker][2]
- [Mono][3] - only for NuGet pack.  Can not find a way to get .Net Core to pack a nuspec or sign dlls.

**Building**

Just run `make`.

The Makefile will fetch all of the Posix Helpers from [Jenkins Mono builds,][4] fetch the [Mono.Posix code][5] from the Mono Github repository, build a netstandard 2.0 targeted version of Mono.Posix.dll using a docker image containing a [build of .Net Core 2.0,][6] and pack the Mono.Posix.Core NuGet.  The resulting NuGet package will be created in the build directory.

Todo Items
-----------------------

 - Remove the manual patch from the Makefile when [Miguel's fix][miguelfix1] lands in a Mono release.

[1]: http://stackoverflow.com/a/10265766
[2]: https://www.docker.com/
[3]: http://www.mono-project.com/download/#download-mac
[4]: https://jenkins.mono-project.com/view/All/job/ng-extract-libmonoposixhelper/
[5]: https://github.com/mono/mono/tree/master/mcs/class/Mono.Posix
[6]: https://hub.docker.com/r/microsoft/dotnet-nightly/
[7]: https://jenkins.mono-project.com/view/Components/job/Components-Mono.Posix/badge/icon
[8]: https://jenkins.mono-project.com/view/Components/job/Components-Mono.Posix
[miguelfix1]: https://github.com/mono/mono/pull/7024
