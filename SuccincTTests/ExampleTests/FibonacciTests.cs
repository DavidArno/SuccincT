using NUnit.Framework;
using SuccincTTests.Examples;
using static NUnit.Framework.Assert;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class FibonacciTests
    {
        [Test]
        public void Zero_ResultsInZero() => AreEqual(0, Fibonacci.Fib(0));

        [Test]
        public void One_ResultsInOne() => AreEqual(1, Fibonacci.Fib(1));

        [Test]
        public void Two_ResultsInOne() => AreEqual(1, Fibonacci.Fib(2));

        [Test]
        public void Ten_ResultsIn55() => AreEqual(55, Fibonacci.Fib(10));
    }
}