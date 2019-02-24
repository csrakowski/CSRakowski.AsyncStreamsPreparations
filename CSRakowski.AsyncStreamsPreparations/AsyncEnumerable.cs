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
        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            return new AsyncEnumerable<T>(enumerable);
        }
    }

    /// <summary>
    /// Sample implementation of the <see cref="IAsyncEnumerable{T}"/> interface, simply wrapping an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">The element type</typeparam>
    public class AsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        internal AsyncEnumerable(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        /// <summary>
        /// Gets an asynchronous enumerator over the sequence.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token used to cancel the enumeration.</param>
        /// <returns>Enumerator for asynchronous enumeration over the sequence.</returns>
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new AsyncEnumerator<T>(_enumerable.GetEnumerator());
    }

    /// <summary>
    /// Sample implementation of the <see cref="IAsyncEnumerator{T}"/> interface, allowing elements to be retrieved asynchronously.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public struct AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        internal AsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        /// <summary>
        /// Advances the enumerator to the next element in the sequence, returning the result asynchronously.
        /// </summary>
        /// <returns>
        /// Task containing the result of the operation: true if the enumerator was successfully advanced
        /// to the next element; false if the enumerator has passed the end of the sequence.
        /// </returns>
        public ValueTask<bool> MoveNextAsync() =>
            new ValueTask<bool>(Task.FromResult(_enumerator.MoveNext()));

        /// <summary>
        /// Gets the current element in the iteration.
        /// </summary>
        public T Current => _enumerator.Current;

        /// <summary>
        /// Implementation of the <see cref="IAsyncDisposable"/> interface method
        /// </summary>
        /// <returns>
        /// Task containing the result of the operation
        /// </returns>
        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
#if NETSTANDARD1_0
            return new ValueTask(Task.FromResult(true));
#else
            return new ValueTask(Task.CompletedTask);
#endif
        }
    }
}