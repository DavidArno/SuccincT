# Succinc\<T\> - Bringing functional programming to the C# masses #

## Introduction ##
**Succinc\<T\>** is a small .NET framework that started out as a means of providing an elegant solution to the problem of functions that need return a success state and value, or a failure state. Initially, it consisted of a few parser methods that returned an `ISuccess<T>` result. Then I started learning F#...

Now Succinc\<T\> has grown into a library that provides discriminated unions, pattern matching and functional composition for C#, in addition to providing a set of value parsers that neatly return `Option<T>`.

## Release Plans ##
Succinc\<T\> is currently in beta. As such:

- There is currently no version number.
- There is no Nuget package yet. If you want to use this code, please clone it and build the solution in VS2013/15.
- Breaking changes are likely. The chances of this are diminishing, but please note that equality comparisons for unions have not yet been implemented and so this behaviour will change significantly when they are.
- The documentation is woefully incomplete and what's there is likely incorrect due to the number of changes made recently.

The following things will (hopefully) be happening soon:

- Implement proper `Equals` and `==` behaviour for unions and options.
- Implement `.Match<T>...Result()` for non-union data. Currently, non-union pattern matching can only invoke an `Action<T>` on match, not return the result of a `Func<TValue,TResult>`. This needs adding.
- Update/redo all the docs, examples, public class comments etc.
- A Nuget package so it can easily be incorporated into your existing projects.

## Features ##
### Discriminated Unions ###
Succinc\<T\> provides a set of union types (`Union<T1,T2>` through to `Union<T1,T2,T3,T4>`) where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of `Option<T>` that can have the value `Some<T>` or `None`.

Succinc\<T\> uses `Option<T>` to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and `None` when the string is not a valid value for that type. No more `out` parameters!

### Pattern Matching ###
Succinc\<T\> can pattern match values, unions etc in a way similar to F#'s pattern matching features. It uses a fluent syntax to achieve this. Some examples of its abilities:

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

See the [Succinc\<T\> pattern matching guide](https://github.com/DavidArno/SuccincT/wiki/PatternMatching) for more details.

### Functional Composition ###
Succinc\<T\> supports functional composition. A parameter can be supplied to a mnulti-parameter method and a new function will be returned that takes the remaining parameters. For example:

```csharp

    Func<int,int> times = (p1, p2) => p1 * p2;
    var times8 = times.Compose(8);
    var result = times8(9); // <- result == 72
```
