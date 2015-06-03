using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static System.Console;
using static System.Environment;
using static NUnit.Framework.Assert;
using static SuccincT.Options.Option<int>;
using static SuccincTTests.Examples.OptionMatcherExamples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class SinglePositiveOddDigitOptionTests
    {
        [Test]
        public void SinglePositiveOddDigitPrinterPrints1357And9Correctly()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(Some(1));
                SinglePositiveOddDigitPrinter(Some(3));
                SinglePositiveOddDigitPrinter(Some(5));
                SinglePositiveOddDigitPrinter(Some(7));
                SinglePositiveOddDigitPrinter(Some(9));
                AreEqual(ExpectedBuilder(new[] { "1", "3", "5", "7", "9" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesEvenNumbers()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(Some(2));
                SinglePositiveOddDigitPrinter(Some(4));
                SinglePositiveOddDigitPrinter(Some(6));
                AreEqual(ExpectedBuilder(new[] { "2 isn't odd", "4 isn't odd", "6 isn't odd" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandles0()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(Some(0));
                AreEqual(ExpectedBuilder(new[] { "0 isn't positive or negative" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandles10()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(Some(10));
                AreEqual(ExpectedBuilder(new[] { "10 isn't 1 digit" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesNegative()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(Some(-20));
                AreEqual(ExpectedBuilder(new[] { "-20 isn't positive" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesNone()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                SinglePositiveOddDigitPrinter(None());
                AreEqual(ExpectedBuilder(new[] { "There was no value" }), sw.ToString());
            }
        }

        [Test]
        public void SinglePositiveOddDigitReporterPrints1357And9Correctly()
        {
            var result = SinglePositiveOddDigitReporter(Some(1)) +
                         SinglePositiveOddDigitReporter(Some(3)) +
                         SinglePositiveOddDigitReporter(Some(5)) +
                         SinglePositiveOddDigitReporter(Some(7)) +
                         SinglePositiveOddDigitReporter(Some(9));

            AreEqual("13579", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesEvenNumbers()
        {
            var result = SinglePositiveOddDigitReporter(Some(2)) +
                         SinglePositiveOddDigitReporter(Some(4)) +
                         SinglePositiveOddDigitReporter(Some(6));

            AreEqual("2 isn't odd4 isn't odd6 isn't odd", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandles0()
        {
            var result = SinglePositiveOddDigitReporter(Some(0));
            AreEqual("0 isn't positive or negative", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandles10()
        {
            var result = SinglePositiveOddDigitReporter(Some(10));
            AreEqual("10 isn't 1 digit", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesNegative()
        {
            var result = SinglePositiveOddDigitReporter(Some(-20));
            AreEqual("-20 isn't positive", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesNone()
        {
            var result = SinglePositiveOddDigitReporter(None());
            AreEqual("There was no value", result);
        }

        private static string ExpectedBuilder(IEnumerable<string> parts)
        {
            return parts.Aggregate("", (current, part) => current + $"{part}{NewLine}");
        }
    }
}