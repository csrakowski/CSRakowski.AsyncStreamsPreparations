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
    /// Sample implementation of the <see cref="IAsyncEnumerable{T}"/> interface, simply wrapping an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of values to enumerate.</typeparam>
    internal class AsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        internal AsyncEnumerable(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        /// <summary>
        /// Returns an enumerator that iterates asynchronously through the collection.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous iteration.</param>
        /// <returns>An enumerator that can be used to iterate asynchronously through the collection.</returns>
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new AsyncEnumerator<T>(_enumerable.GetEnumerator());
    }

    /// <summary>
    /// Sample implementation of the <see cref="IAsyncEnumerator{T}"/> interface, allowing elements to be retrieved asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    internal struct AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        internal AsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        /// <summary>
        /// Advances the enumerator asynchronously to the next element of the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="ValueTask{Boolean}"/> that will complete with a result of <c>true</c> if the enumerator
        /// was successfully advanced to the next element, or <c>false</c> if the enumerator has passed the end
        /// of the collection.
        /// </returns>
        public ValueTask<bool> MoveNextAsync() =>
            new ValueTask<bool>(_enumerator.MoveNext());

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current => _enumerator.Current;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources asynchronously.
        /// </summary>
        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();

#if NETSTANDARD1_1
            return new ValueTask(Task.FromResult(true));
#elif NET5_0_OR_GREATER
            return ValueTask.CompletedTask;
#else
            return new ValueTask(Task.CompletedTask);
#endif
        }
    }
}