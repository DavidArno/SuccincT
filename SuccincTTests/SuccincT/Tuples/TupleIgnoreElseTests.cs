using System;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using SuccincT.Tuples;

namespace SuccincTTests.SuccincT.Tuples
{
    public class TupleIgnoreElseTests
    {
        private class TestClassOf2 : ITupleMatchable<int, string>
        {
            public int A;
            public string B;

            public Tuple<int, string> PropertiesToMatch => Tuple.Create(A, B);
        }

        private class TestClassOf3 : ITupleMatchable<int, string, double>
        {
            public int A;
            public string B;
            public double C;

            public Tuple<int, string, double> PropertiesToMatch => Tuple.Create(A, B, C);
        }

        private class TestClassOf4 : ITupleMatchable<int, string, double, bool>
        {
            public int A;
            public string B;
            public double C;
            public bool D;

            public Tuple<int, string, double, bool> PropertiesToMatch => Tuple.Create(A, B, C, D);
        }

        [Test]
        public void TupleOfTwoWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var tuple = new TestClassOf2 { A = 1, B = "a" };
            var result = false;
            tuple.Match().With(1, "b").Do((x, y) => result = true).IgnoreElse().Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleOfThreeWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var tuple = new TestClassOf3 { A = 1, B = "a", C = 1.0 };
            var result = false;
            tuple.Match().With(1, "b", 1.0).Do((x, y, z) => result = true).IgnoreElse().Exec();
            Assert.IsFalse(result);
        }

        [Test]
        public void TupleOfFourWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var tuple = new TestClassOf4 { A = 1, B = "a", C = 1.0, D = false };
            var result = false;
            tuple.Match().With(1, "a", 1.0, true).Do((w, x, y, z) => result = true).IgnoreElse().Exec();
            Assert.IsFalse(result);
        }
    }
}