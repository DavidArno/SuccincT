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
        public static void UnionWithT1_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants, Foods>(1);
            IsTrue(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case3));
            IsFalse(union.HasCase(Variant.Case4));
        }

        [Test]
        public static void UnionWithT2_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants, Foods>("a");
            IsTrue(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case3));
            IsFalse(union.HasCase(Variant.Case4));
        }

        [Test]
        public static void UnionWithT3_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            IsTrue(union.HasCase(Variant.Case3));
            IsFalse(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case4));
        }

        [Test]
        public static void UnionWithT4_HasCaseReturnsCorrectResults()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            IsTrue(union.HasCase(Variant.Case4));
            IsFalse(union.HasCase(Variant.Case1));
            IsFalse(union.HasCase(Variant.Case2));
            IsFalse(union.HasCase(Variant.Case3));
        }

        [Test]
        public void UnionWithT1_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(1);
            var result = union.CaseOf<int>();
            AreEqual(1, result);
        }

        [Test]
        public static void UnionWithT1_CaseOfT2T3OrT4Throws()
        {
            var union = new Union<int, string, Plants, Foods>(1);
            Throws<InvalidCaseException>(() => union.CaseOf<string>());
            Throws<InvalidCaseException>(() => union.CaseOf<Plants>());
            Throws<InvalidCaseException>(() => union.CaseOf<Foods>());
        }

        [Test]
        public void UnionWithT2_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("string");
            var result = union.CaseOf<string>();
            AreEqual("string", result);
        }

        [Test]
        public static void UnionWithT2_CaseOfT1T3orT4Throws()
        {
            var union = new Union<int, string, Plants, Foods>("string");
            Throws<InvalidCaseException>(() => union.CaseOf<int>());
            Throws<InvalidCaseException>(() => union.CaseOf<Plants>());
            Throws<InvalidCaseException>(() => union.CaseOf<Foods>());
        }

        [Test]
        public void UnionWithT3_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            var result = union.CaseOf<Plants>();
            AreEqual(Plants.Rose, result);
        }

        [Test]
        public static void UnionWithT3_CaseOfT1T2OrT4Throws()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            Throws<InvalidCaseException>(() => union.CaseOf<int>());
            Throws<InvalidCaseException>(() => union.CaseOf<string>());
            Throws<InvalidCaseException>(() => union.CaseOf<Foods>());
        }

        [Test]
        public void UnionWithT4_CaseOfMatchesCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var result = union.CaseOf<Foods>();
            AreEqual(Foods.Cake, result);
        }

        [Test]
        public static void UnionWithT4_CaseOfT1T2OrT3Throws()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            Throws<InvalidCaseException>(() => union.CaseOf<int>());
            Throws<InvalidCaseException>(() => union.CaseOf<string>());
            Throws<InvalidCaseException>(() => union.CaseOf<Plants>());
        }

        [Test]
        public void UnionT1T2T3T4WithInvalidTypeValue_ThrowsException()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            Throws<InvalidCaseOfTypeException>(() => union.CaseOf<float>());
        }

        [Test]
        public void UnionT1HasCaseOf_HandledCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            var hasInteger = union.HasCaseOf<int>();
            var hasString = union.HasCaseOf<string>();
            var hasPlants = union.HasCaseOf<Plants>();
            var hasFoods = union.HasCaseOf<Foods>();

            IsTrue(hasInteger);
            IsFalse(hasString);
            IsFalse(hasPlants);
            IsFalse(hasFoods);
        }
        [Test]
        public void UnionT2HasCaseOf_HandledCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("aaa");
            var hasInteger = union.HasCaseOf<int>();
            var hasString = union.HasCaseOf<string>();
            var hasPlants = union.HasCaseOf<Plants>();
            var hasFoods = union.HasCaseOf<Foods>();

            IsTrue(hasString);
            IsFalse(hasInteger);
            IsFalse(hasPlants);
            IsFalse(hasFoods);
        }

        [Test]
        public void UnionT3HasCaseOf_HandledCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            var hasInteger = union.HasCaseOf<int>();
            var hasString = union.HasCaseOf<string>();
            var hasPlants = union.HasCaseOf<Plants>();
            var hasFoods = union.HasCaseOf<Foods>();

            IsTrue(hasPlants);
            IsFalse(hasInteger);
            IsFalse(hasString);
            IsFalse(hasFoods);
        }

        [Test]
        public void UnionT4HasCaseOf_HandledCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var hasInteger = union.HasCaseOf<int>();
            var hasString = union.HasCaseOf<string>();
            var hasPlants = union.HasCaseOf<Plants>();
            var hasFoods = union.HasCaseOf<Foods>();

            IsTrue(hasFoods);
            IsFalse(hasInteger);
            IsFalse(hasString);
            IsFalse(hasPlants);
        }

        [Test]
        public void UnionT1T2T3T4HasValueTest_ReturnsFalseAndDoesNotThrowExceptionTypeNotInUnion()
        {
            var union = new Union<int, string, Plants, Foods>(2);
            var hasBool = union.HasCaseOf<bool>();
            IsFalse(hasBool);
        }

        [Test]
        public static void UnionWithT1_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(1);
            var result1 = union.TryCaseOf<int>(out var value1);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<Plants>(out _);
            var result4 = union.TryCaseOf<Foods>(out _);
            var result5 = union.TryCaseOf<double>(out _);

            IsTrue(result1);
            AreEqual(1, value1);
            IsFalse(result2);
            IsFalse(result3);
            IsFalse(result4);
            IsFalse(result5);
        }

        [Test]
        public static void UnionWithT2_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>("xyz");
            var result1 = union.TryCaseOf<int>(out _);
            var result2 = union.TryCaseOf<string>(out var value);
            var result3 = union.TryCaseOf<Plants>(out _);
            var result4 = union.TryCaseOf<Foods>(out _);
            var result5 = union.TryCaseOf<double>(out _);

            IsTrue(result2);
            AreEqual("xyz", value);
            IsFalse(result1);
            IsFalse(result3);
            IsFalse(result4);
            IsFalse(result5);
        }

        [Test]
        public static void UnionWithT3_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Plants.Rose);
            var result1 = union.TryCaseOf<int>(out _);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<Plants>(out var value);
            var result4 = union.TryCaseOf<Foods>(out _);
            var result5 = union.TryCaseOf<double>(out _);

            IsTrue(result3);
            AreEqual(Plants.Rose, value);
            IsFalse(result1);
            IsFalse(result2);
            IsFalse(result4);
            IsFalse(result5);
        }

        [Test]
        public static void UnionWithT4_HandlesTryCaseOfRespondsCorrectly()
        {
            var union = new Union<int, string, Plants, Foods>(Foods.Cake);
            var result1 = union.TryCaseOf<int>(out _);
            var result2 = union.TryCaseOf<string>(out _);
            var result3 = union.TryCaseOf<Plants>(out _);
            var result4 = union.TryCaseOf<Foods>(out var value);
            var result5 = union.TryCaseOf<double>(out _);

            IsTrue(result4);
            AreEqual(Foods.Cake, value);
            IsFalse(result1);
            IsFalse(result2);
            IsFalse(result3);
            IsFalse(result5);
        }
    }
}