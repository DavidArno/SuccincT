using NUnit.Framework;
using SuccincT.Functional;
using System;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class WithExtensionsTests
    {
        [Test]
        public void TryWithWithNullUpdateParameters_ShouldFail()
        {
            var car = new Car();
            var optionResult = car.TryWith<Car, Car>(null);

            IsFalse(optionResult.HasValue);
        }

        [Test]
        public void WithWithNullUpdateParameters_ShouldFail()
        {
            var car = new Car();
            Throws<ArgumentNullException>(() => car.With<Car, Car>(null));
        }

        [Test]
        public void TryWithWithoutProperties_ShouldReturnCopyOfOriginalObject()
        {
            var car = new Car();

            var newCar = car.TryWith(new { }).Value;

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual(car.Color, newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void WithWithoutProperties_ShouldReturnCopyOfOriginalObject()
        {
            var car = new Car();

            var newCar = car.With(new { });

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual(car.Color, newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void TryWithWithOneProperty_ShouldCopyOtherPropertiesFromOriginalObject()
        {
            var car = new Car();
            var newCar = car.TryWith(new { Color = "Red" }).Value;

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void WithWithOneProperty_ShouldCopyOtherPropertiesFromOriginalObject()
        {
            var car = new Car();
            var newCar = car.With(new { Color = "Red" });

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void TryWithUsedMultipleTimes_ShouldUseLastUpdatedPropertyForFinalObject()
        {
            var car = new Car();

            var newCar1 = car.TryWith(new { Color = "Red" }).Value;
            var newCar2 = newCar1.TryWith(new { Color = "Blue" }).Value;
            var newCar3 = newCar2.TryWith(new { Color = "Green" }).Value;

            AreNotSame(car, newCar3);
            AreEqual(car.Constructor, newCar3.Constructor);
            AreEqual("Green", newCar3.Color);
            AreEqual(car.CreationDate, newCar3.CreationDate);
        }

        [Test]
        public void WithUsedMultipleTimes_ShouldUseLastUpdatedPropertyForFinalObject()
        {
            var car = new Car();

            var newCar1 = car.With(new { Color = "Red" });
            var newCar2 = newCar1.With(new { Color = "Blue" });
            var newCar3 = newCar2.With(new { Color = "Green" });

            AreNotSame(car, newCar3);
            AreEqual(car.Constructor, newCar3.Constructor);
            AreEqual("Green", newCar3.Color);
            AreEqual(car.CreationDate, newCar3.CreationDate);
        }

        [Test]
        public void TryWithWithAllProperties_ShouldReturnObjectWithAllPropertiesUpdated()
        {
            var car = new Car { Constructor = "Ford", Color = "Black", CreationDate = new DateTime(1908, 10, 1) };

            var newCar = car.TryWith(new
            {
                Constructor = "BMW",
                Color = "Red",
                CreationDate = new DateTime(2017, 01, 01)
            }).Value;

            AreNotSame(car, newCar);
            AreEqual("BMW", newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(new DateTime(2017, 01, 01), newCar.CreationDate);
        }

        [Test]
        public void WithWithAllProperties_ShouldReturnObjectWithAllPropertiesUpdated()
        {
            var car = new Car { Constructor = "Ford", Color = "Black", CreationDate = new DateTime(1908, 10, 1) };

            var newCar = car.With(new
            {
                Constructor = "BMW",
                Color = "Red",
                CreationDate = new DateTime(2017, 01, 01)
            });

            AreNotSame(car, newCar);
            AreEqual("BMW", newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(new DateTime(2017, 01, 01), newCar.CreationDate);
        }

        [Test]
        public void TryWithWithNoMatchingProperty_ShouldReturnCopyOfOriginalObject()
        {
            var car = new Car { Color = "Blue" };
            var newCar = car.TryWith(new { Cost = "1,000$" }).Value;

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Blue", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void WithWithNoMatchingProperty_ShouldReturnCopyOfOriginalObject()
        {
            var car = new Car { Color = "Blue" };
            var newCar = car.With(new { Cost = "1,000$" });

            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Blue", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void TryWithUpdatingPropertyThatsNotHandledByConstructor_ShouldReturnNewObjectWithUpdatedProperty()
        {
            var book = new Book("Lewis Caroll", "Alice's Adventures In Wonderland");
            var publishedBook = book.TryWith(new { PublishDate = new DateTime(1865, 11, 26) }).Value;

            AreNotSame(book, publishedBook);
            AreEqual(book.Author, publishedBook.Author);
            AreEqual(book.Name, publishedBook.Name);
            AreEqual(new DateTime(1865, 11, 26), publishedBook.PublishDate);
        }

        [Test]
        public void WithUpdatingPropertyThatsNotHandledByConstructor_ShouldReturnNewObjectWithUpdatedProperty()
        {
            var book = new Book("Lewis Caroll", "Alice's Adventures In Wonderland");
            var publishedBook = book.With(new { PublishDate = new DateTime(1865, 11, 26) });

            AreNotSame(book, publishedBook);
            AreEqual(book.Author, publishedBook.Author);
            AreEqual(book.Name, publishedBook.Name);
            AreEqual(new DateTime(1865, 11, 26), publishedBook.PublishDate);
        }

        [Test]
        public void TryWithUpdatingPropertyViaConstructorParameter_ShouldReturnNewObjectWithUpdatedProperty()
        {
            var book = new Book("J.R.R. Tolkien", "The Lord Of The Rings");
            var newBook = book.TryWith(new { Name = "The Hobbit" }).Value;

            AreNotSame(book, newBook);
            AreEqual(book.Author, newBook.Author);
            AreEqual("The Hobbit", newBook.Name);
            AreEqual(book.PublishDate, newBook.PublishDate);
        }

        [Test]
        public void WithUpdatingPropertyViaConstructorParameter_ShouldReturnNewObjectWithUpdatedProperty()
        {
            var book = new Book("J.R.R. Tolkien", "The Lord Of The Rings");
            var newBook = book.With(new { Name = "The Hobbit" });

            AreNotSame(book, newBook);
            AreEqual(book.Author, newBook.Author);
            AreEqual("The Hobbit", newBook.Name);
            AreEqual(book.PublishDate, newBook.PublishDate);
        }

        [Test]
        public void TryWithThatResetsAllValues_ShouldReturnNewObjectWithResetValues()
        {
            var book = new Book("Lewis Caroll", "Alice's Adventures In Wonderland")
            {
                PublishDate = new DateTime(1865, 11, 26)
            };

            var emptyBook = book
                            .TryWith(new { Author = string.Empty, Name = string.Empty, PublishDate = default(DateTime?) })
                            .Value;

            AreNotSame(book, emptyBook);
            IsEmpty(emptyBook.Author);
            IsEmpty(emptyBook.Name);
            IsNull(emptyBook.PublishDate);
        }

        [Test]
        public void WithThatResetsAllValues_ShouldReturnNewObjectWithResetValues()
        {
            var book = new Book("Lewis Caroll", "Alice's Adventures In Wonderland")
            {
                PublishDate = new DateTime(1865, 11, 26)
            };

            var emptyBook = book.With(new
            {
                Author = string.Empty,
                Name = string.Empty,
                PublishDate = default(DateTime?)
            });

            AreNotSame(book, emptyBook);
            IsEmpty(emptyBook.Author);
            IsEmpty(emptyBook.Name);
            IsNull(emptyBook.PublishDate);
        }

        private class Car
        {
            public string Constructor { get; set; }
            public string Color { get; set; }
            public DateTime? CreationDate { get; set; }
        }

        private class Book
        {
            public Book(string author, string name) => (Author, Name) = (author, name);

            public string Author { get; }
            public string Name { get; }
            public DateTime? PublishDate { get; set; }
        }
    }
}