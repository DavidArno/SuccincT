using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SuccincT.Functional;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class ConsEnumerableTests
    {
        [Test]
        public void ListConvertedToCons_CanBeEnumerated()
        {
            var list = new List<int> { 1, 2, 3 };
            var consList = list.ToConsEnumerable();
            var count = consList.Aggregate((x, y) => x + y);
            AreEqual(6, count);
        }

        [Test]
        public void EmptyListConvertedToCons_CanBeEnumerated()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var list = new List<int>();
            var consList = list.ToConsEnumerable();
            var count = consList.Count();
            AreEqual(0, count);
        }

        [Test]
        public void EnumerationConvertedToCons_CanBeEnumeratedManyTimesWithoutReRunningOriginalEnumeration()
        {
            var enumRunCount = 0;
            var cons = EnumerationWithNotificationOfEnd(() => enumRunCount++).ToConsEnumerable();
            var joinedString1 = cons.Aggregate((x, y) => x + y);
            var joinedString2 = cons.Aggregate((x, y) => x + y);
            AreEqual("redgreenblue", joinedString1);
            AreEqual("redgreenblue", joinedString2);
            AreEqual(1, enumRunCount);
        }

        [Test]
        public void ItemAddedToList_ResultsInItAndOriginalListAllBeingEnumerated()
        {
            var list = new List<int> { 2, 3, 4 };
            var consList = list.Cons(1);
            var count = consList.Aggregate((x, y) => x + y);
            AreEqual(10, count);
        }

        [Test]
        public void ItemAddedToEmptyList_ResultsInItBeingEnumerated()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var list = new List<int>();
            var consList = list.Cons(1);
            var count = consList.Aggregate((x, y) => x + y);
            AreEqual(1, count);
        }

        [Test]
        public void ManyItemsAddedToList_ResultsInThemAndOriginalListAllBeingEnumerated()
        {
            var list = new List<int> { 4, 5, 6 };
            var consList1 = list.Cons(3);
            var consList2 = consList1.Cons(2);
            var consList3 = consList2.Cons(1);
            var count = consList3.Aggregate((x, y) => x + y);
            AreEqual(21, count);
        }

        [Test]
        public void EnumerationConvertedToConsAndItemsAdded_CanBeEnumeratedManyTimesWithoutReRunningOriginalEnumeration()
        {
            var enumRunCount = 0;
            var cons1 = EnumerationWithNotificationOfEnd(() => enumRunCount++).ToConsEnumerable();
            var cons2 = cons1.Cons("yellow");
            var joinedString1 = cons2.Aggregate((x, y) => x + y);
            var joinedString2 = cons2.Aggregate((x, y) => x + y);
            AreEqual("yellowredgreenblue", joinedString1);
            AreEqual("yellowredgreenblue", joinedString2);
            AreEqual(1, enumRunCount);
        }

        [Test]
        public void WhenListsAndItemsAddedToList_ResultsInThemAllBeingEnumerated()
        {
            var list1 = new List<int> { 1, 2 };
            var list2 = new List<int> { 3, 4 };
            var consList1 = list1.Cons(new List<int> { 5, 6 });
            var consList2 = list2.Cons(7);
            var consList3 = consList2.Cons(new List<int> { 8, 9 });
            var consList4 = consList1.Cons(consList3);
            var count = consList4.Aggregate((x, y) => x + y);
            AreEqual(45, count);
        }

        [Test]
        public void SplittingEnumerationViaCons_DoesntFullyEnumerateEnumeration()
        {
            var enumRunCount = 0;
            var (_, _) = EnumerationWithNotificationOfEnd(() => enumRunCount++);
            AreEqual(0, enumRunCount);
        }

        [Test]
        public void SplittingEnumerationAsTuple_AllowsRemainingEnumerationToBeEnumeratedAndHeadCorrectValue()
        {
            var (head, tail) = new List<int> { 1, 2, 3, 4 };
            var count = tail.Aggregate((x, y) => x + y);
            AreEqual(1, head);
            AreEqual(9, count);
        }

        [Test]
        public void SplittingEnumerationViaCons_AllowsRemainingEnumerationToBeEnumeratedAndHeadCorrectValue()
        {
            var consData = new List<int> { 1, 2, 3, 4 }.ToConsEnumerable().Cons();
            var count = consData.Tail.Aggregate((x, y) => x + y);
            AreEqual(1, consData.Head.Value);
            AreEqual(9, count);
        }

        [Test]
        public void SplittingOneElementEnumerationToTuple_GivesEmptyTail()
        {
            var (head, tail) = new List<int> { 1 };
            var count = tail.Count();
            AreEqual(1, head);
            AreEqual(0, count);
        }

        [Test]
        public void SplittingOneElementEnumerationViaCons_GivesEmptyTail()
        {
            var consData = new List<int> { 1 }.ToConsEnumerable().Cons();
            var count = consData.Tail.Count();
            AreEqual(1, consData.Head.Value);
            AreEqual(0, count);
        }

        [Test]
        public void SplittingEmptyEnumerationViaCons_GivesEmptyTailAndNoneForHead()
        {
            var consData = new List<int>().ToConsEnumerable().Cons();
            var count = consData.Tail.Count();
            IsFalse(consData.Head.HasValue);
            AreEqual(0, count);
        }

        [Test]
        public void SplittingEmptyEnumerationIntoTuple_ThrowsException() =>
            Throws<InvalidOperationException>(() => { var (_, _) = new List<int>(); });

        [Test]
        public void SplittingEnumerationViaCons_AllowsAllowsOriginalEnumerationToBeEnumeratedCorrectly()
        {
            var consList = new List<int> { 1, 2, 3, 4 }.ToConsEnumerable();
            var consData = consList.Cons();
            var count1 = consData.Tail.Aggregate((x, y) => x + y);
            var count2 = consList.Aggregate((x, y) => x + y);
            AreEqual(9, count1);
            AreEqual(10, count2);
        }

        [Test]
        public void MultipleEnumerationConsEnumerable_CanBeCorrectlyReadViaCons()
        {
            var consList1 = new List<int> { 3, 4 }.ToConsEnumerable();
            var consList2 = consList1.Cons(new[] { 1, 2 });
            var data1 = consList2.Cons();
            var data2 = data1.Tail.Cons();
            var data3 = data2.Tail.Cons();
            var data4 = data3.Tail.Cons();
            var data5 = data4.Tail.Cons();

            AreEqual(1, data1.Head.Value);
            AreEqual(2, data2.Head.Value);
            AreEqual(3, data3.Head.Value);
            AreEqual(4, data4.Head.Value);
            IsFalse(data5.Head.HasValue);
        }

        [Test]
        public void MultipleEnumerationConsEnumerable_CanBeCorrectlyReadViaDeconstruct()
        {
            var consList1 = new List<int> { 3, 4 }.ToConsEnumerable();
            var consList2 = consList1.Cons(new[] { 1, 2 });
            var (head1, (head2, (head3, (head4, tail)))) = consList2;

            AreEqual(1, head1);
            AreEqual(2, head2);
            AreEqual(3, head3);
            AreEqual(4, head4);
            AreEqual(0, tail.Count());
        }

        [Test]
        public void ForEach_WorksWithConsEnumerable()
        {
            var enumRunCount = 0;
            var result = "";
            var cons = EnumerationWithNotificationOfEnd(() => enumRunCount++).ToConsEnumerable();

            for (var i = 0; i < 10; i++)
            {
                foreach (var item in cons)
                {
                    result += item.Substring(0, 1);
                }

            }

            AreEqual(1, enumRunCount);
            AreEqual("rgbrgbrgbrgbrgbrgbrgbrgbrgbrgb", result);
        }

        [Test]
        public void Sum_WorksWithMultipleConsEnumerable()
        {
            var consList1 = new List<int> {3, 4}.ToConsEnumerable();
            var consList2 = consList1.Cons(new[] {1, 2});
            var result = consList2.Sum();
            AreEqual(10, result);
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
