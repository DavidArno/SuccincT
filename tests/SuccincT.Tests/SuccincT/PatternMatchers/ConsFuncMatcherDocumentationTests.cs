using NUnit.Framework;
using SuccincT.PatternMatchers;
using System.Collections.Generic;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class ConsFuncMatcherDocumentationTests
    {
        [Test]
        public void ListContentsCanBeSummedUsingMatch()
        {
            var list = new [] {1, 2, 3, 4, 5};

            static int SumListContents(IEnumerable<int> collection) =>
                collection.Match().To<int>()
                            .Single().Do(x => x)
                            .Cons().Do((head, tail) => head + SumListContents(tail))
                            .Result();

            var result = SumListContents(list);
            AreEqual(15, result);
        }
    }
}
