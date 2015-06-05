using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SuccincTTests.Examples;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public sealed class PatternWithOrAndElseMatcherTester
    {
        [Test]
        public void Filter123CorrectlyMatches12And3()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                PatternWithOrAndElseMatcher.Filter123(1);
                PatternWithOrAndElseMatcher.Filter123(2);
                PatternWithOrAndElseMatcher.Filter123(3);

                Assert.AreEqual(ExpectedBuilder(new[] { "Found 1, 2, or 3!", "Found 1, 2, or 3!", "Found 1, 2, or 3!" }),
                                sw.ToString());
            }
        }

        [Test]
        public void Filter123CorrectlyIgnores0And4()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                PatternWithOrAndElseMatcher.Filter123(0);
                PatternWithOrAndElseMatcher.Filter123(4);

                Assert.AreEqual(ExpectedBuilder(new[] { "0", "4" }),
                                sw.ToString());
            }
        }

        private string ExpectedBuilder(IEnumerable<string> parts)
        {
            return parts.Aggregate("", (current, part) => current + string.Format("{0}{1}", part, Environment.NewLine));
        }
    }
}