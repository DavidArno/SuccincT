## Succinc\<T\> ##
#### Discriminated unions, pattern matching and partial applications for C#  ####
[![Build Status](https://ci.appveyor.com/api/projects/status/github/DavidArno/Succinct?svg=true)](https://ci.appveyor.com/project/DavidArno/succinct) &nbsp;[![NuGet](https://img.shields.io/nuget/v/SuccincT.svg)](http://www.nuget.org/packages/SuccincT)
----------
### Introduction ###
Succinc\<T\> is a small, but growing, .NET library that adds a number of functional features to C#:

* Discriminated unions, 
* Pattern matching,
* Partial applications,
* "Implicitly" typed lambdas,
* The ability to treat `void` methods as `Unit` functions,
* Replacements for `TryParse` methods that return an `Option<T>` (or `Maybe<T>`, if you prefer), rather than using the `out` parameter anti-pattern.
* "cons" support for `IEnumerable<T>` (add elements to the head of an enumeration, or split an enumeration into its head element and an enumeration containing the remaining items, all without repeatedly enumerating that enumerable).
* `Cycle()` methods that endlessly repeat an enumeration or array, again without repeatedly enumerating that enumerable).
* Replacements for `IEnumerable<T>`'s `XXXOrDefault` methods that return an `Option<T>` (or `Maybe<T>`, if you prefer), avoiding `null` and the "did it return a value, or the default?" problem,
* And finally, as an experimental feature at this stage, forward pipe support.

### Current Release ###
The current release of Succinc\<T\> is 2.1.0, which is available as a [nuget package that supports .NET 4+, Windows 8+ appstore apps, Windows Phone 8.1 apps and .NET Core](https://www.nuget.org/packages/SuccincT/). 

This release includes:
* "cons" support for `IEnumerable<T>`. This allows items to be added to the head of an enumeration and for enumerations to be split into the head item and the "tail" enumeration.
* `Cycle()` methods that can endlessly repeat an enumeration without repeatedly enumerating it.
* A new experimental feature of forward piping parameters into method calls.
* Support for .NET Core.

### Features ###
#### Discriminated Unions ####
Succinc\<T\> provides a set of union types ([`Union<T1, T2>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2), [`Union<T1, T2, T3>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2T3) and [`Union<T1, T2, T3, T4>`](https://github.com/DavidArno/SuccincT/wiki/UnionT1T2T3T4))  where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of [`Option<T>`](https://github.com/DavidArno/SuccincT/wiki/Option_T_) and [`Maybe<T>`](https://github.com/DavidArno/SuccincT/wiki/Maybe_T_) that can have the value `Some<T>` or [`None`](https://github.com/DavidArno/SuccincT/wiki/None).

Succinc\<T\> uses [`Option<T>`](https://github.com/DavidArno/SuccincT/wiki/Option_T_) to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and [`None`](https://github.com/DavidArno/SuccincT/wiki/None) when the string is not a valid value for that type. No more `out` parameters! See the [Option Parsers guide](https://github.com/DavidArno/SuccincT/wiki/OptionParsers) for more details.

Further Succinc\<T\> uses [`Option<T>`](https://github.com/DavidArno/SuccincT/wiki/Option_T_) to [provide replacements for the XxxxOrDefault LINQ extension methods on `IEnumerable<T>`](https://github.com/DavidArno/SuccincT/wiki/IEnumerableExtensions). In all cases, these new extension methods, eg `TryFirst<T>()` return an option with a value if a match occurred, or `None` if not.

#### Pattern Matching ####
Succinc\<T\> can pattern match values, tuples, unions etc in a way similar to F#'s pattern matching features. It uses a fluent (method chaining) syntax to achieve this. Some examples of its abilities:

```csharp
public static void PrintColorName(Color color) =>
    color.Match()
         .With(Color.Red).Do(x => Console.WriteLine("Red"))
         .With(Color.Green).Do(x => Console.WriteLine("Green"))
         .With(Color.Blue).Do(x => Console.WriteLine("Blue"))
         .Exec();

public static string SinglePositiveOddDigitReporter(Option<int> data) =>
    data.Match<string>()
        .Some().Of(0).Do(x => "0 isn't positive or negative")
        .Some().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
        .Some().Where(x => x > 9).Do(x => string.Format("{0} isn't 1 digit", x))
        .Some().Where(x => x < 0).Do(i => string.Format("{0} isn't positive", i))
        .Some().Do(x => string.Format("{0} isn't odd", x))
        .None().Do(() => string.Format("There was no value"))
        .Result();
```

See the [Succinc\<T\> pattern matching guide](https://github.com/DavidArno/SuccincT/wiki/PatternMatching) for more details.

#### Partial Applications ####
Succinc\<T\> supports partial function applications. A parameter can be supplied to a multi-parameter method and a new function will be returned that takes the remaining parameters. For example:

```csharp
var times = Lambda<double>((p1, p2) => p1 * p2);
var times8 = times.Apply(8);
var result = times8(9); // <- result == 72
```

See the [Succinc\<T\> partial applications guide](https://github.com/DavidArno/SuccincT/wiki/PartialFunctionApplications) for more details.

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

This is explained in detail in the [Succinc\<T\> typed lambdas guide](https://github.com/DavidArno/SuccincT/wiki/TypedLambdas).

#### `Action`/`Func` conversions ####
The `ToUnitFunc` methods supplied by Succinc\<T\> can be used to cast an `Action` lambda or method (from 0 to 4 parameters) to a `Func` delegate that returns [`unit`](Unit), allowing `void` methods to be treated as functions and thus eg used in ternary oprators. In addition, Succinc\<T\> provides an `Ignore` method that can be used to explicitly ignore the result of any expression, effectively turning that expression into a `void` statement.

These methods are explained in detail in the [Succinc\<T\> `Action`/`Func` conversions guide](https://github.com/DavidArno/SuccincT/wiki/ActionFuncConversions).

#### `IEnumerable<T>` cons ####
In many functional languages, collections of data are (can be) treated as linked lists and they have the built in pattern of treating that list as the head (first element) and tail (the rest of the list). This pattern is referred to as "cons". It allows recursive composition and decomposition of the list via a simple syntax, such as:

````
let newList = newItem :: existingList
let head :: tail = newList
````
The first line adds `newItem` to the list, `exstingItem`, to create a new list, which is assigned to `newList`.

The second, splits `newList` into the value held in the first element, and assigns it to `head`. In addition, the remainder of `newList` is assigned to `tail`. So `newItem` and `head` have the same value and `tail` and `existingList` contain the same elements.

Succinc\<T\> introduces the same idea to C#, but for all types that implement `IEnumerable<T>`. Following the immutable-by-default conventions of functional languages, items can be added and removed from the head of an enumeration without affecting the original collection. Also, in keeping with the basic tenet of `IEnumerable<T>` (that it should only be enumerated once), this is also done in a way that avoids re-enumerating the collection. This is achieved by caching the collection as it is enumerated.

So for an `IEnumerable<T>`, `enumeration`, the above lines with Succinc\<T\> would be:

````csharp
var newList = enumeration.Cons(newItem);
var consResult = newList.Cons();
var head = consResult.Head;
var tail = consResult.Tail;
````

For more details, see the [Cons help page](https://github.com/DavidArno/SuccincT/wiki/Cons).

#### `Cycle()` methods ####
Sometimes, with declarative programming, it's useful to be able to treat a set of data as an endlessly repeating set. Haskell's solution to this is the `cycle` functions, which endlessly repeat a list. Succinc\<T\> provides `Cycle` methods too, as either an extension method for `IEnumerable<T>`, or as a method that takes a list of values as it's parameters.

An example of their use is in solving the "fizzbuzz" problem, as shown below:

````csharp
var fizzes = Cycle("", "", "Fizz");
var buzzes = Cycle("", "", "", "", "Buzz");
var words = fizzes.Zip(buzzes, (f, b) => f + b);
var numbers = Range(1, 100);
var fizzBuzz = numbers.Zip(words, (n, w) => w == "" ? n.ToString() : w);
````
In the above code, `fizzBuzz` is a 100 element enumeration of the form "1", "2", "Fizz", "4", "Buzz", etc. All achieved without using a single `if` or loop.

For more details, see the [Cycle help page](https://github.com/DavidArno/SuccincT/wiki/Cycle).

#### Pipe operators ####
Another feature in many functional languages is the concept of the pipe operator. The idea behind it is that, rather than expressing a series of functions - where each returns a value which is passed to the next function - as:

````
let result = f4(f3(f2(f1(value))))
````

The expression can instead be written in an easier to read form:
````
let result = value |> f1 |> f2 |> f3 |> f4
````
Succinc\<T\> offers similar functionality. Custom operators aren't supported in C#, so the syntax isn't as neat as the above, but by using an extension method, something close can be achieved:

````csharp
var result = value.Into(f1).Into(f2).Into(f3).Into(f4);
````

For more details, see the [Forward Pipe Operator page](https://github.com/DavidArno/SuccincT/wiki/PipeOperators).
