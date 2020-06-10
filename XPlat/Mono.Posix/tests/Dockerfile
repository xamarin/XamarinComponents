FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /working

COPY . .

RUN dotnet add Test.csproj package Mono.Posix.NETStandard -v @NUGET_VERSION@
RUN dotnet restore

RUN dotnet build

RUN dotnet run --result="TestResults.xml;format=nunit2"; exit 0
