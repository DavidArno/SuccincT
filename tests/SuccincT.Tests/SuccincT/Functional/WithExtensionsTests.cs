using System;
using NUnit.Framework;
using SuccincT.Functional;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public class WithExtensionsTests
    {
        [Test]
        public void UpdatingWithNull_ShouldReturnSameObject()
        {
            // Arrange
            var car = new Car();

            // Act
            var optionResult = car.With<Car, Car>(null);

            // Assert
            IsFalse(optionResult.HasValue);
        }

        [Test]
        public void UpdatingWithoutProperty_ShouldReturnImmutableObject()
        {
            // Arrange
            var car = new Car();

            // Act
            var newCar = car.With(new { }).Value;

            // Assert
            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual(car.Color, newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void UpdatingWithOneProperty_ShouldReturnImmutableObjectWithUpdatedProperties()
        {
            // Arrange
            var car = new Car();

            // Act
            var newCar = car.With(new { Color = "Red" }).Value;

            // Assert
            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void UpdatingWithOnePropertyMultipleTimes_ShouldReturnImmutableObjectWithLastUpdatedProperties()
        {
            // Arrange
            var car = new Car();

            // Act
            var newCar1 = car.With(new { Color = "Red" }).Value;
            var newCar2 = newCar1.With(new { Color = "Blue" }).Value;
            var newCar3 = newCar2.With(new { Color = "Green" }).Value;

            // Assert
            AreNotSame(car, newCar3);
            AreEqual(car.Constructor, newCar3.Constructor);
            AreEqual("Green", newCar3.Color);
            AreEqual(car.CreationDate, newCar3.CreationDate);
        }

        [Test]
        public void UpdatingWithAllProperties_ShouldReturnImmutableObjectWithUpdatedProperties()
        {
            // Arrange
            var car = new Car();

            // Act
            var newCar = car.With(new
            {
                Constructor = "BMW",
                Color = "Red",
                CreationDate = new DateTime(2017, 01, 01)
            }).Value;

            // Assert
            AreNotSame(car, newCar);
            AreEqual("BMW", newCar.Constructor);
            AreEqual("Red", newCar.Color);
            AreEqual(new DateTime(2017, 01, 01), newCar.CreationDate);
        }

        [Test]
        public void UpdatingWithNoMatchingProperty_ShouldReturnImmutableObject()
        {
            // Arrange
            var car = new Car { Color = "Blue" };

            // Act
            var newCar = car.With(new { Cost = "1,000$" }).Value;

            // Assert
            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Blue", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        [Test]
        public void UpdatingNotConstructorParameter_ShouldReturnImmutableObjectWithUpdatedProperties()
        {
            // Arrange
            var book = new Book("Lewis Caroll", "Alice's Adventures In Wonderland");

            // Act
            var publishedBook = book.With(new { PublishDate = new DateTime(1865, 11, 26) }).Value;

            // Assert
            AreNotSame(book, publishedBook);
            AreEqual(book.Author, publishedBook.Author);
            AreEqual(book.Name, publishedBook.Name);
            AreEqual(new DateTime(1865, 11, 26), publishedBook.PublishDate);
        }

        [Test]
        public void UpdatingConstructorParameter_ShouldReturnImmutableObjectWithUpdatedProperties()
        {
            // Arrange
            var book = new Book("J.R.R. Tolkien", "The Lord Of The Rings");

            // Act
            var newBook = book.With(new { Name = "The Hobbit" }).Value;

            // Assert
            AreNotSame(book, newBook);
            AreEqual(book.Author, newBook.Author);
            AreEqual("The Hobbit", newBook.Name);
            AreEqual(book.PublishDate, newBook.PublishDate);
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
