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
            foreach(var pair in pairs)
            {
                Assert.AreEqual(pair.x.Red, pair.y.Red);
                Assert.AreEqual(pair.x.Amber, pair.y.Amber);
                Assert.AreEqual(pair.x.Green, pair.y.Green);
                count--;
                if (count == 0) break;
            }
        }
    }
}
