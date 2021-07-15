using NUnit.Framework;
using System;
using SuccincT.Options;
using SuccincT.Parsers;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.EnumParsers
{
    [TestFixture]
    public class EnumCharParserTests
    {
        private enum TestType
        {
            Foo = 'F',
            Bar = 'B'
        }

        private enum NotCharType
        {
            Foo,
            Bar
        }

        private struct JustAStruct
        {

        }

        [Test]
        public void NonEnumShouldThrowException()
        {
            // GIVEN a char

            // WHEN it is parsed into a type that is not an enum
            Action action = () =>
            {
                'F'.TryParsEnum<JustAStruct>();
            };

            // THEN an argument exception should be thrown
            Throws<ArgumentException>(new TestDelegate(action), "an argument exception should have been thrown");
        }

        [Test]
        public void EnumNotKeyedToCharShouldReturnNone()
        {
            // GIVEN a char
            // WHEN it is parsed into an enum type that is not keyed to chars
            var result = 'C'.TryParsEnum<NotCharType>();

            // THEN it should return option none
            AreEqual(Option<NotCharType>.None(), result, "if the enum is not keyed to chars, it should return none");
        }

        [Test]
        public void InvalidValueReturnsNone()
        {
            // GIVEN a char
            // WHEN it is parsed into an enum type that doesn't have a corresponding value
            var result = 'C'.TryParsEnum<TestType>();

            // THEN it should return option none
            AreEqual(Option<TestType>.None(),result, "if the char doesn't correspond to an enum value, it should return none");
        }

        [Test]
        public void ValidCharReturnsProperEnumValue()
        {
            // GIVEN a char
            // WHEN it is parsed into an enum type with the corresponding char value
            var result = 'F'.TryParsEnum<TestType>();

            // THEN the corresponding enum value option should be returned
            AreEqual(Option<TestType>.Some(TestType.Foo), result,
                "result should have returned some with the correct enum value");
        }


    }
}
