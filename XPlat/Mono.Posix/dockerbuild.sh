#!/bin/bash

cd /hostdir/monorepo/mcs/class/Mono.Posix

dotnet restore --configfile /hostdir/nuget.config Mono.Posix.NETStandard-netstandard_2_0.csproj

mkdir -p build/any
dotnet build Mono.Posix.NETStandard-netstandard_2_0.csproj /p:ForceUseLibC=false -c Release
cp bin/Release/netstandard2.0/Mono.Posix.NETStandard.dll build/any/

dotnet clean Mono.Posix.NETStandard-netstandard_2_0.csproj -c Release

mkdir -p build/unix
dotnet build Mono.Posix.NETStandard-netstandard_2_0.csproj /p:ForceUseLibC=true -c Release
cp bin/Release/netstandard2.0/Mono.Posix.NETStandard.dll build/unix/

