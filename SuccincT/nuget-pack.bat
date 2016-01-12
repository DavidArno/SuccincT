echo off
erase *.nupkg
C:\Development\NuGet\nuget.exe pack SuccincT.csproj -Prop Configuration=Release -Symbols
