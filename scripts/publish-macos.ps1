# Cleanup previous contents of publish directory
Remove-Item ../publish -Recurse -Force
mkdir ../publish

# Create macOS app package
dotnet publish -c Release -r osx-arm64 --self-contained -o ../publish/RKCheckList/Contents/MacOS -p:PublishReadyToRun=true ../src/RKCheckList/RKCheckList.csproj
cp -r ../macos-app-template/* ../publish/RKCheckList/Contents
mv ../publish/RKCheckList ../publish/RKCheckList.app