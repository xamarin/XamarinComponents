#!/bin/bash

cd /hostdir/monorepo/mcs/class/Mono.Posix

dotnet restore Mono.Posix-netstandard_2_0.csproj

mkdir -p build/any
dotnet build Mono.Posix-netstandard_2_0.csproj /p:ForceUseLibC=false -c Release
cp bin/Release/netstandard2.0/Mono.Posix.dll build/any/

dotnet clean Mono.Posix-netstandard_2_0.csproj -c Release

mkdir -p build/unix
dotnet build Mono.Posix-netstandard_2_0.csproj /p:ForceUseLibC=true -c Release
cp bin/Release/netstandard2.0/Mono.Posix.dll build/unix/

