using NUnit.Framework;
using ReversedString;
using SuccincT.Functional;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class ForwardPipeTests
    {
        [Test]
        public void OneParameterPipeWithLambdas_GivesSameResultAsStandardWay()
        {
            var square = Lambda<int>(x => x * x);
            var toStr = Transform((int x) => x.ToString());
            var rev = Lambda<string>(x => x.Reverse());
            var expected = rev(toStr(square(512)));

            var actual = 512.PipeTo(square).PipeTo(toStr).PipeTo(rev);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void OneParameterPipeWithMethods_GivesSameResultAsStandardWay()
        {
            var expected = ToString(Square(512)).Reverse();

            var actual = 512.PipeTo(Square).PipeTo(ToString).PipeTo(StringReverseExtension.Reverse);

            Assert.AreEqual(expected, actual);
        }

        private static int Square(int x) => x * x;
        private static string ToString(int x) => x.ToString();
    }
}
