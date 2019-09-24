using NUnit.Framework;
using SuccincT.Functional;
using System;
using System.Diagnostics.CodeAnalysis;
using static NUnit.Framework.Assert;

namespace SuccincTTests.SuccincT.Functional
{
    [TestFixture]
    public static class CopyExtensionsTests
    {
        [Test]
        public static void TryCopyForTypeWithNoConstructor_ShouldReturnCopyOfOriginalObjectUsingProperties()
        {
            var car = new Car {Color = "red", Constructor = "Ford", CreationDate = DateTime.Now};
            var newCar = car.TryCopy().Value;

            Multiple(
                () => {
                    AreNotSame(car, newCar);
                    AreEqual(car.Constructor, newCar.Constructor);
                    AreEqual(car.Color, newCar.Color);
                    AreEqual(car.CreationDate, newCar.CreationDate);
                });
        }

        [Test]
        public static void TryCopyForTypeWithConstructor_ShouldReturnCopyOfOriginalObject()
        {
            var book = new Book("John Smith", "Common English Names") {PublishDate = DateTime.Now};
            var newBook = book.TryCopy().Value;

            Multiple(
                () => {
                    AreNotSame(book, newBook);
                    AreEqual(book.Author, newBook.Author);
                    AreEqual(book.Name, newBook.Name);
                    AreEqual(book.PublishDate, newBook.PublishDate);
                });
        }

        [Test]
        public static void TryCopyForTypeWithConstructorAndNoGetters_ShouldReturnNone()
        {
            var testObject = new TypeWithConstructorAndNoGetters(1);
            var newObject = testObject.TryCopy();

            IsFalse(newObject.HasValue);
        }

        [Test]
        public static void TryCopyForTypeWithConstructorAndNoMatchingGetter_ShouldReturnNone()
        {
            var testObject = new TypeWithConstructorAndNoMatchingGetter(1);
            var newObject = testObject.TryCopy();

            IsFalse(newObject.HasValue);
        }

        [Test]
        public static void CopyForTypeWithNoConstructor_ShouldReturnCopyOfOriginalObjectUsingProperties()
        {
            var car = new Car { Color = "red", Constructor = "Ford", CreationDate = DateTime.Now };
            var newCar = car.Copy();

            Multiple(
                () => {
                    AreNotSame(car, newCar);
                    AreEqual(car.Constructor, newCar.Constructor);
                    AreEqual(car.Color, newCar.Color);
                    AreEqual(car.CreationDate, newCar.CreationDate);
                });
        }


        [Test]
        public static void CopyForTypeWithConstructor_ShouldReturnCopyOfOriginalObject()
        {
            var book = new Book("John Smith", "Common English Names") { PublishDate = DateTime.Now };
            var newBook = book.Copy();

            Multiple(
                () => {
                    AreNotSame(book, newBook);
                    AreEqual(book.Author, newBook.Author);
                    AreEqual(book.Name, newBook.Name);
                    AreEqual(book.PublishDate, newBook.PublishDate);
                });
        }

        [Test]
        public static void CopyWithoutProperties_ShouldReturnCopyOfOriginalObject()
        {
            var car = new Car();
            var newCar = car.Copy();

            Multiple(
                () => {
                    AreNotSame(car, newCar);
                    AreEqual(car.Constructor, newCar.Constructor);
                    AreEqual(car.Color, newCar.Color);
                    AreEqual(car.CreationDate, newCar.CreationDate);
                });
        }

        [Test]
        public static void CopyForTypeWithConstructorAndNoGetters_ShouldThrow()
        {
            var testObject = new TypeWithConstructorAndNoGetters(1);

            _ = Throws<CopyException>(() => testObject.Copy());
        }

        [Test]
        public static void CopyForTypeWithNoPublicConstructor_ShouldThrow()
        {
            var testObject = new TypeWithNoPublicConstructor(1);

            _ = Throws<CopyException>(() => testObject.Copy());
        }

        [Test]
        public static void CopyForTypeWithConstructorAndNoMatchingGetter_ShouldReturnNone()
        {
            var testObject = new TypeWithConstructorAndNoMatchingGetter(1);

            _ = Throws<CopyException>(() => testObject.Copy());
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

        private class TypeWithConstructorAndNoGetters
        {
            // ReSharper disable once UnusedParameter.Local
            public TypeWithConstructorAndNoGetters(int x) { }
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private class TypeWithConstructorAndNoMatchingGetter
        {
            public TypeWithConstructorAndNoMatchingGetter(int a, int c) { }
            public TypeWithConstructorAndNoMatchingGetter(int a) => B = a;
            public int B { get; }
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class TypeWithNoPublicConstructor
        {
            internal TypeWithNoPublicConstructor(int a) => A = a;
            public int A { get; }
        }
    }
}