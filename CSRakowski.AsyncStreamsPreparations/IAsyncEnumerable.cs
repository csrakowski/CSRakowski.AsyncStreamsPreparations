using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
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

namespace System.Runtime.CompilerServices
{
    // Source: https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Runtime/CompilerServices/ConfiguredCancelableAsyncEnumerable.cs

    /// <summary>Provides an awaitable async enumerable that enables cancelable iteration and configured awaits.</summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ConfiguredCancelableAsyncEnumerable<T>
    {
        private readonly IAsyncEnumerable<T> _enumerable;
        private readonly CancellationToken _cancellationToken;
        private readonly bool _continueOnCapturedContext;

        internal ConfiguredCancelableAsyncEnumerable(IAsyncEnumerable<T> enumerable, bool continueOnCapturedContext, CancellationToken cancellationToken)
        {
            _enumerable = enumerable;
            _continueOnCapturedContext = continueOnCapturedContext;
            _cancellationToken = cancellationToken;
        }

        /// <summary>Configures how awaits on the tasks returned from an async iteration will be performed.</summary>
        /// <param name="continueOnCapturedContext">Whether to capture and marshal back to the current context.</param>
        /// <returns>The configured enumerable.</returns>
        /// <remarks>This will replace any previous value set by <see cref="ConfigureAwait(bool)"/> for this iteration.</remarks>
        public ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext) =>
            new ConfiguredCancelableAsyncEnumerable<T>(_enumerable, continueOnCapturedContext, _cancellationToken);

        /// <summary>Sets the <see cref="CancellationToken"/> to be passed to <see cref="IAsyncEnumerable{T}.GetAsyncEnumerator(CancellationToken)"/> when iterating.</summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
        /// <returns>The configured enumerable.</returns>
        /// <remarks>This will replace any previous <see cref="CancellationToken"/> set by <see cref="WithCancellation(CancellationToken)"/> for this iteration.</remarks>
        public ConfiguredCancelableAsyncEnumerable<T> WithCancellation(CancellationToken cancellationToken) =>
            new ConfiguredCancelableAsyncEnumerable<T>(_enumerable, _continueOnCapturedContext, cancellationToken);

        public Enumerator GetAsyncEnumerator() =>
            // as with other "configured" awaitable-related type in CompilerServices, we don't null check to defend against
            // misuse like `default(ConfiguredCancelableAsyncEnumerable<T>).GetAsyncEnumerator()`, which will null ref by design.
            new Enumerator(_enumerable.GetAsyncEnumerator(_cancellationToken), _continueOnCapturedContext);

        /// <summary>Provides an awaitable async enumerator that enables cancelable iteration and configured awaits.</summary>
        [StructLayout(LayoutKind.Auto)]
        public readonly struct Enumerator
        {
            private readonly IAsyncEnumerator<T> _enumerator;
            private readonly bool _continueOnCapturedContext;

            internal Enumerator(IAsyncEnumerator<T> enumerator, bool continueOnCapturedContext)
            {
                _enumerator = enumerator;
                _continueOnCapturedContext = continueOnCapturedContext;
            }

            /// <summary>Advances the enumerator asynchronously to the next element of the collection.</summary>
            /// <returns>
            /// A <see cref="ConfiguredValueTaskAwaitable{Boolean}"/> that will complete with a result of <c>true</c>
            /// if the enumerator was successfully advanced to the next element, or <c>false</c> if the enumerator has
            /// passed the end of the collection.
            /// </returns>
            public ConfiguredValueTaskAwaitable<bool> MoveNextAsync() =>
                _enumerator.MoveNextAsync().ConfigureAwait(_continueOnCapturedContext);

            /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            public T Current => _enumerator.Current;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or
            /// resetting unmanaged resources asynchronously.
            /// </summary>
            public ConfiguredValueTaskAwaitable DisposeAsync() =>
                _enumerator.DisposeAsync().ConfigureAwait(_continueOnCapturedContext);
        }
    }
}