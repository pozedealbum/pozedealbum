using System;
using System.Collections.Generic;

namespace PKB.Utility
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action.Invoke(item);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int index = 0;
            foreach (var item in enumerable)
                action.Invoke(item, index++);
        }
    }
}
