using SuccincT.Unions;

namespace SuccincTTests.Examples
{
    public static class UnionMatcherExamples
    {
        public static string BoolOrIntMatcherExample(Union<int, bool> value)
        {
            return value.Match<string>()
                        .Case1().Do(i => string.Format("int={0}", i))
                        .Case2().Do(b => string.Format("bool={0}", b))
                        .Result();
        }

        public static string YesNoOrIntMatcherExample(Union<int, bool> value)
        {
            return value.Match<string>()
                        .Case1().Do(i => string.Format("int={0}", i))
                        .Case2().Of(true).Do(b => "Yes")
                        .Case2().Of(false).Do(b => "No")
                        .Result();
        }

        public static string YesNo123OrOtherIntMatcherExample(Union<int, bool> value)
        {
            return value.Match<string>()
                        .Case1().Of(1).Or(2).Or(3).Do(i => string.Format("{0} in range 1-3", i))
                        .Case1().Do(i => string.Format("int={0}", i))
                        .Case2().Of(true).Do(b => "Yes")
                        .Case2().Of(false).Do(b => "No")
                        .Result();
        }

        public static string SingleDigitOffNumberOrTrueDetectorExample(Union<int, bool> value)
        {
            return value.Match<string>()
                        .Case1().Of(0).Do(x => "0 isn't positive or negative")
                        .Case1().Where(x => x == 1 || x == 3 || x == 5 || x == 7 || x == 9).Do(x => x.ToString())
                        .Case1().Where(x => x > 9).Do(x => string.Format("{0} isn't 1 digit", x))
                        .Case1().Where(x => x < 0).Do(i => string.Format("{0} isn't positive", i))
                        .Case1().Do(x => string.Format("{0} isn't odd", x))
                        .Case2().Of(true).Do(b => "Found true")
                        .Case2().Do(b => "{0} isn't true or single odd digit.")
                        .Result();
        }

        public static string ElseExample(Union<int, bool> value)
        {
            return value.Match<string>()
                        .Case1().Of(0).Do(x => "0")
                        .Case2().Of(false).Do(x => "false")
                        .Else(x => "??")
                        .Result();
        }
    }
}
