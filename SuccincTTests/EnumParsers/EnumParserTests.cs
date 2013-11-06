using System;
using NUnit.Framework;
using SuccincT.EnumParsers;
using SuccincT.Unions;

namespace SuccincTTests.EnumParsers
{
    [TestFixture]
    public class EnumParserTests
    {
        [Test]
        public void ValidEnumValue_CorrectlyParsed()
        {
            var actual = "Value1".ParseEnum<TestEnum>();
            Assert.AreEqual(TestEnum.Value1, actual.Case1);
        }

        [Test]
        public void WrongCaseEnumValue_CorrectlyParsedIfCaseIgnored()
        {
            var actual = "value2".ParseEnumIgnoringCase<TestEnum>();
            Assert.AreEqual(TestEnum.Value2, actual.Case1);
        }

        [Test]
        public void InvalidEnumValue_ResultsInError()
        {
            var actual = "nonsense".ParseEnum<TestEnum>();
            Assert.AreEqual(Variant.Case2, actual.Case);
        }

        [Test]
        public void InvalidEnumValue_ResultsInErrorWhenCaseIgnored()
        {
            var actual = "nonsense".ParseEnumIgnoringCase<TestEnum>();
            Assert.AreEqual(Variant.Case2, actual.Case);
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
