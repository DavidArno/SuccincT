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
            var newCar = car.With<Car, Car>(null);

            // Assert
            AreSame(car, newCar);
        }

        [Test]
        public void UpdatingWithoutProperty_ShouldReturnImmutableObject()
        {
            // Arrange
            var car = new Car();

            // Act
            var newCar = car.With(new { });

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
            var newCar = car.With(new { Color = "Red" });

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
            var newCar1 = car.With(new { Color = "Red" });
            var newCar2 = newCar1.With(new { Color = "Blue" });
            var newCar3 = newCar2.With(new { Color = "Green" });

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
            });

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
            var newCar = car.With(new { Cost = "1,000$" });

            // Assert
            AreNotSame(car, newCar);
            AreEqual(car.Constructor, newCar.Constructor);
            AreEqual("Blue", newCar.Color);
            AreEqual(car.CreationDate, newCar.CreationDate);
        }

        private class Car
        {
            public string Constructor { get; set; }
            public string Color { get; set; }
            public DateTime? CreationDate { get; set; }
        }
    }
}
