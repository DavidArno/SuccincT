using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2DirectValueTests
    {
        [Test]
        public void UnionWithT1_ValueMatchesCorrectly()
        {
            var union = new Union<int, string>(1);
            var result = union.Value<int>();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT1_TryGetValueMatchesCorrectly()
        {
            var union = new Union<int, string>(1);
            var result = union.TryGetValue<int>();
            AreEqual(1, result.Value);
        }

        [Test]
        public void UnionWithT2_ValueMatchesCorrectly()
        {
            var union = new Union<int, string>("string");
            var result = union.Value<string>();
            AreEqual("string", result);
        }

        [Test]
        public void UnionWithT2_TryGetValueMatchesCorrectly()
        {
            var union = new Union<int, string>("string");
            var result = union.TryGetValue<string>();
            AreEqual("string", result.Value);
        }

        [Test]
        public void UnionT1T2WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Value<float>());
        }

        [Test]
        public void UnionT1T2WithInvalidTypeTryGetValue_ReturnNone()
        {
            var union = new Union<int, string>(2);
            var result = union.TryGetValue<float>();
            IsFalse(result.HasValue);
        }
    }
}