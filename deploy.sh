#! /bin/bash

cd ~/Proyectos/hollidays

git pull

rm -R ~/Proyectos/hollidays/bin/release

dotnet publish Desconectate.csproj --configuration release

sudo cp -a ~/Proyectos/hollidays/bin/release/netcoreapp3.1/publish/. /var/desconectate

sudo systemctl restart apache2

