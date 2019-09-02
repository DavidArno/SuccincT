using NUnit.Framework;
using SuccincT.Unions;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Unions
{
    public static class UnionT1T2T3DirectValueTests
    {
        private enum Plants { Rose }

        [Test]
        public static void UnionWithT1_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants>(1);
            IsTrue(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case3));
        }

        [Test]
        public static void UnionWithT2_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants>("123");
            IsTrue(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case3));
        }

        [Test]
        public static void UnionWithT3_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants>(Plants.Rose);
            IsTrue(union.HasCase(Variant.Case3));
            IsFalse(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case2));
        }

        [Test]
        public static void UnionWithT1_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(1);
            var result = union.CaseOf<int>();
            AreEqual(1, result);
        }

        [Test]
        public static void UnionWithT1_CaseOfT2OrT3Throws()
        {
            var union = new Union<int, string, Plants>(1);
            Throws<InvalidCaseException>(() => union.CaseOf<string>());
            Throws<InvalidCaseException>(() => union.CaseOf<Plants>());
        }

        [Test]
        public static void UnionWithT2_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>("string");
            var result = union.CaseOf<string>();
            AreEqual("string", result);
        }

        [Test]
        public static void UnionWithT2_CaseOfT1orT3Throws()
        {
            var union = new Union<int, string, Plants>("string");
            Throws<InvalidCaseException>(() => union.CaseOf<int>());
            Throws<InvalidCaseException>(() => union.CaseOf<Plants>());
        }

        [Test]
        public static void UnionWithT3_ValueMatchesCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Rose);
            var result = union.CaseOf<Plants>();
            AreEqual(Plants.Rose, result);
        }

        [Test]
        public static void UnionWithT3_CaseOfT1OrT2Throws()
        {
            var union = new Union<int, string, Plants>(Plants.Rose);
            Throws<InvalidCaseException>(() => union.CaseOf<int>());
            Throws<InvalidCaseException>(() => union.CaseOf<string>());
        }

        [Test]
        public static void UnionT1T2T3WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string, Plants>(2);
            Throws<InvalidCaseOfTypeException>(() => union.CaseOf<float>());
        }

        [Test]
        public static void UnionWithT1HasHasCaseOfTest_ReturnsTrue()
        {
            var union = new Union<int, string, Plants>(2);
            var hasInteger = union.HasCaseOf<int>();
            IsTrue(hasInteger);
        }

        [Test]
        public static void UnionWithT1AndHasCaseOfT2Test_ReturnsFalse()
        {
            var union = new Union<int, string, Plants>(2);
            var hasString = union.HasCaseOf<string>();
            IsFalse(hasString);
        }

        [Test]
        public static void UnionWithT1AndHasCaseOfT3Test_ReturnsFalse()
        {
            var union = new Union<int, string, Plants>(2);
            var hasPlants = union.HasCaseOf<Plants>();
            IsFalse(hasPlants);
        }

        [Test]
        public static void UnionWithT2HasHasCaseOfTest_ReturnsTrue()
        {
            var union = new Union<int, string, Plants>("x");
            var hasString = union.HasCaseOf<string>();
            IsTrue(hasString);
        }

        [Test]
        public static void UnionWithT2AndHasCaseOfT1Test_ReturnsFalse()
        {
            var union = new Union<int, string, Plants>("x");
            var hasInteger = union.HasCaseOf<int>();
            IsFalse(hasInteger);
        }

        [Test]
        public static void UnionWithT2AndHasCaseOfT3Test_ReturnsFalse()
        {
            var union = new Union<int, string, Plants>("x");
            var hasPlants = union.HasCaseOf<Plants>();
            IsFalse(hasPlants);
        }

        [Test]
        public static void UnionT1T2T3HasValueTest_ReturnsFalseAndDoesNotThrowExceptionTypeNotInUnion()
        {
            var union = new Union<int, string, Plants>(2);
            var hasBool = union.HasCaseOf<bool>();
            IsFalse(hasBool);
        }

        [Test]
        public static void UnionWithT1_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants>(1);
            var result1 = union.TryCaseOf<int>(out var value1);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<Plants>(out _);
            var result4 = union.TryCaseOf<double>(out _);

            IsTrue(result1);
            AreEqual(1, value1);
            IsFalse(result2);
            IsFalse(result3);
            IsFalse(result4);
        }

        [Test]
        public static void UnionWithT2_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants>("qwerty");
            var result1 = union.TryCaseOf<int>(out _);
            var result2 = union.TryCaseOf<string>(out var value);
            var result3 = union.TryCaseOf<Plants>(out _);
            var result4 = union.TryCaseOf<double>(out _);

            IsTrue(result2);
            AreEqual("qwerty", value);
            IsFalse(result1);
            IsFalse(result3);
            IsFalse(result4);
        }

        [Test]
        public static void UnionWithT3_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants>(Plants.Rose);
            var result1 = union.TryCaseOf<int>(out _);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<Plants>(out var value);
            var result4 = union.TryCaseOf<double>(out _);

            IsTrue(result3);
            AreEqual(Plants.Rose, value);
            IsFalse(result1);
            IsFalse(result2);
            IsFalse(result4);
        }
    }
}