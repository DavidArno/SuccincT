using System;
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
            var pairs = SequenceTrafficLightDeclarative().Zip(SequenceTrafficLightImperative(), Tuple.Create);
            var count = 10;
            foreach(var pair in pairs)
            {
                Assert.AreEqual(pair.Item1, pair.Item2);
                count--;
                if (count == 0) break;
            }
        }
    }
}
