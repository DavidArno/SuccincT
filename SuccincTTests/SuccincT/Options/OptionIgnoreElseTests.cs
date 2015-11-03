using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class OptionIgnoreElseTests
    {
        [Test]
        public void OptionSomeWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var option = Option<bool>.Some(true);
            var result = false;
            option.Match().None().Do(() => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }

        [Test]
        public void OptionNoneWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var option = Option<bool>.None();
            var result = false;
            option.Match().Some().Do(x => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }

        [Test]
        public void ValueOrErrorWithValueWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var value = ValueOrError.WithValue("test");
            var result = false;
            value.Match().Error().Do(x => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }

        [Test]
        public void ValueOrErrorWithErrorWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var value = ValueOrError.WithError("error");
            var result = false;
            value.Match().Value().Do(x => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }
    }
}