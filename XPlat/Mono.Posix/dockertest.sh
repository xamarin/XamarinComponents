#!/bin/bash

cd /hostdir/Test

cp Mono.Posix.Test-netstandard_2_0.csproj Mono.Posix.Test-netstandard_2_0.copy.csproj

dotnet restore Mono.Posix.Test-netstandard_2_0.copy.csproj
dotnet add Mono.Posix.Test-netstandard_2_0.copy.csproj package -s packagesource Mono.Posix.Core -v $1
dotnet restore Mono.Posix.Test-netstandard_2_0.copy.csproj
dotnet build Mono.Posix.Test-netstandard_2_0.copy.csproj
dotnet run -p Mono.Posix.Test-netstandard_2_0.copy.csproj
rm Mono.Posix.Test-netstandard_2_0.copy.csproj