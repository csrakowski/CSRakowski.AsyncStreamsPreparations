#if !HAS_ASYNCENUMERABLE

using System.Threading.Tasks;

namespace System
{
    public interface IAsyncDisposable
    {
        ValueTask DisposeAsync();
    }
}

#else
using System.Runtime.CompilerServices;

[assembly: TypeForwardedTo(typeof(System.IAsyncDisposable))]

#endif