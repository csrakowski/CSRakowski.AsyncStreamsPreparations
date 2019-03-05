using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> enumerable)=>
            new AsyncEnumerable<T>(enumerable);
    }

    internal class AsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        internal AsyncEnumerable(IEnumerable<T> enumerable) => _enumerable = enumerable;

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new AsyncEnumerator<T>(_enumerable.GetEnumerator());
    }

    internal struct AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        internal AsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public ValueTask<bool> MoveNextAsync() =>
            new ValueTask<bool>(_enumerator.MoveNext());

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();

            return new ValueTask();
        }
    }
}