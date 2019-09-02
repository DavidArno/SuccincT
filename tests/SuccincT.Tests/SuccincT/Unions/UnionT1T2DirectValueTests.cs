using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public static class UnionT1T2DirectValueTests
    {
        [Test]
        public static void UnionWithT1_ValueMatchesCorrectly()
        {
            var union = new Union<int, string>(1);
            var result = union.CaseOf<int>();
            AreEqual(1, result);
        }

        [Test]
        public static void UnionWithT1_TryGetsCaseCorrectly()
        {
            var union = new Union<int, string>(1);
            var result = union.TryCaseOf<int>(out var value);
            IsTrue(result);
            AreEqual(1, value);
        }

        [Test]
        public static void UnionWithT2_ValueMatchesCorrectly()
        {
            var union = new Union<int, string>("string");
            var result = union.CaseOf<string>();
            AreEqual("string", result);
        }

        [Test]
        public static void UnionWithT2_TryGetCaseCorrectly()
        {
            var union = new Union<int, string>("string");
            var result = union.TryCaseOf<string>(out var value);
            IsTrue(result);
            AreEqual("string", value);
        }

        [Test]
        public static void UnionT1T2WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string>(2);
            Throws<InvalidCaseOfTypeException>(() => union.CaseOf<float>());
        }

        [Test]
        public static void UnionT1T2WithWrongCaseTypeValue_ThrowsException()
        {
            var union1 = new Union<int, string>(2);
            var union2 = new Union<int, string>("2");

            Throws<InvalidCaseException>(() => union1.CaseOf<string>());
            Throws<InvalidCaseException>(() => union2.CaseOf<int>());
        }

        [Test]
        public static void UnionT1HasValueTestWhenT1_ReturnsTrueForT1()
        {
            var union = new Union<int, string>(2);
            var hasInteger1 = union.HasCaseOf<int>();
            var hasInteger2 = union.HasCase(Variant.Case1);
            IsTrue(hasInteger1);
            IsTrue(hasInteger2);
        }

        [Test]
        public static void UnionT1T2HasValueTestWhenT1_ReturnsFalseForT2()
        {
            var union = new Union<int, string>(2);
            var hasString1 = union.HasCaseOf<string>();
            var hasString2 = union.HasCase(Variant.Case2);
            IsFalse(hasString1);
            IsFalse(hasString2);
        }

        [Test]
        public static void UnionT1T2HasValueTestWhenT2_ReturnsFalseForT1()
        {
            var union = new Union<int, string>("2");
            var hasInteger1 = union.HasCaseOf<int>();
            var hasInteger2 = union.HasCase(Variant.Case1);
            IsFalse(hasInteger1);
            IsFalse(hasInteger2);
        }

        [Test]
        public static void UnionT1HasValueTestWhenT2_ReturnsTrueForT2()
        {
            var union = new Union<int, string>("2");
            var hasString1 = union.HasCaseOf<string>();
            var hasString2 = union.HasCase(Variant.Case2);
            IsTrue(hasString1);
            IsTrue(hasString2);
        }

        [Test]
        public static void UnionT1T2HasValueTest_ReturnsFalseAndDoesNotThrowExceptionWhenTypeNotInUnion()
        {
            var union = new Union<int, string>(2);
            var hasBool = union.HasCaseOf<bool>();
            IsFalse(hasBool);
        }

        [Test]
        public static void UnionWithT1_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string>(1);
            var result1 = union.TryCaseOf<int>(out var value1);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<double>(out _);

            IsTrue(result1);
            AreEqual(1, value1);
            IsFalse(result2);
            IsFalse(result3);
        }
    }
}