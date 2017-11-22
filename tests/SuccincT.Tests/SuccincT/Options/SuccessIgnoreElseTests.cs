using NUnit.Framework;
using SuccincT.Options;

namespace SuccincTTests.SuccincT.Options
{
    [TestFixture]
    public class SuccessIgnoreElseTests
    {
        [Test]
        public void ErrorWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var success = Success.CreateFailure(1);
            var result = false;
            success.Match().Success().Do(() => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }

        [Test]
        public void SuccessWithNoMatchAndIgnoreElse_DoesNothing()
        {
            var success = new Success<int>();
            var result = false;
            success.Match().Error().Do(x => result = true).IgnoreElse().Exec();
            Assert.False(result);
        }
    }
}