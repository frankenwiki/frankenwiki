using System;
using System.Collections.Generic;
using System.Linq;

namespace Frankenwiki.Plumbing
{
    internal static partial class EnumerableExtensions
    {
        // http://stackoverflow.com/a/1300116/149259
        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> source,
            Func<T, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>();

            return source.Where(element => knownKeys.Add(keySelector(element)));
        }
    }
}