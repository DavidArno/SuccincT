using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using SuccincT.Functional;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public static class WithExtensionsAdverseConditionsTests
    {
        [Test]
        public static void TryWithOfTypeWithConstructorAndNoGetters_FailsCleanly()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            var y = x.TryWith(new { });

            IsFalse(y.HasValue);
        }

        [Test]
        public static void WithOfTypeWithConstructorAndNoGetters_ThrowsCopyException()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            
            Throws<CopyException>(() => x.With(new { }));
        }

        [Test]
        public static void TryWithOfTypeWithConstructorAndNoGettersSuppliedWithWrongParam_FailsCleanly()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            var y = x.TryWith(new { x = "2" });

            IsFalse(y.HasValue);
        }

        [Test]
        public static void WithOfTypeWithConstructorAndNoGettersSuppliedWithWrongParam_ThrowsCopyException()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            
            Throws<CopyException>(() => x.With(new { x = "2" }));
        }

        [Test]
        public static void TryWithOfTypeWithConstructorAndNoGettersSuppliedWithWrongParamName_FailsCleanly()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            var y = x.TryWith(new { y = 1 });

            IsFalse(y.HasValue);
        }

        [Test]
        public static void WithOfTypeWithConstructorAndNoGettersSuppliedWithWrongParamName_ThrowsCopyException()
        {
            var x = new TypeWithConstructorAndNoGetters(1);
            
            Throws<CopyException>(() => x.With(new { y = 1 }));
        }

        [Test]
        public static void TryWithOfTypeWithConstructorAndNoMatchingGetter_CanHavePropSetViaConstructor()
        {
            var x = new TypeWithConstructorAndNoMatchingGetter(1);
            var y = x.TryWith(new { a = 2 });

            AreEqual(2, y.Value.B);
        }

        [Test]
        public static void WithOfTypeWithConstructorAndNoMatchingGetter_CanHavePropSetViaConstructor()
        {
            var x = new TypeWithConstructorAndNoMatchingGetter(1);
            var y = x.With(new { a = 2 });

            AreEqual(2, y.B);
        }

        [Test]
        public static void TryWithOfTypeWithConstructorAndNoMatchingGetter_FailsCleanlyWhenPropertyNameRatherThanParamNameIsSupplied()
        {
            var x = new TypeWithConstructorAndNoMatchingGetter(1);
            var y = x.TryWith(new { b = 2 });

            IsFalse(y.HasValue);
        }

        [Test]
        public static void WithOfTypeWithConstructorAndNoMatchingGetter_ThrowsCopyExceptionWhenPropertyNameRatherThanParamNameIsSupplied()
        {
            var x = new TypeWithConstructorAndNoMatchingGetter(1);

            Throws<CopyException>(() => x.With(new { b = 2 }));
        }

        [Test]
        public static void TryWithOfTypeWithMultipleConstructors_ChoosesCorrectConstructor()
        {
            var x = new TypeWithMultipleConstructors(1);
            var (_, value) = x.TryWith(new { B = 3 });

            AreEqual(1, value.A);
            AreEqual(3, value.B);
        }

        [Test]
        public static void WithOfTypeWithMultipleConstructors_ChoosesCorrectConstructor()
        {
            var x = new TypeWithMultipleConstructors(1);
            var y = x.With(new { B = 3 });

            AreEqual(1, y.A);
            AreEqual(3, y.B);
        }

        [Test]
        public static void TryWithOfTypeToTestPropertiesSetViaConstructorArentSetAfterConstruction_OnlySetsPropertyViaConstructor()
        {
            var x = new TypeToTestPropertiesSetViaConstructorArentSetAfterConstruction(2);
            var y = x.TryWith(new { a = 3 });

            AreEqual(3, y.Value.A);
        }

        [Test]
        public static void WithOfTypeToTestPropertiesSetViaConstructorArentSetAfterConstruction_OnlySetsPropertyViaConstructor()
        {
            var x = new TypeToTestPropertiesSetViaConstructorArentSetAfterConstruction(2);
            var y = x.With(new { a = 3 });

            AreEqual(3, y.A);
        }

        [Test]
        public static void TryWithOfTypeToTestPropertiesAreOnlySetOnceWhenNoConstructor_OnlySetsPropertyOnce()
        {
            var x = new TypeToTestPropertiesAreOnlySetOnceWhenNoConstructor { A = 2 };
            var y = x.TryWith(new { a = 3 });

            AreEqual(3, y.Value.A);
        }

        [Test]
        public static void WithOfTypeToTestPropertiesAreOnlySetOnceWhenNoConstructor_OnlySetsPropertyOnce()
        {
            var x = new TypeToTestPropertiesAreOnlySetOnceWhenNoConstructor { A = 2 };
            var y = x.With(new { a = 3 });

            AreEqual(3, y.A);
        }

        private class TypeWithConstructorAndNoGetters
        {
            // ReSharper disable once UnusedParameter.Local
            public TypeWithConstructorAndNoGetters(int x) { }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private class TypeWithConstructorAndNoMatchingGetter
        {
            public TypeWithConstructorAndNoMatchingGetter(int a, int c) { }
            public TypeWithConstructorAndNoMatchingGetter(int a) => B = a;
            public int B { get; }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private class TypeWithMultipleConstructors
        {
            public TypeWithMultipleConstructors(int a) => (A, B) = (a, 1);
            public TypeWithMultipleConstructors(int a, int b) => (A, B) = (a, b);
            public TypeWithMultipleConstructors(int a, int c, int b) => (A, B) = (a, b);
            public int A { get; }
            public int B { get; }
        }

        private class TypeToTestPropertiesSetViaConstructorArentSetAfterConstruction
        {
            private int _a;

            public TypeToTestPropertiesSetViaConstructorArentSetAfterConstruction(int a) => A = a;

            public int A
            {
                get => _a;
                set => _a += value;
            }
        }

        private class TypeToTestPropertiesAreOnlySetOnceWhenNoConstructor
        {
            private int _a;

            public int A
            {
                get => _a;
                set => _a += value;
            }
        }
    }
}