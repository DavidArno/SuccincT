using NUnit.Framework;
using SuccincT.BasicTypesParsers;

namespace SuccincTTests.BasicTypesParsers
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
            var expected = new object[] { true, 12345 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ValidNegativeIntString_ResultsInSuccess()
        {
            var result = "-12345".ParseInt();
            var expected = new object[] { true, -12345 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidIntString_ResultsInError()
        {
            var result = "la la".ParseInt();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void InvalidIntString_ResultsInErrorMessage()
        {
            var result = "la la".ParseInt();
            Assert.AreEqual("la la isn't a Int32 value.", result.FailureReason);
        }

        [Test]
        public void OutOfRangeIntString_ResultsInError()
        {
            var result = "-3000000000".ParseInt();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidUnsignedIntString_ResultsInSuccess()
        {
            var result = "4000000000".ParseUnsignedInt();
            var expected = new object[] { true, 4000000000 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidUnsignedIntString_ResultsInError()
        {
            var result = "-1".ParseUnsignedInt();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidSignedByteString_ResultsInSuccess()
        {
            var result = "-123".ParseSignedByte();
            var expected = new object[] { true, -123 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidSignedByteString_ResultsInError()
        {
            var result = "180".ParseSignedByte();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidUnsignedByteString_ResultsInSuccess()
        {
            var result = "200".ParseUnsignedByte();
            var expected = new object[] { true, 200 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidUnsignedByteString_ResultsInError()
        {
            var result = "-1".ParseUnsignedByte();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidShortString_ResultsInSuccess()
        {
            var result = "-10000".ParseShort();
            var expected = new object[] { true, -10000 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidShortString_ResultsInError()
        {
            var result = "200000".ParseShort();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidUnsignedShortString_ResultsInSuccess()
        {
            var result = "300".ParseUnsignedShort();
            var expected = new object[] { true, 300 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidUnsignedShortString_ResultsInError()
        {
            var result = "-1000".ParseUnsignedShort();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidLongString_ResultsInSuccess()
        {
            var result = "-23000000000".ParseLong();
            var expected = new object[] { true, -23000000000 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidLongString_ResultsInError()
        {
            var result = "not a number".ParseLong();
            Assert.IsFalse(result.Successful);
        }

        [Test]
        public void ValidUnsignedLongString_ResultsInSuccess()
        {
            var result = "18446744073709551615".ParseUnsignedLong();
            var expected = new object[] { true, 18446744073709551615 };
            var actual = new object[] { result.Successful, result.Value };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidUnsignedLongString_ResultsInError()
        {
            var result = "-1000".ParseUnsignedLong();
            Assert.IsFalse(result.Successful);
        }
    }
}
