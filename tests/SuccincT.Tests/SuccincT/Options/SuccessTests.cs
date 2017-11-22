using NUnit.Framework;
using SuccincT.Options;
using System;
using SuccincT.PatternMatchers;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class SuccessTests
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

        [Test]
        public void AFailure_CanBeDirectlyCreatedFromTheFailureValue()
        {
            Success<int> failure = 1;
            AreEqual(1, failure.Failure);
        }
        [Test]
        public void WhenFailure_DecomposeReturnsFalseAndError()
        {
            var failure = Success.CreateFailure(1);
            var (isSuccess, error) = failure;
            IsFalse(isSuccess);
            AreEqual(1, error);
        }

        [Test]
        public void WhenSuccess_DecomposeReturnsTrueAndDefault()
        {
            var success = new Success<int>();
            var (isSuccess, error) = success;
            IsTrue(isSuccess);
            AreEqual(0, error);
        }

        [Test]
        public void WhenSuccessAndElseIsDefinedAndNoSuccessMatch_ElseResultIsReturned()
        {
            var success = new Success<int>();
            var result = success.Match<int>().Error().Do(x => 1).Else(o => 0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenSuccessAndElseIsDefinedAndNoSuccessMatch_ElseExpressionIsReturned()
        {
            var success = new Success<int>();
            var result = success.Match<int>().Error().Do(x => 1).Else(0).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenFailureAndElseIsDefinedAndNoErrorMatch_ElseIsExecuted()
        {
            var success = Success.CreateFailure(2);
            var result = 0;
            success.Match().Success().Do(() => result = 1).Else(f => result = f.Failure).Exec();
            AreEqual(2, result);
        }

        [Test]
        public void WhenFailureAndElseIsDefinedAndErrorDoesntMatch_ElseIsExecuted()
        {
            var success = Success.CreateFailure(2);
            var result = 0;
            success.Match()
                   .Error().Of(1).Do(x => result = 1)
                   .Success().Do(() => result = 0)
                   .Else(f => result = f.Failure)
                   .Exec();
            AreEqual(2, result);
        }


        [Test]
        public void WhenFailureAndElseIsDefinedAndNoErrorMatch_ElseResultIsReturned()
        {
            var success = Success.CreateFailure(1);
            var result = success.Match<int>().Success().Do(() => 0).Else(o => o.Failure).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenFailureAndElseIsDefinedAndErrorDoesntMatch_ElseResultIsReturned()
        {
            var success = Success.CreateFailure(2);
            var result = success.Match<int>()
                                .Error().Of(1).Do(x => 1)
                                .Success().Do(() => 0)
                                .Else(o => o.Failure)
                                .Result();
            AreEqual(2, result);
        }

        [Test]
        public void WhenSuccessAndNoMatchDefined_ExceptionThrown()
        {
            var success = new Success<int>();
            Throws<NoMatchException>(() => Ignore(success.Match<int>().Error().Do(x => 1).Result()));
        }

        [Test]
        public void WhenFailureAndNoMatchDefined_ExceptionThrown()
        {
            var success = Success.CreateFailure(1);
            Throws<NoMatchException>(() => Ignore(success.Match<int>().Success().Do(() => 0).Result()));
        }

        [Test]
        public void WhenSuccessAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var success = new Success<int>();
            Throws<NoMatchException>(() => success.Match().Error().Do(x => { }).Exec());
        }

        [Test]
        public void WhenFailureAndNoMatchDefinedForExec_ExceptionThrown()
        {
            var success = Success.CreateFailure(1);
            Throws<NoMatchException>(() => success.Match().Success().Do(() => { }).Exec());
        }

        [Test]
        public void WhenFailure_ErrorDoMatchSupported()
        {
            var success = Success.CreateFailure(1);
            var result = success.Match<int>().Error().Do(1).Success().Do(2).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenFailure_ErrorOfDoSupported()
        {
            var success = Success.CreateFailure(1);
            var result = success.Match<int>().Error().Of(1).Do(1).Error().Do(2).Success().Do(3).Result();
            AreEqual(1, result);
        }

        [Test]
        public void WhenFailure_ErrorWhereDoSupported()
        {
            var success = Success.CreateFailure(1);
            var result = success.Match<int>().Error().Where(x => x < 2).Do(0).Error().Do(2).Success().Do(3).Result();
            AreEqual(0, result);
        }

        [Test]
        public void WhenSuccess_NoneDoSupported()
        {
            var success = new Success<int>();
            var result = success.Match<int>().Error().Do(1).Success().Do(2).Result();
            AreEqual(2, result);
        }

        [Test]
        public void WhenSuccessAndElseIsDefinedAndSuccessMatch_SuccessIsExecuted()
        {
            var success = new Success<int>();
            var result = 0;
            success.Match().Success().Do(() => result = 1).Else(s => result = s.Failure).Exec();
            AreEqual(1, result);
        }

        [Test]
        public void SuccessHashCode_Is1()
        {
            var success = new Success<float>();
            AreEqual(1, success.GetHashCode());
        }

        [Test]
        public void FailureHashCode_IsSameAsValueHashCode()
        {
            var success = Success.CreateFailure("this is a failure");
            AreEqual("this is a failure".GetHashCode(), success.GetHashCode());
        }
    }
}
