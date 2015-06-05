using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class ColorMatcherTester
    {
        [Test]
        public void ColorMatcherGeneratesRedGreenBlueCorrectly()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                ColorMatcher.PrintColorName(ColorMatcher.Color.Red);
                ColorMatcher.PrintColorName(ColorMatcher.Color.Green);
                ColorMatcher.PrintColorName(ColorMatcher.Color.Blue);

                Assert.AreEqual(ExpectedBuilder(new[] { "Red", "Green", "Blue" }), sw.ToString());
            }
        }

        [Test]
        public void ColorMatcherGeneratesGreenBlueRedCorrectly()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                ColorMatcher.PrintColorName(ColorMatcher.Color.Green);
                ColorMatcher.PrintColorName(ColorMatcher.Color.Blue);
                ColorMatcher.PrintColorName(ColorMatcher.Color.Red);

                Assert.AreEqual(ExpectedBuilder(new[] { "Green", "Blue", "Red" }), sw.ToString());
            }
        }

        private string ExpectedBuilder(IEnumerable<string> parts)
        {
            return parts.Aggregate("", (current, part) => current + string.Format("{0}{1}", part, Environment.NewLine));
        }
    }
}