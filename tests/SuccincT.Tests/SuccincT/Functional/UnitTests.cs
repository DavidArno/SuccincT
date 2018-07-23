using NUnit.Framework;
using SuccincT.Functional;
using static NUnit.Framework.Assert;
using static SuccincT.Functional.Unit;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void AllUnits_AreEqual()
        {
            var unit1 = unit;
            var unit2 = new Unit();
            var unit3 = default(Unit);

            AreEqual(unit1, unit2);
            AreEqual(unit1, unit3);
            AreEqual(unit2, unit3);

            IsTrue(unit1.Equals(unit2));
            IsTrue(unit1.Equals((object)unit3));
            IsTrue(unit2.Equals((object)unit3));

            AreEqual(unit1.GetHashCode(), unit2.GetHashCode());
            AreEqual(unit1.GetHashCode(), unit3.GetHashCode());
            AreEqual(unit2.GetHashCode(), unit3.GetHashCode());

            IsTrue(unit1 == unit2);
            IsTrue(unit1 == unit3);
            IsTrue(unit2 == unit3);

            IsFalse(unit1 != unit2);
            IsFalse(unit1 != unit3);
            IsFalse(unit2 != unit3);

            AreEqual("()", unit1.ToString());
            AreEqual("()", unit2.ToString());
            AreEqual("()", unit3.ToString());
        }
    }
}
