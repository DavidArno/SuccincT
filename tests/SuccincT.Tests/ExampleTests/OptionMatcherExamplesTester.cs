using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincT.Options;
using static System.Console;
using static NUnit.Framework.Assert;
using static SuccincTTests.Examples.OptionMatcherExamples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class OptionMatcherExamplesTester
    {
        [Test]
        public void PrintOptionPrintsValues()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);

                PrintOption(Option<int>.Some(-1));
                PrintOption(Option<int>.Some(0));
                PrintOption(Option<int>.Some(1));
                PrintOption(Option<int>.Some(5));
                PrintOption(Option<int>.Some(8));

                AreEqual(ExpectedBuilder(new[] { "-1", "0", "1", "5", "8" }), sw.ToString());
            }
        }

        [Test]
        public void PrintOptionPrintsNothingForNone()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                PrintOption(Option<int>.None());
                AreEqual("", sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherPrints1To3Correctly()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);

                OptionMatcher(Option<int>.Some(1));
                OptionMatcher(Option<int>.Some(2));
                OptionMatcher(Option<int>.Some(3));

                AreEqual(ExpectedBuilder(new[] { "1", "2", "3" }), sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherCorrectlyIdentifiesNumbersOutside1To3()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);

                OptionMatcher(Option<int>.Some(0));
                OptionMatcher(Option<int>.Some(4));

                AreEqual(ExpectedBuilder(new[] { "0 isn't 1, 2 or 3!", "4 isn't 1, 2 or 3!" }),
                         sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherPrintsNothingForNone()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                OptionMatcher(Option<int>.None());
                AreEqual("", sw.ToString());
            }
        }

        [Test]
        public void NumberToNameMapperReturnsCorrectNames()
        {
            var result = NumberToNameMapper(Option<int>.Some(1)) +
                         NumberToNameMapper(Option<int>.Some(2)) +
                         NumberToNameMapper(Option<int>.Some(3)) +
                         NumberToNameMapper(Option<int>.Some(4)) +
                         NumberToNameMapper(Option<int>.Some(5)) +
                         NumberToNameMapper(Option<int>.Some(6)) +
                         NumberToNameMapper(Option<int>.Some(7)) +
                         NumberToNameMapper(Option<int>.Some(8)) +
                         NumberToNameMapper(Option<int>.Some(9));

            AreEqual("OneTwoThreeFourFiveSixSevenEightNine", result);
        }

        [Test]
        public void NumberToNameMapperReturnsNumbersForValuesOutside1To4()
        {
            var result = NumberToNameMapper(Option<int>.Some(-1)) +
                         NumberToNameMapper(Option<int>.Some(0)) +
                         NumberToNameMapper(Option<int>.Some(10));

            AreEqual("-1010", result);
        }

        [Test]
        public void NumberToNameMapperReturnsNoneForNone()
        {
            var result = NumberToNameMapper(Option<int>.None());
            AreEqual("None", result);
        }

        private static string ExpectedBuilder(IEnumerable<string> parts) =>
            parts.Aggregate("", (current, part) => current + $"{part}{Environment.NewLine}");
    }
}