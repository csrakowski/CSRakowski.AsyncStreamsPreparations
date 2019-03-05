﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace System.Threading.Tasks
{
    // Source: https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Threading/Tasks/TaskExtensions.cs

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    // See the LICENSE file in the project root for more information.

    /// <summary>Provides a set of static methods for working with specific kinds of <see cref="Task"/> instances.</summary>
    public static class TaskExtensions
    {
        /// <summary>Configures how awaits on the tasks returned from an async disposable will be performed.</summary>
        /// <param name="source">The source async disposable.</param>
        /// <param name="continueOnCapturedContext">Whether to capture and marshal back to the current context.</param>
        /// <returns>The configured async disposable.</returns>
        public static ConfiguredAsyncDisposable ConfigureAwait(this IAsyncDisposable source, bool continueOnCapturedContext) =>
            new ConfiguredAsyncDisposable(source, continueOnCapturedContext);

        /// <summary>Configures how awaits on the tasks returned from an async iteration will be performed.</summary>
        /// <typeparam name="T">The type of the objects being iterated.</typeparam>
        /// <param name="source">The source enumerable being iterated.</param>
        /// <param name="continueOnCapturedContext">Whether to capture and marshal back to the current context.</param>
        /// <returns>The configured enumerable.</returns>
        public static ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<T>(
            this IAsyncEnumerable<T> source, bool continueOnCapturedContext) =>
            new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext, default);

        /// <summary>Sets the <see cref="CancellationToken"/> to be passed to <see cref="IAsyncEnumerable{T}.GetAsyncEnumerator(CancellationToken)"/> when iterating.</summary>
        /// <typeparam name="T">The type of the objects being iterated.</typeparam>
        /// <param name="source">The source enumerable being iterated.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
        /// <returns>The configured enumerable.</returns>
        public static ConfiguredCancelableAsyncEnumerable<T> WithCancellation<T>(
            this IAsyncEnumerable<T> source, CancellationToken cancellationToken) =>
            new ConfiguredCancelableAsyncEnumerable<T>(source, true, cancellationToken);
    }
}