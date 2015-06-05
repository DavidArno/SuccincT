# ValueOrError

**SuccincT.Options.ValueOrError**

## Creation of `ValueOrError` instances
Instances of `ValueOrError` cannot be created directly using `new`, as both possible values are of type `string`. Instead use the two static
methods `CreateWithValue()` and `CreateWithError()`.

## Using an instance of `ValueOrError`
An instance of `ValueOrError` can be using in one of two ways: using a functional style and an imperitive style.

### Using options functionally
One approach to using `ValueOrError` is to invoke different functionality depending on whether it holds a value or error. Two methods are
supplied for this purpose. One that takes two `Action` instances and so doesn't return a value. The other takes two `Func<T>` instances
and returns an instance of `T`.

**public void MatchAndAction(Action&lt;T&gt; valueAction, Action noneAction)**
Invokes `valueAction`, passing in the value it holds, if the option has a value. If it holds an error, then `errorAction` is invoked
instead.

**public TResult MatchAndResult&lt;TResult&gt;(Func&lt;T, TResult&gt; valueFunction, Func&lt;TResult&gt; noneFunction)**
Invokes `valueFunction`, passing in the value it holds, if the option has a value. If it holds an error, then `errorFunction` is invoked
instead. In bopth cases, the `TResult` value returned by the called function is then returned by `MatchAndResult`.

### Using options imperatively
As an alternative to using `ValueOrError` in a functional style, its value or error can be directly accessed by more traditional, imperitive style C#
code. Two read-only properties are provided for this purpose:

**public bool HasValue { get; }**
True if the option has a value; false if its an error.

**public T Value { get; }**
If `HasValue` is true, this will return the value held. Otherwise it will return the error held.