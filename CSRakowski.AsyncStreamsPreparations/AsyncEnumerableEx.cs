using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace CSRakowski.AsyncStreamsPreparations
{
    /// <summary>
    /// Helper class with extension methods
    /// </summary>
    public static class AsyncEnumerableEx
    {
        /// <summary>
        /// Wraps the <paramref name="enumerable"/> in an <see cref="AsyncEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="enumerable">The collection to wrap</param>
        /// <returns>The wrapped collection</returns>
        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> enumerable) =>
            new AsyncEnumerable<T>(enumerable);
    }
}