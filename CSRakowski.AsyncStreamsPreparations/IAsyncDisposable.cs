using System.Runtime.InteropServices;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
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

namespace System.Runtime.CompilerServices
{
    // Source: https://github.com/dotnet/coreclr/blob/master/src/System.Private.CoreLib/shared/System/Runtime/CompilerServices/ConfiguredAsyncDisposable.cs

    /// <summary>Provides a type that can be used to configure how awaits on an <see cref="IAsyncDisposable"/> are performed.</summary>
    [StructLayout(LayoutKind.Auto)]
    public readonly struct ConfiguredAsyncDisposable
    {
        private readonly IAsyncDisposable _source;
        private readonly bool _continueOnCapturedContext;

        internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
        {
            _source = source;
            _continueOnCapturedContext = continueOnCapturedContext;
        }

        public ConfiguredValueTaskAwaitable DisposeAsync() =>
            // as with other "configured" awaitable-related type in CompilerServices, we don't null check to defend against
            // misuse like `default(ConfiguredAsyncDisposable).DisposeAsync()`, which will null ref by design.
            _source.DisposeAsync().ConfigureAwait(_continueOnCapturedContext);
    }
}
