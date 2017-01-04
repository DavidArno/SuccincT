using NUnit.Framework;
using SuccincT.Options;
using System;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    class SuccessTests
    {
        [Test]
        public void CreatingSuccessValue_ResultsInNoFailure()
        {
            var success = new Success<int>();
            IsTrue(success);
            IsFalse(success.IsFailure);
            Throws<InvalidOperationException>(() => Ignore(success.Failure));
        }

        [Test]
        public void CreatingFailureValue_ResultsInFailure()
        {
            var failure = Success.CreateFailure(1);
            IsFalse(failure);
            IsTrue(failure.IsFailure);
            AreEqual(1, failure.Failure);
        }

        [Test]
        public void WhenCreatingErrorValue_ErrorMustNotBeNull() =>
            Throws<ArgumentNullException>(() => Success.CreateFailure<string>(null));

        [Test]
        public void TwoSuccesses_AreEqual()
        {
            var success1 = new Success<int>();
            var success2 = new Success<int>();
            AreEqual(success1, success2);
            IsTrue(success1 == success2);
            IsFalse(success1 != success2);
        }

        [Test]
        public void TwoSuccessesWithDifferentTypes_AreNotEqual()
        {
            var success1 = new Success<int>();
            var success2 = new Success<string>();
            AreNotEqual(success1, success2);
            IsFalse(success1 == success2);
            IsTrue(success1 != success2);
        }

        [Test]
        public void SuccessesAndFailure_AreNotEqual()
        {
            var success = new Success<int>();
            var failure = Success.CreateFailure(1);
            AreNotEqual(success, failure);
            IsFalse(success == failure);
            IsTrue(success != failure);
        }

        [Test]
        public void TwoFailuresWithSameError_AreEqual()
        {
            var failure1 = Success.CreateFailure(1);
            var failure2 = Success.CreateFailure(1);
            AreEqual(failure1, failure2);
            IsTrue(failure1 == failure2);
            IsFalse(failure1 != failure2);
        }

        [Test]
        public void TwoFailuresWithDifferentErrors_AreNotEqual()
        {
            var failure1 = Success.CreateFailure(1);
            var failure2 = Success.CreateFailure(2);
            AreNotEqual(failure1, failure2);
            IsFalse(failure1 == failure2);
            IsTrue(failure1 != failure2);
        }

        [Test]
        public void TwoFailuresWithDifferentTypes_AreNotEqual()
        {
            var failure1 = Success.CreateFailure(1);
            var failure2 = Success.CreateFailure("1");
            AreNotEqual(failure1, failure2);
            IsFalse(failure1 == failure2);
            IsTrue(failure1 != failure2);
        }
    }
}
