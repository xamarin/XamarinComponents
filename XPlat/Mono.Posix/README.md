Mono.Posix for .Net Core
========================

[![Components-Mono.Posix][7]][8]

The purpose of this folder project is to prepare and package Mono.Posix for 
.Net Core 2.0 as a NuGet.  The intention of these scripts is to help the 
conversation with Release Engineering.  Depending on discussions with RE the files
in this project may be moved or replaced.  

Building the Mono.Posix.Core NuGet
----------------------------------

**Prerequisites**

- MacOS - The Makefile has only been tested on MacOS.  I suspect it should have not problem on Linux as well.
- [make][1]
- [Docker][2]
- [Mono][3] - only for NuGet pack.  Can not find a way to get .Net Core to pack a nuspec.

**Building**

Just run `make`.

The Makefile will fetch all of the Posix Helpers from [Jenkins Mono builds,][4] fetch the [Mono.Posix code][5] from the Mono Github repository, build a netstandard 2.0 targeted version of Mono.Posix.dll using a docker image containing the [latest nightly build of .Net Core 2.0,][6] and pack the Mono.Posix.Core NuGet.  The resulting NuGet package will be created in the build directory.

Todo Items
-----------------------

 - Remove the `monochanges` folder
   - Need to send a PR to mono repo for code changes and add csproj
   - Add build step to generate Consts.cs
 - Add unit test run
 - add more arch builds for PosixHelper
 - Revisit package ID
 - Revisit all metadata in nuspec
 - When .Net Core 2.0 is released we can stop using Docker and install .Net Core 2.0 on a build bot

[1]: http://stackoverflow.com/a/10265766
[2]: https://www.docker.com/
[3]: http://www.mono-project.com/download/#download-mac
[4]: https://jenkins.mono-project.com/view/All/job/ng-extract-libmonoposixhelper/
[5]: https://github.com/mono/mono/tree/master/mcs/class/Mono.Posix
[6]: https://hub.docker.com/r/microsoft/dotnet-nightly/
[7]: https://jenkins.mono-project.com/view/Components/job/Components-Mono.Posix/badge/icon
[8]: https://jenkins.mono-project.com/view/Components/job/Components-Mono.Posix/badge/icon