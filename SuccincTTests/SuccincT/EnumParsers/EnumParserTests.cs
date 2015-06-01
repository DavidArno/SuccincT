using System;
using NUnit.Framework;
using SuccincT.Parsers;

namespace SuccincTTests.SuccincT.EnumParsers
{
    [TestFixture]
    public class EnumParserTests
    {
        [Test]
        public void ValidEnumValue_CorrectlyParsed()
        {
            var actual = "Value1".ParseEnum<TestEnum>();
            Assert.AreEqual(TestEnum.Value1, actual.Value);
        }

        [Test]
        public void ValidEnumValue_HasValue()
        {
            var actual = "Value1".ParseEnum<TestEnum>();
            Assert.IsTrue(actual.HasValue);
        }

        [Test]
        public void WrongCaseEnumValue_CorrectlyParsedIfCaseIgnored()
        {
            var actual = "value2".ParseEnumIgnoringCase<TestEnum>();
            Assert.AreEqual(TestEnum.Value2, actual.Value);
        }

        [Test]
        public void InvalidEnumValue_ResultsInNoValue()
        {
            var actual = "nonsense".ParseEnum<TestEnum>();
            Assert.IsFalse(actual.HasValue);
        }

        [Test]
        public void InvalidEnumValue_ResultsInNoValueWhenCaseIgnored()
        {
            var actual = "nonsense".ParseEnumIgnoringCase<TestEnum>();
            Assert.IsFalse(actual.HasValue);
        }

        [Test, ExpectedException(exceptionType: typeof(ArgumentException))]
        public void ParsingNonEnum_ResultsInException()
        {
            "true".ParseEnum<bool>();
        }

        [Test, ExpectedException(exceptionType: typeof(ArgumentException))]
        public void ParsingWithCaseIgnoreNonEnum_ResultsInException()
        {
            "1".ParseEnumIgnoringCase<int>();
        }

        private enum TestEnum { Value1, Value2 }
    }
}
