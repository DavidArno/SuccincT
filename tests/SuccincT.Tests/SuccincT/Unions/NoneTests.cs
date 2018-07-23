using NUnit.Framework;
using static NUnit.Framework.Assert;
using static SuccincT.Unions.None;

namespace SuccincTTests.SuccincT.Unions
{
    [TestFixture]
    public class NoneTests
    {
        [Test]
        public void NoneToString_GivesMeaningfulResult()
        {
            var value = none;
            AreEqual("!none!", value.ToString());
        }
    }
}
