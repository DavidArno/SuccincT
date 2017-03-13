using System.Collections.Generic;
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

            var actual = 512.Into(square).Into(toStr).Into(rev);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void OneParameterPipeWithMethods_GivesSameResultAsStandardWay()
        {
            var expected = ToString(Square(512)).Reverse();
            var rev = Lambda<string>(StringReverseExtension.Reverse);

            var actual = 512.Into(Square).ToString().Into(rev);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OneActionParameterPipeWithLambdas_GivesSameResultAsStandardWay()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 1, 2, 3 };
            var removeFromList1 = Action<int>(x => list1.Remove(x));

            2.Into(removeFromList1);
            list2.Remove(2);

            CollectionAssert.AreEqual(list1, list2);
        }

        [Test]
        public void OneActionParameterPipeWithMethods_GivesSameResultAsStandardWay()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 1, 2, 3 };

            2.Into(list1.Remove);
            list2.Remove(2);

            CollectionAssert.AreEqual(list1, list2);
        }

        private static int Square(int x) => x * x;
        private static string ToString(int x) => x.ToString();
    }
}
