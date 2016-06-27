## Succinc\<T\> ##
#### Discriminated unions, pattern matching and partial applications for C#  ####
[![Build Status](https://ci.appveyor.com/api/projects/status/github/DavidArno/Succinct?svg=true)](https://ci.appveyor.com/project/DavidArno/succinct) &nbsp;[![NuGet](https://img.shields.io/nuget/v/SuccincT.svg)](http://www.nuget.org/packages/SuccincT)
----------
### Introduction ###
Succinc\<T\> is a small .NET framework that started out as a means of providing an elegant solution to the problem of functions that need return a success state and value, or a failure state. Initially, it consisted of a few `Parse` methods that returned an `ISuccess<T>` result. Then I started learning F#...

Now Succinc\<T\> has grown into a library that provides discriminated unions, pattern matching and partial applications for C#, in addition to providing a set of value parsers that do away with the need for `out` parameters and exceptions, and instead return return an `Option<T>`.

### Current Release ###
The current release of Succinc\<T\> is 2.0.0, which is [available as a nuget package](https://www.nuget.org/packages/SuccincT/). 

Please be warned that this release [includes a number of breaking changes. Please consult the documentation before installing](https://github.com/DavidArno/SuccincT/issues/6). 

This release includes:
* A completely rewritten implementation of pattern matching (keeping the same semantics, so this shouldn't break and existing uses),
* A new `Unit` type and associated methods for converting `Action<T..>` delegates to `Func<T...,Unit>` ones,
* New `Lambda`, `Transform`, `Func` and `Action` methods to simplify the declaration and typing of lambdas.
* A new `Maybe` type, which is a `struct` version of `Option`. This type is largely interchangable with `Option` and is provided for those that (a) prefer the semantics associated with "maybe" versus "option" and (b) those that prefer the idea of such a type being a `struct` (and those not able to be `null`.

### Features ###
#### Discriminated Unions ####
Succinc\<T\> provides a set of union types ([`Union<T1, T2>`](UnionT1T2), [`Union<T1, T2, T3>`](UnionT1T2T3) and [`Union<T1, T2, T3, T4>`](UnionT1T2T3T4))  where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of [`Option<T>`](Option_T_) and [`Maybe<T>`](Maybe_T_) that can have the value `Some<T>` or [`None`](None).

Succinc\<T\> uses [`Option<T>`](Option_T_) to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and [`None`](None) when the string is not a valid value for that type. No more `out` parameters! See the [Option Parsers guide](OptionParsers) for more details.

Further Succinc\<T\> uses [`Option<T>`](Option_T_) to [provide replacements for the XxxxOrDefault LINQ extension methods on `IEnumerable<T>`](IEnumerableExtensions). In all cases, these new extension methods, eg `TryFirst<T>()` return an option with a value if a match occurred, or `None` if not.

#### Pattern Matching ####
Succinc\<T\> can pattern match values, tuples, unions etc in a way similar to F#'s pattern matching features. It uses a fluent (method chaining) syntax to achieve this. Some examples of its abilities:

```csharp
public static void PrintColorName(Color color)
{
    color.Match()
         .With(Color.Red).Do(x => Console.WriteLine("Red"))
         .With(Color.Green).Do(x => Console.WriteLine("Green"))
         .With(Color.Blue).Do(x => Console.WriteLine("Blue"))
         .Exec();
}

public static string SinglePositiveOddDigitReporter(Option<int> data)
{
    return data.Match<string>()
               .Some().Of(0).Do(x => "0 isn't positive or negative")
               .Some().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
               .Some().Where(x => x > 9).Do(x => string.Format("{0} isn't 1 digit", x))
               .Some().Where(x => x < 0).Do(i => string.Format("{0} isn't positive", i))
               .Some().Do(x => string.Format("{0} isn't odd", x))
               .None().Do(() => string.Format("There was no value"))
               .Result();
}
```

See the [Succinc\<T\> pattern matching guide](PatternMatching) for more details.

#### Partial Applications ####
Succinc\<T\> supports partial function applications. A parameter can be supplied to a multi-parameter method and a new function will be returned that takes the remaining parameters. For example:

```csharp
var times = Lambda<double>((p1, p2) => p1 * p2);
var times8 = times.Apply(8);
var result = times8(9); // <- result == 72
```

See the [Succinc\<T\> partial applications guide](PartialFunctionApplications) for more details.

#### "Implicitly" Typed Lambdas ####
C# doesn't support implicitly typed lambdas, meaning it's not possible to declare something like:
```csharp
var times = (p1, p2) => p1 * p2;
```
Normally, `times` would have to explicitly typed:
```csharp
Func<double, double, double> times = (p1, p2) => p1 * p2;
```
Succinc\<T\> offers an alternative approach, taking advantage of the fact that `var` can be used if the result of a method is assigned to the variable. Using the `Func`, `Action`, `Transform` and `Lambda` set of methods, the above can be expressed more simply as:
```csharp
var times = Lambda<double>((p1, p2) => p1 * p2);
```
For functions, the `Lambda` methods can be used when the parameters and return type all have the same value, as above. This means the type parameter need only be specified once. `Transform` can be used when all the parameters are of one type and the return value, another. The `Func` methods are used when the parameters and/or return type are of different values. For actions, `Lambda` can also be used when only one type is involved and the `Action` methods do a similar job to the `Func` methods. 

This is explained in detail in the [Succinc\<T\> typed lambdas guide](TypedLambdas).

#### `Action`/`Func` conversions ####
The `ToUnitFunc` methods supplied by Succinc\<T\> can be used to cast an `Action` lambdas or method (from 0 to 4 parameters) to a `Func` delegate that returns [`unit`](Unit), allowing `void` methods to be treated as functions and thus eg used in ternary oprators. In addition, Succinc\<T\> provides an `Ignore` method that can be used to explicitly ignore the result of any expression, effectively turning that expression into a `void`

These methods are explained in detail in the [Succinc\<T\> `Action`/`Func` conversions guide](ActionFuncConversions).
