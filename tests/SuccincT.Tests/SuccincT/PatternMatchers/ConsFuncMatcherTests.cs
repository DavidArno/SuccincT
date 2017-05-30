using System.Collections.Generic;
using NUnit.Framework;
using SuccincT.PatternMatchers;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.PatternMatchers
{
    [TestFixture]
    public class ConsFuncMatcherTests
    {
        [Test]
        public void EmptyList_CanBeMatchedWithEmpty()
        {
            var list = new List<int>();
            var result = list.Match().To<int>()
                             .Empty().Do(0)
                             .Single().Do(1)
                             .Result();
            AreEqual(0, result);
        }

        [Test]
        public void SingleItemList_CanBeMatchedWithSingle()
        {
            var list = new List<int> { 1 };
            var result = list.Match().To<bool>()
                             .Empty().Do(false)
                             .Single().Do(true)
                             .Result();
            IsTrue(result);
        }

        [Test]
        public void SingleItemList_CanBeMatchedWithWhere()
        {
            var list = new List<int> { 1 };
            var result = list.Match().To<bool>()
                             .Single().Where(x => x == 1).Do(true)
                             .Single().Do(false)
                             .Result();
            IsTrue(result);
        }

        [Test]
        public void SingleItemList_CanBeMatchedWhenWhereDoesntMatch()
        {
            var list = new List<int> { 0 };
            var result = list.Match().To<bool>()
                             .Single().Where(x => x == 1).Do(true)
                             .Single().Do(false)
                             .Result();
            IsFalse(result);
        }
    }
}
