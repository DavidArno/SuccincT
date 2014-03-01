# Option&lt;T&gt;

**SuccincT.Options.Option&lt;T&gt;**

## Creation of `Option<T>` instances
Instances of `Option<T>` cannot be created directly. Instead use the two helper methods in the [`Option`](Option.md) static class: `Option.None<T>()` 
and `Option.Some<T>(T value)`.

## Using an instance of `Option<T>`
An instance of `Option<T>` (option) can be using in one of two ways: using a functional style and an imperitive style.

### Using options functionally
One approach to using an option is to invoke different functionality depending on whether the option has a value or not. Two methods are 
supplied for this purpose. One that takes two `Action` instances and so doesn't return a value. The other takes two `Func<T>` instances
and returns an instance of `T`.

**public void MatchAndAction(Action&lt;T&gt; valueAction, Action noneAction)**  
Invokes `valueAction`, passing in the value it holds, if the option has a value. If there is no value, then `noneAction` is invoked
instead.

**public TResult MatchAndResult&lt;TResult&gt;(Func&lt;T, TResult&gt; valueFunction, Func&lt;TResult&gt; noneFunction)**  
Invokes `valueFunction`, passing in the value it holds, if the option has a value. If there is no value, then `noneFunction` is invoked
instead. In bopth cases, the `TResult` value returned by the called function is then returned by `MatchAndResult`. 

### Using options imperatively
As an alternative to using an option in a functional style, its value can be directly accessed by more traditional, imperitive style C#
code. Two read-only properties are provided for this purpose:

**public bool HasValue { get; }**  
True if the option has a value; else false.

**public T Value { get; }**
If `HasValue` is true, this will return the value held by the option. Otherwise an `InvalidOperationException` will be thrown.


