using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincT.Options;
using SuccincTTests.Examples;

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
                Console.SetOut(sw);

                OptionMatcherExamples.PrintOption(Option<int>.Some(-1));
                OptionMatcherExamples.PrintOption(Option<int>.Some(0));
                OptionMatcherExamples.PrintOption(Option<int>.Some(1));
                OptionMatcherExamples.PrintOption(Option<int>.Some(5));
                OptionMatcherExamples.PrintOption(Option<int>.Some(8));

                Assert.AreEqual(ExpectedBuilder(new[] { "-1", "0", "1", "5", "8" }),
                                sw.ToString());
            }
        }

        [Test]
        public void PrintOptionPrintsNothingForNone()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                OptionMatcherExamples.PrintOption(Option<int>.None());
                Assert.AreEqual("", sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherPrints1To3Correctly()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                OptionMatcherExamples.OptionMatcher(Option<int>.Some(1));
                OptionMatcherExamples.OptionMatcher(Option<int>.Some(2));
                OptionMatcherExamples.OptionMatcher(Option<int>.Some(3));

                Assert.AreEqual(ExpectedBuilder(new[] { "1", "2", "3" }),
                                sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherCorrectlyIdentifiesNumbersOutside1To3()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                OptionMatcherExamples.OptionMatcher(Option<int>.Some(0));
                OptionMatcherExamples.OptionMatcher(Option<int>.Some(4));

                Assert.AreEqual(ExpectedBuilder(new[] { "0 isn't 1, 2 or 3!", "4 isn't 1, 2 or 3!" }),
                                sw.ToString());
            }
        }

        [Test]
        public void OptionMatcherPrintsNothingForNone()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                OptionMatcherExamples.OptionMatcher(Option<int>.None());
                Assert.AreEqual("", sw.ToString());
            }
        }

        [Test]
        public void NumberNamerReturnsCorrectNames()
        {
            var result = OptionMatcherExamples.NumberNamer(Option<int>.Some(1)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(2)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(3)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(4)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(5)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(6)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(7)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(8)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(9));

            Assert.AreEqual("OneTwoThreeFourFiveSixSevenEightNine", result);
        }

        [Test]
        public void NumberNamerReturnsNumbersForValuesOutside1To4()
        {
            var result = OptionMatcherExamples.NumberNamer(Option<int>.Some(-1)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(0)) +
                         OptionMatcherExamples.NumberNamer(Option<int>.Some(10));

            Assert.AreEqual("-1010", result);
        }

        [Test]
        public void NumberNamerReturnsNoneForNone()
        {
            var result = OptionMatcherExamples.NumberNamer(Option<int>.None());
            Assert.AreEqual("None", result);
        }

        private static string ExpectedBuilder(IEnumerable<string> parts)
        {
            return parts.Aggregate("", (current, part) => current + string.Format("{0}{1}", part, Environment.NewLine));
        }
    }
}