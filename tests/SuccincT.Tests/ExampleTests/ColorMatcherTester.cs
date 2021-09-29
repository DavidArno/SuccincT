using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static System.Console;
using static NUnit.Framework.Assert;
using static SuccincTTests.Examples.ColorMatcher;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class ColorMatcherTester
    {
        [Test]
        public void ColorMatcherGeneratesRedGreenBlueCorrectly()
        {
            using var sw = new StringWriter();
            SetOut(sw);

            PrintColorName(Color.Red);
            PrintColorName(Color.Green);
            PrintColorName(Color.Blue);

            AreEqual(ExpectedBuilder(new[] { "Red", "Green", "Blue" }), sw.ToString());
        }

        [Test]
        public void ColorMatcherGeneratesGreenBlueRedCorrectly()
        {
            using var sw = new StringWriter();
            SetOut(sw);

            PrintColorName(Color.Green);
            PrintColorName(Color.Blue);
            PrintColorName(Color.Red);

            AreEqual(ExpectedBuilder(new[] { "Green", "Blue", "Red" }), sw.ToString());
        }

        private static string ExpectedBuilder(IEnumerable<string> parts) => 
            parts.Aggregate("", (current, part) => current + $"{part}{Environment.NewLine}");
    }
}