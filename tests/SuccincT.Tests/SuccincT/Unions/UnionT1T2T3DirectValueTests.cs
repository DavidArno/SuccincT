using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public sealed class UnionT1T2T3DirectValueTests
    {
        private enum Plants { Rose }

        [Test]
        public void UnionWithT1_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(1);
            var result = union.Value<int>();
            AreEqual(1, result);
        }

        [Test]
        public void UnionWithT2_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>("string");
            var result = union.Value<string>();
            AreEqual("string", result);
        }

        [Test]
        public void UnionWithT3_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Rose);
            var result = union.Value<Plants>();
            AreEqual(Plants.Rose, result);
        }

        [Test]
        public void UnionT1T2T3WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string, Plants>(2);
            Throws<InvalidCaseOfTypeException>(() => union.Value<float>());
        }
        
        [Test]
        public void UnionT1HasValueTest_ReturnsTrue()
        {
            var union = new Union<int, string, Plants>(2);
            var hasInteger = union.HasValue<int>();
            IsTrue(hasInteger);
        }
        
        [Test]
        public void UnionT1T2T3HasValueTest_ReturnsFalse()
        {
            var union = new Union<int, string, Plants>(2);
            var hasInteger = union.HasValue<Plants>();
            IsFalse(hasInteger);
        }
        
        [Test]
        public void UnionT1T2T3HasValueTest_ReturnsFalseAndDoesNotThrowExceptionTypeNotInUnion()
        {
            var union = new Union<int, string, Plants>(2);
            var hasBool = union.HasValue<bool>();
            IsFalse(hasBool);
        }
    }
}