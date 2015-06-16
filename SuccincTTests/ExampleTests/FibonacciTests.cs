using NUnit.Framework;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class FibonacciTests
    {
        [Test]
        public void Zero_ResultsInZero()
        {
            Assert.AreEqual(0, Fibonacci.Fib(0));
        }

        [Test]
        public void One_ResultsInOne()
        {
            Assert.AreEqual(1, Fibonacci.Fib(1));
        }

        [Test]
        public void Two_ResultsInOne()
        {
            Assert.AreEqual(1, Fibonacci.Fib(2));
        }

        [Test]
        public void Ten_ResultsIn55()
        {
            Assert.AreEqual(55, Fibonacci.Fib(10));
        }
    }
}