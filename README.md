## Succinc\<T\> ##
#### Discriminated unions, pattern matching and functional composition for C#  ####
[![Build Status](https://travis-ci.org/DavidArno/SuccincT.svg?branch=master)](https://travis-ci.org/DavidArno/SuccincT) &nbsp;[![NuGet](https://img.shields.io/nuget/v/SuccincT.svg)](http://www.nuget.org/packages/SuccincT)
----------
### Introduction ###
Succinc\<T\> is a small .NET framework that started out as a means of providing an elegant solution to the problem of functions that need return a success state and value, or a failure state. Initially, it consisted of a few `Parse` methods that returned an `ISuccess<T>` result. Then I started learning F#...

Now Succinc\<T\> has grown into a library that provides discriminated unions, pattern matching and functional composition for C#, in addition to providing a set of value parsers that do away with the need for `out` parameters and exceptions, and instead return return an `Option<T>`.

### Current Release ###
The current, and initial, release of Succinc\<T\> is 1.0.0, which is [available as a nuget package](https://www.nuget.org/packages/SuccincT/). 

### Coming next ####
1. The description of the nuget package contains a typo. This will be fixed in the next release.
2. The documentation is slowly improving, but needs more work. The wiki needs completing and some more useful comments need adding to the code.
3. Currently `.Do()` and `Else()` require an `Action` or `Func<TResult>` for match and exec and match and result respectively. Often though, a fixed value will be returned for both. Therefore, overloaded versions that take a parameter of `T` will be introduced soon.
4. Longer term, I plan on experimenting with tuples, to see if Succinc\<T\> can offer an improvement on the current clunky `Tuple` classes, maybe through some sort of `ITuple` interface that can allow any value object to be treated as a tuple. Watch this space for that one. 

### Features ###
#### Discriminated Unions ####
Succinc\<T\> provides a set of union types (`Union<T1,T2>` through to `Union<T1,T2,T3,T4>`) where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of `Option<T>` that can have the value `Some<T>` or `None`.

Succinc\<T\> uses `Option<T>` to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and `None` when the string is not a valid value for that type. No more `out` parameters!

#### Pattern Matching ####
Succinc\<T\> can pattern match values, unions etc in a way similar to F#'s pattern matching features. It uses a fluent syntax to achieve this as shown by the following example:
```csharp
public static void PrintColorName(Color color)
{
    color.Match()
         .With(Color.Red).Do(x => Console.WriteLine("Red"))
         .With(Color.Green).Do(x => Console.WriteLine("Green"))
         .With(Color.Blue).Do(x => Console.WriteLine("Blue"))
         .Exec();
}
```

See the [Succinc\<T\> pattern matching guide](https://github.com/DavidArno/SuccincT/wiki/PatternMatching) for more details.

#### Functional Composition ####
Succinc\<T\> supports functional composition. A parameter can be supplied to a multi-parameter method and a new function will be returned that takes the remaining parameters. For example:

```csharp
Func<int,int> times = (p1, p2) => p1 * p2;
var times8 = times.Compose(8);
var result = times8(9); // <- result == 72
```
