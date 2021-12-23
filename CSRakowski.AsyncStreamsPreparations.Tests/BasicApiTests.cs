using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSRakowski.AsyncStreamsPreparations.Tests
{
    [TestFixture]
    public class BasicApiTests
    {
        [Test]
        public async Task SimpleForeachLogic()
        {
            const long expectedSum = 5050;

            IAsyncEnumerable<int> enumerable = Enumerable.Range(1, 100).AsAsyncEnumerable();
            IAsyncEnumerator<int> enumerator = enumerable.GetAsyncEnumerator();

            long summedTotal = 0;
            try
            {
                while (await enumerator.MoveNextAsync().ConfigureAwait(false))
                {
                    int current = enumerator.Current;
                    summedTotal += current;
                }
            }
            finally
            {
                await enumerator.DisposeAsync().ConfigureAwait(false);
            }

            Assert.That(expectedSum == summedTotal, "Summed total should be {0}, but was found to be {1}", expectedSum, summedTotal);
        }

        [Test]
        public async Task AsyncForeachLogic()
        {
            const long expectedSum = 5050;

            IAsyncEnumerable<int> enumerable = Enumerable.Range(1, 100).AsAsyncEnumerable();

            long summedTotal = 0;

            await foreach (var current in enumerable)
            {
                summedTotal += current;
            }

            Assert.That(expectedSum == summedTotal, "Summed total should be {0}, but was found to be {1}", expectedSum, summedTotal);
        }
    }
}
