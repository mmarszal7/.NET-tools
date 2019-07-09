using NUnit.Framework;
using System.Threading;

namespace Tests
{
    public class UnitTest
    {
        [Test]
        public void PerformanceTest()
        {
            TestTime.Start(3, () => Thread.Sleep(1000));
        }

        [Test]
        public void ExpressionTest()
        {
            3.ShouldBeGreaterThan(1);
        }
    }
}