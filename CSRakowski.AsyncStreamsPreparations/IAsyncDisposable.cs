#if !HAS_ASYNCENUMERABLE

using System.Threading.Tasks;

namespace System
{
    // Source: https://github.com/dotnet/corefx/blob/master/src/Common/src/CoreLib/System/IAsyncDisposable.cs

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license.
    // See the LICENSE file in the project root for more information.

    /// <summary>
    /// Provides a mechanism for releasing unmanaged resources asynchronously.
    /// </summary>
    public interface IAsyncDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources asynchronously.
        /// </summary>
        ValueTask DisposeAsync();
    }
}

#else
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(System.IAsyncDisposable))]

#endif