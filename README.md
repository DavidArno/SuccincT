## Succinc\<T\> ##
#### Discriminated unions, pattern matching and partial applications for C#  ####
[![Build Status](https://ci.appveyor.com/api/projects/status/github/DavidArno/Succinct?svg=true)](https://travis-ci.org/DavidArno/SuccincT) &nbsp;[![NuGet](https://img.shields.io/nuget/v/SuccincT.svg)](http://www.nuget.org/packages/SuccincT)
----------
### Introduction ###
Succinc\<T\> is a small .NET framework that started out as a means of providing an elegant solution to the problem of functions that need return a success state and value, or a failure state. Initially, it consisted of a few `Parse` methods that returned an `ISuccess<T>` result. Then I started learning F#...

Now Succinc\<T\> has grown into a library that provides discriminated unions, pattern matching and partial applications for C#, in addition to providing a set of value parsers that do away with the need for `out` parameters and exceptions, and instead return return an `Option<T>`.

### Current Release ###
The current release of Succinc\<T\> is 1.4.1, which is [available as a nuget package](https://www.nuget.org/packages/SuccincT/). 

This release changes Succinc\<T\> to be a portable class library (PCL). This then allows other PCL's to use Succinc\<T\>. It should have had no other effect.
### Features ###
#### Discriminated Unions ####
Succinc\<T\> provides a set of union types ([`Union<T1, T2>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2), [`Union<T1, T2, T3>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2T3) and [`Union<T1, T2, T3, T4>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2T3T4)) where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of [`Option<T>`](https://github.com/DavidArno/SuccincT/wiki/Option_T_) that can have the value `Some<T>` or `None`.

Succinc\<T\> uses [`Option<T>`](https://github.com/DavidArno/SuccincT/wiki/Option_T_) to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and `None` when the string is not a valid value for that type. No more `out` parameters!

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

#### Partial Applications ####
Succinc\<T\> supports partial function applications. A parameter can be supplied to a multi-parameter method and a new function will be returned that takes the remaining parameters. For example:

```csharp
Func<int,int> times = (p1, p2) => p1 * p2;
var times8 = times.Apply(8);
var result = times8(9); // <- result == 72
```

See the [Succinc\<T\> partial applications guide](https://github.com/DavidArno/SuccincT/wiki/PartialFunctionApplications) for more details.
