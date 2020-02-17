using System;
using NUnit.Framework;
using SuccincT.Functional;
using SuccincT.Options;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public sealed class OptionExtensionsTests
    {
        [Test]
        public void WhenOptionHasValue_MapAppliesFuncToThatValueAndReturnsNewOptionWithThatValue()
        {
            var opt = Option<int>.Some(50);
            var mappedOpt = opt.Map(x => x * x);
            IsTrue(mappedOpt.HasValue);
            AreEqual(2500, mappedOpt.Value);
        }

        [Test]
        public void WhenOptionHasNoValue_MapReturnsNone()
        {
            var opt = Option<bool>.None();
            var mappedOpt = opt.Map(x => x ? 300 : 100);
            IsFalse(mappedOpt.HasValue);
        }

        [Test]
        public void WhenOptionHasValue_OrReturnsThatOption()
        {
            var opt1 = Option<int>.Some(10);
            var opt2 = Option<int>.Some(50);
            var res = opt1.Or(opt2);
            IsTrue(res.HasValue);
            AreEqual(10, res.Value);
        }

        [Test]
        public void WhenOptionHasNoValue_OrReturnsAnotherOption()
        {
            var opt1 = Option<int>.None();
            var opt2 = Option<int>.Some(50);
            var res = opt1.Or(opt2);
            IsTrue(res.HasValue);
            AreEqual(50, res.Value);
        }

        [Test]
        public void WhenBothOptionsHaveNoValue_OrReturnsNone()
        {
            var opt1 = Option<int>.None();
            var opt2 = Option<int>.None();
            var res = opt1.Or(opt2);
            IsFalse(res.HasValue);
        }

        [Test]
        public void WhenOptionHasValue_LazyOrReturnsThatOptionAndDoesNotComputeAnotherOption()
        {
            var opt1 = Option<string>.Some("OK");
            Option<string> Opt2() => throw new InvalidOperationException();
            var res = opt1.Or(Opt2);
            IsTrue(res.HasValue);
            AreEqual("OK", res.Value);
        }

        [Test]
        public void WhenOptionHasNoValue_LazyOrComputesAndReturnsAnotherOption()
        {
            var opt1 = Option<string>.None();
            Option<string> Opt2() => Option<string>.Some("OK too");
            var res = opt1.Or(Opt2);
            IsTrue(res.HasValue);
            AreEqual("OK too", res.Value);
        }

        [Test]
        public void WhenBothOptionsHaveNoValue_LazyOrReturnsNone()
        {
            var opt1 = Option<string>.None();
            Func<Option<string>> opt2 = Option<string>.None;
            var res = opt1.Or(opt2);
            IsFalse(res.HasValue);
        }

        [Test]
        public void WhenOptionHasValue_FlattenReturnsUnderlyingOption()
        {
            var opt = Option<int>.Some(50).Into(Option<Option<int>>.Some);
            var flat = opt.Flatten();
            IsTrue(flat.HasValue);
            AreEqual(50, flat.Value);
        }

        [Test]
        public void WhenOptionHasNoValue_FlattenReturnsNone()
        {
            var opt = Option<Option<int>>.None();
            var flat = opt.Flatten();
            IsFalse(flat.HasValue);
        }

        [Test]
        public void WhenUnderlyingOptionHasNoValue_FlattenReturnsNone()
        {
            var opt = Option<int>.None().Into(Option<Option<int>>.Some);
            var flat = opt.Flatten();
            IsFalse(flat.HasValue);
        }

        [Test]
        public void WhenThereAreAnyOptionsWithValuesInCollection_ChooseReturnsThoseValuesAsAnotherCollection()
        {
            var opts = new[] { Option<int>.None(), Option<int>.Some(10), Option<int>.Some(20), Option<int>.None(), Option<int>.Some(30), Option<int>.None() };
            var chosenOpts = opts.Choose();
            CollectionAssert.AreEqual(new[] { 10, 20, 30 }, chosenOpts);
        }

        [Test]
        public void WhenThereAreNoOptionsWithValuesInCollection_ChooseReturnsEmptyCollection()
        {
            var opts = new[] { Option<bool>.None(), Option<bool>.None() };
            var chosenOpts = opts.Choose();
            CollectionAssert.IsEmpty(chosenOpts);
        }

        [Test]
        public void WhenThereAreAnyOptionsWithValuesInProjectedCollection_ChooseReturnsThoseValuesAsAnotherCollection()
        {
            var items = new[]
            {
                new { Parent = Option<int>.None() },
                new { Parent = Option<int>.Some(1) },
                new { Parent = Option<int>.None() }
            };

            var chosenOpts = items.Choose(x => x.Parent);
            CollectionAssert.AreEqual(new[] { 1 }, chosenOpts);
        }

        [Test]
        public void WhenThereAreNoOptionsWithValuesInProjectedCollection_ChooseReturnsEmptyCollection()
        {
            var items = new[]
            {
                new { Parent = Option<int>.None() },
                new { Parent = Option<int>.None() }
            };

            var chosenOpts = items.Choose(x => x.Parent);
            CollectionAssert.IsEmpty(chosenOpts);
        }

        [Test]
        public void ValueCanBeConvertedToOptionUsingSome()
        {
            var option = "hello".Some();
            AreEqual("hello", option.Value);
        }

        [Test]
        public void WhenThereIsAValue_SomeCreatesTheSameOptionAsRegularOptionSome()
        {
            var actual = 1.Some();
            var expected = Option<int>.Some(1);

            AreEqual(expected, actual);
        }

        [Test]
        public void NullCanBeConvertedToOptionUsingSome()
        {
            _ = Throws<ArgumentNullException>(() => ((string)null).Some());
        }
    }
}