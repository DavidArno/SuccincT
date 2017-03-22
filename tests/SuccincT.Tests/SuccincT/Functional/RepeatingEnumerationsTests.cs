using System;
using NUnit.Framework;
using System.Collections.Generic;
using SuccincT.Functional;
using System.Linq;
using static System.Linq.Enumerable;
using static SuccincT.Functional.RepeatingEnumerations;
using static SuccincT.Functional.TypedLambdas;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class RepeatingEnumerationsTests
    {
        [Test]
        public void CyclingOverAnEmptyList_ReturnsEmptyEnumeration()
        {
            var result = new List<int>().Cycle();
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void CyclingOverList_RepeatsValues()
        {
            var list = new List<int> {1, 2};
            var result = new List<int>();
            foreach (var item in list.Cycle())
            {
                result.Add(item);
                if (result.Count == 6) break;
            }

            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(1, result[2]);
            Assert.AreEqual(2, result[3]);
            Assert.AreEqual(1, result[4]);
            Assert.AreEqual(2, result[5]);
        }

        [Test]
        public void CyclingEnumeration_EnumeratesJustOnceButStillRepeats()
        {
            var enumerationEndCount = 0;
            var enumerationEndNotification = Action(() => enumerationEndCount++);
            var results = new List<string>();
            foreach(var item in EnumerationWithNotificationOfEnd(enumerationEndNotification).Cycle())
            {
                results.Add(item);
                if (results.Count == 7) break;
            }

            Assert.AreEqual(1, enumerationEndCount);
            Assert.AreEqual("red", results[0]);
            Assert.AreEqual("green", results[1]);
            Assert.AreEqual("blue", results[2]);
            Assert.AreEqual("red", results[3]);
            Assert.AreEqual("green", results[4]);
            Assert.AreEqual("blue", results[5]);
            Assert.AreEqual("red", results[6]);
        }

        [Test]
        public void FizzBuzzUsingCycle_GeneratesCorrectSequence()
        {
            var fizzes = Cycle("", "", "Fizz");
            var buzzes = Cycle("", "", "", "", "Buzz");
            var words = fizzes.Zip(buzzes, (f, b) => f + b);
            var numbers = Range(1, 100);
            var fizzBuzz = numbers.Zip(words, (n, w) => w == "" ? n.ToString() : w).ToList();

            Assert.AreEqual("1", fizzBuzz[0]);
            Assert.AreEqual("2", fizzBuzz[1]);
            Assert.AreEqual("Fizz", fizzBuzz[2]);
            Assert.AreEqual("4", fizzBuzz[3]);
            Assert.AreEqual("Buzz", fizzBuzz[4]);
            Assert.AreEqual("Fizz", fizzBuzz[5]);
            Assert.AreEqual("7", fizzBuzz[6]);
            Assert.AreEqual("14", fizzBuzz[13]);
            Assert.AreEqual("FizzBuzz", fizzBuzz[14]);
            Assert.AreEqual("Buzz", fizzBuzz[99]);
        }

        [Test]
        public void FizzBuzz_GeneratesCorrectSequence()
        {
            var fizzBuzz = new List<string>();
            for (var i = 1; i <= 100; i++)
            {
                var entry = "";
                if (i % 3 == 0)
                {
                    entry += "Fizz";
                }
                if (i % 5 == 0)
                {
                    entry += "Buzz";
                }
                if (entry == "")
                {
                    entry = i.ToString();
                }
                fizzBuzz.Add(entry);
            }
            //var fizzes = new List<string> { "", "", "Fizz" }.Cycle();
            //var buzzes = new List<string> { "", "", "", "", "Buzz" }.Cycle();
            //var words = fizzes.Zip(buzzes, (f, b) => f + b);
            //var numbers = Range(1, 100);
            //var fizzBuzz = numbers.Zip(words, (n, w) => w == "" ? n.ToString() : w).ToList();

            Assert.AreEqual("1", fizzBuzz[0]);
            Assert.AreEqual("2", fizzBuzz[1]);
            Assert.AreEqual("Fizz", fizzBuzz[2]);
            Assert.AreEqual("4", fizzBuzz[3]);
            Assert.AreEqual("Buzz", fizzBuzz[4]);
            Assert.AreEqual("Fizz", fizzBuzz[5]);
            Assert.AreEqual("7", fizzBuzz[6]);
            Assert.AreEqual("14", fizzBuzz[13]);
            Assert.AreEqual("FizzBuzz", fizzBuzz[14]);
            Assert.AreEqual("Buzz", fizzBuzz[99]);
        }

        private static IEnumerable<string> EnumerationWithNotificationOfEnd(Action endReached)
        {
            yield return "red";
            yield return "green";
            yield return "blue";
            endReached();
        }
    }
}
