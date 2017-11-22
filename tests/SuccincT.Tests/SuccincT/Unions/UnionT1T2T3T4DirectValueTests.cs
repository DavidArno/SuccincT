using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3T4DirectValueTests
    {
        private enum Plants { Rose }
        private enum Foods { Cake }

        [Test]
        public void UnionWithT1_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(1);
            var result = union.Value<int>();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("string");
            var result = union.Value<string>();
            AreEqual("string", result);
        }

        [Test]
        public void UnionWithT3_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            var result = union.Value<Plants>();
            AreEqual(Plants.Rose, result);
        }

        [Test]
        public void UnionWithT4_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var result = union.Value<Foods>();
            AreEqual(Foods.Cake, result);
        }

        [Test]
        public void UnionT1T2T3WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Value<float>());
        }
    }
}