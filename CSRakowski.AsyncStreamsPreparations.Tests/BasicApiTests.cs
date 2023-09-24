using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRakowski.AsyncStreamsPreparations.Tests
{
    public class BasicApiTests
    {
        [Fact]
        public async Task SimpleForeachLogic()
        {
            const long expectedSum = 5050;

            IAsyncEnumerable<int> enumerable = Enumerable.Range(1, 100).AsAsyncEnumerable();
            IAsyncEnumerator<int> enumerator = enumerable.GetAsyncEnumerator();

            long summedTotal = 0;
            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    int current = enumerator.Current;
                    summedTotal += current;
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            Assert.Equal(expectedSum, summedTotal);
        }

        [Fact]
        public async Task AsyncForeachLogic()
        {
            const long expectedSum = 5050;

            IAsyncEnumerable<int> enumerable = Enumerable.Range(1, 100).AsAsyncEnumerable();

            long summedTotal = 0;

            await foreach (var current in enumerable)
            {
                summedTotal += current;
            }

            Assert.Equal(expectedSum, summedTotal);
        }
    }
}
