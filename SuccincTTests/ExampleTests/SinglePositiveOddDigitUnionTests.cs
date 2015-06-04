using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincT.Unions;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class SinglePositiveOddDigitUnionTests
    {
        [Test]
        public void SinglePositiveOddDigitAndTruePrinterPrints1357And9Correctly()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(1));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(3));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(5));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(7));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(9));

                Assert.AreEqual(ExpectedBuilder(new[] { "1", "3", "5", "7", "9" }),
                                sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandlesEvenNumbers()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(2));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(4));
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(6));

                Assert.AreEqual(ExpectedBuilder(new[] { "2 isn't odd", "4 isn't odd", "6 isn't odd" }),
                                sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandles0()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(0));
                Assert.AreEqual(ExpectedBuilder(new[] { "0 isn't positive or negative" }),
                                sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandles10()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(10));
                Assert.AreEqual(ExpectedBuilder(new[] { "10 isn't 1 digit" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandlesNegative()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(-20));
                Assert.AreEqual(ExpectedBuilder(new[] { "-20 isn't positive" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandlesTrue()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(true));
                Assert.AreEqual(ExpectedBuilder(new[] { "Found true" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTruePrinterHandlesFalse()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                UnionMatcherExamples.SinglePositiveOddDigitAndTruePrinter(new Union<int, bool>(false));
                Assert.AreEqual(ExpectedBuilder(new[] { "False isn't true or single odd digit." }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterPrints1357And9Correctly()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(1)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(3)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(5)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(7)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(9));

            Assert.AreEqual("13579", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandlesEvenNumbers()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(2)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(4)) +
                         UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(6));

            Assert.AreEqual("2 isn't odd4 isn't odd6 isn't odd", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandles0()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(0));
            Assert.AreEqual("0 isn't positive or negative", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandles10()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(10));
            Assert.AreEqual("10 isn't 1 digit", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandlesNegative()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(-20));
            Assert.AreEqual("-20 isn't positive", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandlesTrue()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(true));
            Assert.AreEqual("Found true", result);
        }

        [Test]
        public void SinglePositiveOddDigitAndTrueReporterHandlesFalse()
        {
            var result = UnionMatcherExamples.SinglePositiveOddDigitAndTrueReporter(new Union<int, bool>(false));
            Assert.AreEqual("False isn't true or single odd digit.", result);
        }

        private static string ExpectedBuilder(IEnumerable<string> parts)
        {
            return parts.Aggregate("", (current, part) => current + string.Format("{0}{1}", part, Environment.NewLine));
        }
    }
}