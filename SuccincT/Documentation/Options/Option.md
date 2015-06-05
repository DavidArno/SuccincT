# Option

**SuccincT.Options.Option**

static class that provides two helper methods for creating instances of the [`Option<T>`](Option_T_.md) sealed class.

**Option.None&lt;T&gt;()**
Returns an instance of `Option<T>`, with no value (or, if you prefer, it's value is `None`).

**Option.Some&lt;T&gt;(T value)**
Returns an instance of `Option<T>`, with the value of `value`.