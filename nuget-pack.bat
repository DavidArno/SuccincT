echo off
cd SuccincT
erase *.nupkg
C:\bin\nuget.exe pack SuccincT.csproj -Prop Configuration=Release -Symbols
cd ..\SuccincT.JSON
C:\bin\nuget.exe pack SuccincT.JSON.csproj -Prop Configuration=Release -Symbols
move *.nupkg ..\SuccincT
cd ..
