echo publish Windows platform self contained
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimmerRemoveSymbols=true --self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/win-x64-nodependencies

echo publish Windows platform no self contained
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true  --no-self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/win-x64-dependencies

echo publish linux platform self contained
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimmerRemoveSymbols=true --self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/linux-x64-nodependencies

echo publish linux platform no self contained
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --no-self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/linux-x64-dependencies

echo publish macos platform self contained
dotnet publish -c Release -r osx-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true -p:TrimmerRemoveSymbols=true --self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/macos-x64-nodependencies

echo publish macos platform no self contained
dotnet publish -c Release -r osx-x64 -p:PublishSingleFile=true --no-self-contained -o RadYanFoFaViewer/bin/Release/net6.0/publish/macos-x64-dependencies
