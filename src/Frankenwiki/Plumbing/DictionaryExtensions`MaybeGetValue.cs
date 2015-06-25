using System.Collections.Generic;

namespace Frankenwiki.Plumbing
{
    internal static partial class DictionaryExtensions
    {
        public static Maybe<TValue> MaybeGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            TValue value;

            return dictionary.TryGetValue(key, out value)
                ? value.ToMaybe()
                : Maybe<TValue>.Nothing;
        }
    }
}