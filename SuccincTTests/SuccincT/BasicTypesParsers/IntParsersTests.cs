using NUnit.Framework;
using SuccincT.Parsers;

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
        public void ValidIntString_ResultsInValue()
        {
            var result = "12345".ParseInt();
            Assert.AreEqual(12345, result.Value);
        }

        [Test]
        public void ValidNegativeIntString_ResultsInValue()
        {
            var result = "-12345".ParseInt();
            Assert.AreEqual(-12345, result.Value);
        }

        [Test]
        public void InvalidIntString_ResultsInNone()
        {
            var result = "la la".ParseInt();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void OutOfRangeIntString_ResultsInNone()
        {
            var result = "-3000000000".ParseInt();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidUnsignedIntString_ResultsInValue()
        {
            var result = "4000000000".ParseUnsignedInt();
            Assert.AreEqual(4000000000, result.Value);
        }

        [Test]
        public void InvalidUnsignedIntString_ResultsInNone()
        {
            var result = "-1".ParseUnsignedInt();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidSignedByteString_ResultsInValue()
        {
            var result = "-123".ParseSignedByte();
            Assert.AreEqual(-123, result.Value);
        }

        [Test]
        public void InvalidSignedByteString_ResultsInNone()
        {
            var result = "180".ParseSignedByte();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidUnsignedByteString_ResultsInValue()
        {
            var result = "200".ParseUnsignedByte();
            Assert.AreEqual(200, result.Value);
        }

        [Test]
        public void InvalidUnsignedByteString_ResultsInNone()
        {
            var result = "-1".ParseUnsignedByte();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidShortString_ResultsInValue()
        {
            var result = "-10000".ParseShort();
            Assert.AreEqual(-10000, result.Value);
        }

        [Test]
        public void InvalidShortString_ResultsInNone()
        {
            var result = "200000".ParseShort();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidUnsignedShortString_ResultsInValue()
        {
            var result = "300".ParseUnsignedShort();
            Assert.AreEqual(300, result.Value);
        }

        [Test]
        public void InvalidUnsignedShortString_ResultsInNone()
        {
            var result = "-1000".ParseUnsignedShort();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidLongString_ResultsInValue()
        {
            var result = "-23000000000".ParseLong();
            Assert.AreEqual(-23000000000, result.Value);
        }

        [Test]
        public void InvalidLongString_ResultsInNone()
        {
            var result = "not a number".ParseLong();
            Assert.IsFalse(result.HasValue);
        }

        [Test]
        public void ValidUnsignedLongString_ResultsInValue()
        {
            var result = "18446744073709551615".ParseUnsignedLong();
            Assert.AreEqual(18446744073709551615, result.Value);
        }
    }
}