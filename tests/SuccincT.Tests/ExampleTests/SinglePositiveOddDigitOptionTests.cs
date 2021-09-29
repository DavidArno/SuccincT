using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincT.Options;
using SuccincTTests.Examples;
using static System.Environment;
using static NUnit.Framework.Assert;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class SinglePositiveOddDigitOptionTests
    {
        [Test]
        public void SinglePositiveOddDigitPrinterPrints1357And9Correctly()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(1));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(3));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(5));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(7));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(9));

            AreEqual(ExpectedBuilder(new[] { "1", "3", "5", "7", "9" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesEvenNumbers()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(2));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(4));
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(6));

            AreEqual(ExpectedBuilder(new[] { "2 isn't odd", "4 isn't odd", "6 isn't odd" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandles0()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(0));
            AreEqual(ExpectedBuilder(new[] { "0 isn't positive or negative" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandles10()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(10));
            AreEqual(ExpectedBuilder(new[] { "10 isn't 1 digit" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesNegative()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.Some(-20));
            AreEqual(ExpectedBuilder(new[] { "-20 isn't positive" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitPrinterHandlesNone()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);
            OptionMatcherExamples.SinglePositiveOddDigitPrinter(Option<int>.None());
            AreEqual(ExpectedBuilder(new[] { "There was no value" }), sw.ToString());
        }

        [Test]
        public void SinglePositiveOddDigitReporterPrints1357And9Correctly()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(1)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(3)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(5)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(7)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(9));

            AreEqual("13579", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesEvenNumbers()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(2)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(4)) +
                         OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(6));

            AreEqual("2 isn't odd4 isn't odd6 isn't odd", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandles0()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(0));
            AreEqual("0 isn't positive or negative", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandles10()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(10));
            AreEqual("10 isn't 1 digit", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesNegative()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.Some(-20));
            AreEqual("-20 isn't positive", result);
        }

        [Test]
        public void SinglePositiveOddDigitReporterHandlesNone()
        {
            var result = OptionMatcherExamples.SinglePositiveOddDigitReporter(Option<int>.None());
            AreEqual("There was no value", result);
        }

        private static string ExpectedBuilder(IEnumerable<string> parts) => 
            parts.Aggregate("", (current, part) => $"{current}{part}{NewLine}");
    }
}