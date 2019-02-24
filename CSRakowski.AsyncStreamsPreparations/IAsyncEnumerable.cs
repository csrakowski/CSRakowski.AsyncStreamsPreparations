#if !HAS_ASYNCENUMERABLE

using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    // Source: https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Collections/Generic/IAsyncEnumerable.cs

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    // See the LICENSE file in the project root for more information.

    /// <summary>
    /// Exposes an enumerator that provides asynchronous iteration over values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of values to enumerate.</typeparam>
    public interface IAsyncEnumerable<out T>
    {
        /// <summary>
        /// Returns an enumerator that iterates asynchronously through the collection.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that may be used to cancel the asynchronous iteration.</param>
        /// <returns>An enumerator that can be used to iterate asynchronously through the collection.</returns>
        IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    }

    // Source: https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Collections/Generic/IAsyncEnumerator.cs

    /// <summary>
    /// Supports a simple asynchronous iteration over a generic collection.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    public interface IAsyncEnumerator<out T> : IAsyncDisposable
    {
        /// <summary>
        /// Advances the enumerator asynchronously to the next element of the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="ValueTask{Boolean}"/> that will complete with a result of <c>true</c> if the enumerator
        /// was successfully advanced to the next element, or <c>false</c> if the enumerator has passed the end
        /// of the collection.
        /// </returns>
        ValueTask<bool> MoveNextAsync();

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        T Current { get; }
    }
}

#else
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(System.Collections.Generic.IAsyncEnumerable<>))]
[assembly: TypeForwardedTo(typeof(System.Collections.Generic.IAsyncEnumerator<>))]

#endif