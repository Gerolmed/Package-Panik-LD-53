using System;
using System.Collections.Generic;

namespace LudumDare.Utils
{
    public static class Extensions
    {
        public static V GetOrCreate<K, V>(this Dictionary<K, V> dict, K key, Func<V> onMissing)
        {
            if (dict.TryGetValue(key, out var outVal)) return outVal;
            
            outVal = onMissing.Invoke();
            dict.Add(key, outVal);

            return outVal;
        }
        public static V ComputeOrCreate<K, V>(this Dictionary<K, V> dict, K key, Func<V> onMissing, Func<V, V> doCompute)
        {
            if (dict.TryGetValue(key, out var outVal))
            {
                return dict[key] = doCompute.Invoke(outVal);
            }
            
            outVal = onMissing.Invoke();
            dict.Add(key, outVal);
            return dict[key] = doCompute.Invoke(outVal);
        }

        public static T Pop<T>(this List<T> list, Predicate<T> match) {
            var item = list.FindLast(match);
            if(item != null)
                list.Remove(item);
            return item;
        }
    }
}