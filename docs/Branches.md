## Succinc\<T\> Active branches ##
Currently, there are two active branches: `master` and `csharp7migration`

All other branches are no longer used, should be considered dead.

### master branch ###
This is the branch from which releases are made (with the exception to v2.2.0, which was released from a now-retired branch, `serialization`).

It is currently compatible with VS2015 and C# 6. When VS2017 reaches RTM, this branch will then be switched to VS2017-only.

### csharp7migration branch ###
This branch contains code changes to take advantage of the new C# 7 features. Mostly, these changes are internal, ie the implementation details of the code have been changed to use expression bodies on constructors, tuples, throw expressions, out vars and the like.

There are some changes to `cons`, to allow deconstructing an enumerable to a `(head, tail)` tuple. See [Make use of C# 7 deconstruct feature to "super charge" IEnumerable<T> cons support](https://github.com/DavidArno/SuccincT/issues/15) for details. Additionally, tuples are used to supply a [new `Indexed` method to `IEnumerable<T>` that returns `IEnumerable<(int index, T item)>`](https://github.com/DavidArno/SuccincT/issues/11).

This branch is currently only compatible with VS2017RC3. Please note that:

1. A manual reference to `System.ValueTuple` has been added to `SuccincT` as it looks like [RC3 broke Nuget imports into portable projects](https://github.com/NuGet/Home/issues/4418).
2. Some people have found Succinc\<T\> crashes VS2017 when loaded. This may be due to features needed by the solution being missing, and poor error handling on the part of Visual Studio. So the major features I have installed are:
   * Universal Windows Platform Development
   * .NET Desktop development
   * ASP.NET and web development
   * Visual Studio extension development
   * .NET Core cross-platform development

If you find VS2017RC3 crashes on you and you have any of these features missing, please try adding them one at a time to see if that solves the problem. I would very much appreciate feedback from anyone who has this problem and finds a fix.




