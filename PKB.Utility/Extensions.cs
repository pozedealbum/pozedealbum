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

        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].Equals(item))
                    return i;

            return -1;
        }

        public static IEnumerable<T> Reverse<T>(this IReadOnlyList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }

        public static IEnumerable<T> Reverse<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }
    }
}
