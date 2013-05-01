## SuccincT ##

**SuccincT** is a small .NET framework that provides an elegant solution to the problem of functions that need return a success state and value, or a failure state.

### What's it for? ###
All too often the .NET framework has methods that throw exceptions for non-exceptional reasons. Quite often no accompanying test method is provided to allow the exception to be avoided. In other cases, a single test & do method is provided, which needs an out parameter to return part of the result.

SuccincT provides the `ISuccess<T>` interface, which is a simple, elegant replacement to cumbersome exceptions and out parameters. It neatly encapsulates a success state, a value that is only valid when successful and an error message that's only valid when not. In addition it provides replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`, all of which use `ISuccess<T>` to report their results. As an added bonus, this methods are all implemented as extension methods to `string`. 

### State of the project ##
At this early stage, I have simply uploaded the VS2012 solution and support files to GitHub. Usage notes and a NuGet package will be added soon.