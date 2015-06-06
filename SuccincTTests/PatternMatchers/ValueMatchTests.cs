using NUnit.Framework;
using SuccincT.PatternMatchers;

namespace SuccincTTests.PatternMatchers
{
    [TestFixture]
    public class ValueMatchTests
    {
        [Test]
        public void IntValue_CanBeMatchedWithExec()
        {
            var result = false;
            1.Match().With(1).Do(x => result = true).Exec();
            Assert.IsTrue(result);
        }
    }
}