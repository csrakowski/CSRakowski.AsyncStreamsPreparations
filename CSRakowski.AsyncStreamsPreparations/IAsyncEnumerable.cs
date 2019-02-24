#if !HAS_ASYNCENUMERABLE

using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    /// <summary>
    /// Asynchronous version of the <see cref="IEnumerable{T}"/> interface, allowing elements of the enumerable sequence to be retrieved asynchronously.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public interface IAsyncEnumerable<out T>
    {
        /// <summary>
        /// Gets an asynchronous enumerator over the sequence.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token used to cancel the enumeration.</param>
        /// <returns>Enumerator for asynchronous enumeration over the sequence.</returns>
        IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Asynchronous version of the <see cref="IEnumerator{T}"/> interface, allowing elements to be retrieved asynchronously.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public interface IAsyncEnumerator<out T> : IAsyncDisposable
    {
        /// <summary>
        /// Gets the current element in the iteration.
        /// </summary>
        T Current { get; }

        /// <summary>
        /// Advances the enumerator to the next element in the sequence, returning the result asynchronously.
        /// </summary>
        /// <returns>
        /// Task containing the result of the operation: true if the enumerator was successfully advanced
        /// to the next element; false if the enumerator has passed the end of the sequence.
        /// </returns>
        ValueTask<bool> MoveNextAsync();
    }
}

#else
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(System.Collections.Generic.IAsyncEnumerable<>))]
[assembly: TypeForwardedTo(typeof(System.Collections.Generic.IAsyncEnumerator<>))]

#endif