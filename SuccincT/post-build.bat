for %%f in (Functional Options Parsers PatternMatchers Tuples Unions) do if not exist %1\..\SuccincT.Core\%%f mklink /D  %1\..\SuccincT.Core\%%f %1\%%f
for %%f in (CompilationTargetHandler.cs Version.cs) do if not exist %1\..\SuccincT.Core\%%f mklink %1\..\SuccincT.Core\%%f %1\%%f
