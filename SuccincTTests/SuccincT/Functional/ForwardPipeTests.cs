using NUnit.Framework;
using ReversedString;
using static SuccincT.Functional.TypedLambdas;


namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class ForwardPipeTests
    {
        [Test]
        public void OneParameterPipe_GivesSameResultAsStandardWay()
        {
            var square = Lambda<int>(x => x*x);
            var toStr = Transform((int x) => x.ToString());
            var rev = Lambda<string>(x => x.Reverse());
            var result = rev(toStr(square(512)));
        }
    }
}
