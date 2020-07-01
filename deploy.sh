#! /bin/sh

date

chmod -R 777 /home/grupomodelo/Proyectos/hollidays

cd /home/grupomodelo/Proyectos/hollidays

dotnet clean Desconectate.csproj --nologo

dotnet publish Desconectate.csproj --configuration release

cp -a /home/grupomodelo/Proyectos/hollidays/bin/release/netcoreapp3.1/publish/. /var/desconectate

sudo chmod -R 777 /var/desconectate

sudo systemctl restart kestrel-aspdotnetcore.service

