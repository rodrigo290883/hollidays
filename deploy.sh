#! /bin/sh

date

cd ~/Proyectos/hollidays

dotnet claen Desconectate.csproj --nologo

dotnet publish Desconectate.csproj --configuration release

cp -a ~/Proyectos/hollidays/bin/release/netcoreapp3.1/publish/. /var/desconectate

sudo chmod -R 777 /var/desconectate

sudo systemctl restart kestrel-aspdotnetcore.service

