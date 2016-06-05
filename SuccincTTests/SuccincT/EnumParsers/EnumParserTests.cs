using System;
using NUnit.Framework;
using SuccincT.Parsers;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.EnumParsers
{
    [TestFixture]
    public sealed class EnumParserTests
    {
        [Test]
        public void ValidEnumValue_CorrectlyParsed()
        {
            var actual = "Value1".TryParseEnum<TestEnum>();
            AreEqual(TestEnum.Value1, actual.Value);
        }

        [Test]
        public void ValidEnumValue_HasValue()
        {
            var actual = "Value1".TryParseEnum<TestEnum>();
            IsTrue(actual.HasValue);
        }

        [Test]
        public void WrongCaseEnumValue_CorrectlyParsedIfCaseIgnored()
        {
            var actual = "value2".TryParseEnumIgnoringCase<TestEnum>();
            AreEqual(TestEnum.Value2, actual.Value);
        }

        [Test]
        public void InvalidEnumValue_ResultsInNoValue()
        {
            var actual = "nonsense".TryParseEnum<TestEnum>();
            IsFalse(actual.HasValue);
        }

        [Test]
        public void InvalidEnumValue_ResultsInNoValueWhenCaseIgnored()
        {
            var actual = "nonsense".TryParseEnumIgnoringCase<TestEnum>();
            IsFalse(actual.HasValue);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ParsingNonEnum_ResultsInException() => "2".TryParseEnum<int>();

        [Test, ExpectedException(typeof(ArgumentException))]
        public void ParsingWithCaseIgnoreNonEnum_ResultsInException() => "1".TryParseEnumIgnoringCase<int>();

        private enum TestEnum { Value1, Value2 }
    }
}