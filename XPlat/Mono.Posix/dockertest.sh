#!/bin/bash

cd /hostdir/Test

cp Mono.Posix.NETStandard.Test-netstandard_2_0.csproj Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj

dotnet restore --configfile /hostdir/nuget.config Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj
dotnet add Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj package -s packagesource Mono.Posix.NETStandard -v $1
dotnet restore --configfile /hostdir/nuget.config Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj
dotnet build Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj
dotnet run -p Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj --result="TestResult-v2.xml;format=nunit2"
rm Mono.Posix.NETStandard.Test-netstandard_2_0.copy.csproj