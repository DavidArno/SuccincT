using System.Collections.Generic;
using SuccincT.Functional;

namespace SuccinctExamples
{
    public static class TrafficLights
    {
        public enum LightState
        {
            Off,
            On
        }

        public struct Lights
        {
            public LightState Red { get; }
            public LightState Amber { get; }
            public LightState Green { get; }

            public Lights(LightState red, LightState amber, LightState green)
            {
                Red = red;
                Amber = amber;
                Green = green;
            }
        }

        private enum LightsStage
        {
            Green,
            Amber,
            Red,
            RedAmber
        }

        public static IEnumerable<Lights> SequenceTrafficLightImperative()
        {
            var state = LightsStage.Green;
            while (true)
            {
                switch (state)
                {
                    case LightsStage.Green:
                        yield return new Lights(LightState.Off, LightState.Off, LightState.On);
                        state = LightsStage.Amber;
                        break;
                    case LightsStage.Amber:
                        yield return new Lights(LightState.Off, LightState.On, LightState.Off);
                        state = LightsStage.Red;
                        break;
                    case LightsStage.Red:
                        yield return new Lights(LightState.On, LightState.Off, LightState.Off);
                        state = LightsStage.RedAmber;
                        break;
                    case LightsStage.RedAmber:
                        yield return new Lights(LightState.On, LightState.On, LightState.Off);
                        state = LightsStage.Green;
                        break;
                }
            }
        }

        public static IEnumerable<Lights> SequenceTrafficLightDeclarative() =>
            new List<Lights>
            {
                new Lights(LightState.Off, LightState.Off, LightState.On),
                new Lights(LightState.Off, LightState.On, LightState.Off),
                new Lights(LightState.On, LightState.Off, LightState.Off),
                new Lights(LightState.On, LightState.On, LightState.Off)
            }.Cycle();
    }
}