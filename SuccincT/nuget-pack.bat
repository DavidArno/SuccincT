echo off
erase *.nupkg
nuget pack SuccincT.csproj -Prop Configuration=Release -Symbols