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
    public static class AsyncEnumerableEx
    {
        public static IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> enumerable)
        {
            return new AsyncEnumerable<T>(enumerable);
        }
    }

    public class AsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;

        internal AsyncEnumerable(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator() =>
            new AsyncEnumerator<T>(_enumerable.GetEnumerator());
    }

    public struct AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        internal AsyncEnumerator(IEnumerator<T> enumerator)
        {
            _enumerator = enumerator;
        }

        public Task<bool> MoveNextAsync() =>
            Task.FromResult(_enumerator.MoveNext());

        public T Current => _enumerator.Current;

        public Task DisposeAsync()
        {
            _enumerator.Dispose();
#if NETSTANDARD1_0
            return Task.FromResult(true);
#else
            return Task.CompletedTask;
#endif
        }
    }
}