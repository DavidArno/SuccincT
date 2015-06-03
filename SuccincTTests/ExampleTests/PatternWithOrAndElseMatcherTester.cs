using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static System.Console;
using static System.Environment;
using static NUnit.Framework.Assert;
using static SuccincTTests.Examples.PatternWithOrAndElseMatcher;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class PatternWithOrAndElseMatcherTester
    {
        [Test]
        public void Filter123CorrectlyMatches12And3()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                Filter123(1);
                Filter123(2);
                Filter123(3);
                AreEqual(ExpectedBuilder(new[] { "Found 1, 2, or 3!", "Found 1, 2, or 3!", "Found 1, 2, or 3!" }),
                         sw.ToString());
            }
        }

        [Test]
        public void Filter123CorrectlyIgnores0And4()
        {
            using (var sw = new StringWriter())
            {
                SetOut(sw);
                Filter123(0);
                Filter123(4);
                AreEqual(ExpectedBuilder(new[] { "0", "4" }), sw.ToString());
            }
        }

        private string ExpectedBuilder(IEnumerable<string> parts) =>
            parts.Aggregate("", (current, part) => $"{current}{part}{NewLine}");
    }
}