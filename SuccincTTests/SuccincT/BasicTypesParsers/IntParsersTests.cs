using NUnit.Framework;
using SuccincT.BasicTypesParsers;
using SuccincT.Unions;

namespace SuccincTTests.SuccincT.BasicTypesParsers
{
    /// <summary>
    /// Defines a set of tests for the extension methods in the IntParsers class.
    /// </summary>
    /// <remarks>
    /// The first first few tests test Int32 parsing in details to cover the common parsing/success
    /// reporting mechanism, as well as parsing Int32 numbers. The other tests consist of just 
    /// one test of valid numbers and one for invalid numbers for all the other int types.
    /// </remarks>
    [TestFixture]
    public class IntParsersTests
    {
        [Test]
        public void ValidIntString_ResultsInSuccess()
        {
            var result = "12345".ParseInt();
            Assert.AreEqual(12345, result.Case1);
        }

        [Test]
        public void ValidNegativeIntString_ResultsInSuccess()
        {
            var result = "-12345".ParseInt();
            Assert.AreEqual(-12345, result.Case1);
        }

        [Test]
        public void InvalidIntString_ResultsInNone()
        {
            var result = "la la".ParseInt();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void OutOfRangeIntString_ResultsInError()
        {
            var result = "-3000000000".ParseInt();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidUnsignedIntString_ResultsInSuccess()
        {
            var result = "4000000000".ParseUnsignedInt();
            Assert.AreEqual(4000000000, result.Case1);
        }

        [Test]
        public void InvalidUnsignedIntString_ResultsInError()
        {
            var result = "-1".ParseUnsignedInt();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidSignedByteString_ResultsInSuccess()
        {
            var result = "-123".ParseSignedByte();
            Assert.AreEqual(-123, result.Case1);
        }

        [Test]
        public void InvalidSignedByteString_ResultsInError()
        {
            var result = "180".ParseSignedByte();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidUnsignedByteString_ResultsInSuccess()
        {
            var result = "200".ParseUnsignedByte();
            Assert.AreEqual(200, result.Case1);
        }

        [Test]
        public void InvalidUnsignedByteString_ResultsInError()
        {
            var result = "-1".ParseUnsignedByte();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidShortString_ResultsInSuccess()
        {
            var result = "-10000".ParseShort();
            Assert.AreEqual(-10000, result.Case1);
        }

        [Test]
        public void InvalidShortString_ResultsInError()
        {
            var result = "200000".ParseShort();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidUnsignedShortString_ResultsInSuccess()
        {
            var result = "300".ParseUnsignedShort();
            Assert.AreEqual(300, result.Case1);
        }

        [Test]
        public void InvalidUnsignedShortString_ResultsInError()
        {
            var result = "-1000".ParseUnsignedShort();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidLongString_ResultsInSuccess()
        {
            var result = "-23000000000".ParseLong();
            Assert.AreEqual(-23000000000, result.Case1);
        }

        [Test]
        public void InvalidLongString_ResultsInError()
        {
            var result = "not a number".ParseLong();
            Assert.AreEqual(Variant.Case2, result.Case);
        }

        [Test]
        public void ValidUnsignedLongString_ResultsInSuccess()
        {
            var result = "18446744073709551615".ParseUnsignedLong();
            Assert.AreEqual(18446744073709551615, result.Case1);
        }
    }
}
