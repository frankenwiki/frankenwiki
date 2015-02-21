using System;
using System.Collections.Generic;

namespace Frankenwiki.Plumbing
{
    internal static partial class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            IEnumerable<TValue> items,
            Func<TValue, TKey> getKey)
        {
            foreach (var item in items)
            {
                dictionary[getKey(item)] = item;
            }
        }
    }
}