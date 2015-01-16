msbuild.exe ..\Danio.sln /p:Configuration=Release
nuget pack Danio.csproj -Symbols -Prop Configuration=Release
pause
