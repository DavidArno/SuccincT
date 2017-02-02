using System.Collections.Generic;

namespace SuccincT.Options
{
    public static class OptionExtensionsForDictionaryType
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? Option<TValue>.Some(value) : Option<TValue>.None();
        }
    }
}
