using System;
using System.Collections.Generic;
using System.Linq;

namespace Odin.Configuration
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Skip items in a list until a condition is met.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> SkipUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.SkipWhile(t => !predicate(t));
        }

        /// <summary>
        /// Take items in a list until a condition is met.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.TakeWhile(t => !predicate(t));
        }
    }
}