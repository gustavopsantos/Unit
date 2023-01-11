using System.Collections.Generic;

namespace Unit.Editor.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}