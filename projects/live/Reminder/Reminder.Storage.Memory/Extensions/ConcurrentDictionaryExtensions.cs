using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.Storage.Memory.Extensions
{
    static class ConcurrentDictionaryExtensions
    {
        public static ConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var dictionary = source?.ToDictionary(keySelector);
            return new ConcurrentDictionary<TKey, TSource>(dictionary);
        }
    }
}
