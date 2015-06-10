## Succinc<T\> \<T\> &lt;T&gt; \074T\076 ##

**Succinc<T\>** is a small .NET framework that started out as a means of providing an elegant solution to the problem of functions that need return a success state and value, or a failure state. Initially, this was `ISuccess<T>`. Then I started learning F#...

Now Succinc<T\> has grown into a library that provides discriminated unions, pattern matching and functional composition for C#.

### Discriminated Unions ###
Succinc<T\> provides a set of union types (`Union<T1,T2>` through to `Union<T1,T2,T3,T4>`) where an instance will hold exactly one value of one of the specified types. In addition, it provides the likes of `Option<T>` that can have the value `Some<T>` or `None`.

Succinc<T\> uses `Option<T>` to provide replacements for the .NET basic types' `TryParse()` methods and `Enum.Parse()`. In all cases, these are extension methods to `string` and they return `Some<T>` on a successful parse and `None` when the string is not a valid value for that type. No more `out` parameters!

### Pattern Matching ###

### Functional Composition ###

