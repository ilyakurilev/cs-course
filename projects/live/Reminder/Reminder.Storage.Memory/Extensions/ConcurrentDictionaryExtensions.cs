using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.Storage.Memory.Extensions
{
    public static class ConcurrentDictionaryExtensions
    {
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            var dictionary = source.ToDictionary(keySelector);
            return new ConcurrentDictionary<TKey, TValue>(dictionary);
        }
    }
}
