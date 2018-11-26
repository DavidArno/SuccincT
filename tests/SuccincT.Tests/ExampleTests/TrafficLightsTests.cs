using NUnit.Framework;
using System.Linq;
using static SuccincT.Examples.TrafficLights;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class TrafficLightsTests
    {
        [Test]
        public void DeclarativeAndImperativeSequences_HaveSameResults()
        {
            var pairs = SequenceTrafficLightDeclarative().Zip(SequenceTrafficLightImperative(), (x, y) => (x, y));
            var count = 10;
            foreach(var (x, y) in pairs)
            {
                Assert.AreEqual(x.Red, y.Red);
                Assert.AreEqual(x.Amber, y.Amber);
                Assert.AreEqual(x.Green, y.Green);
                count--;
                if (count == 0) break;
            }
        }
    }
}
