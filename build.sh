#!/bin/bash
# use Ubuntu 16.10
repoFolder="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

bonbonniereZip="https://github.com/guoxin-qiu/Bonbonniere/archive/master.zip"

buildFolder="$repoFolder/build"

tempFolder="/tmp/Bonbonniere-$(uuidgen)"
mkdir $tempFolder

localZipFile="$tempFolder/master.zip"
wget -O $localZipFile $bonbonniereZip

unzip -q -d $tempFolder $localZipFile

if test ! -d $buildFolder; then
    mkdir $buildFolder
fi
cp -r $tempFolder/**/src/**/ $buildFolder

# Cleanup
if test -d $tempFolder; then
    rm -rf $tempFolder
fi

cd $buildFolder/Bonbonniere.Website
dotnet restore
dotnet publish -o $repoFolder/publish -c Release

docker rm bonbonniere -f
docker rmi bonbonniere:1.0 -f
cd $repoFolder
docker build -t bonbonniere:1.0 .
docker run --name bonbonniere -d -p 8000:80 bonbonniere:1.0