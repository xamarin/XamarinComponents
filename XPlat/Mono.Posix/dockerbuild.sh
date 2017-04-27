#!/bin/bash

cd /hostdir/monorepo/mcs/class/Mono.Posix

dotnet restore Mono.Posix-netstandard_2_0.csproj
dotnet build Mono.Posix-netstandard_2_0.csproj
