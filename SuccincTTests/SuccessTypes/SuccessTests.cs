using System.IO;
using NUnit.Framework;

using SuccincT.Exceptions;
using SuccincT.SuccessTypes;

namespace SuccincTTests.SuccessTypes
{
    [TestFixture]
    public class SuccessTests
    {
        [Test]
        public void WhenResultIsSuccessful_ResultHasValue()
        {
            var result = new Success<int> { Successful = true, Value = 1 };
            Assert.AreEqual(1, result.Value);
        }

        [Test, ExpectedException(exceptionType: typeof(InvalidSuccessOperationException))]
        public void WhenResultIsSuccessful_ResultHasNoErrorMessage()
        {
            var result = new Success<int> { Successful = true, FailureReason = "not accessible" };
            NullSink(result.FailureReason);
        }

        [Test, ExpectedException(exceptionType: typeof(InvalidSuccessOperationException))]
        public void WhenResultNotSuccessful_ResultHasNoValue()
        {
            var result = new Success<int> { Successful = false, Value = 1 };
            NullSink(result.Value);
        }

        [Test]
        public void WhenResultNotSuccessful_ResultHasErrorMessage()
        {
            var result = new Success<int> { Successful = false, FailureReason = "la la" };
            Assert.AreEqual("la la", result.FailureReason);
        }

        /// <summary>
        /// Provides a means of disposing of a result read, which would otherwise upset the compiler
        /// or ReSharper. In no way is this a hack ;)
        /// </summary>
        private void NullSink(object anything)
        {
            StreamWriter.Null.Write(anything);
        }
    }
}
