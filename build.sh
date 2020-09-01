#! /bin/sh

dotnet clean Desconectate.csproj

dotnet publish Desconectate.csproj -c Release

dotnet ./bin/Release/netcoreapp3.1/publish/Desconectate.dll

