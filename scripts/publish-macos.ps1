dotnet publish -c Release -r osx-arm64  -o ../publish/macos/RKCheckList/Contents/MacOS -p:PublishReadyToRun=true ../src/RKCheckList/RKCheckList.csproj
cp -r ../macos-app-template/* ../publish/macos/RKCheckList/Contents
mv ../publish/macos/RKCheckList ../publish/macos/RKCheckList.app