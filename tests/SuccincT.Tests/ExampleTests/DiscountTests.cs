using NUnit.Framework;
using SuccincT.PatternMatchers.GeneralMatcher;
using SuccincT.Unions;
using static NUnit.Framework.Assert;
using static SuccincTTests.ExampleTests.DiscountTests.Customer;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public static class DiscountTests
    {
        public enum Customer { Simple, Valuable, MostValuable }

        private class Registered
        {
            public Customer CustomerType { get; }
            public int Years { get; }

            public Registered(Customer customerType, int years)
            {
                CustomerType = customerType;
                Years = years;
            }
        }

        private class UnRegistered { }

        private static int CustomerDiscount(Customer customer) =>
            customer.Match().To<int>()
                    .With(Simple).Do(1)
                    .With(Valuable).Do(3)
                    .With(MostValuable).Do(5)
                    .Result();

        private static int YearsDiscount(int years) =>
            years.Match().To<int>()
                 .Where(y => y > 5).Do(5)
                 .Else(y => y)
                 .Result();

        private static (int customerDiscount, int yearsDiscount) AccountDiscount(Union<Registered, UnRegistered> customer) =>
            customer.Match<(int, int)>()
                    .CaseOf<Registered>().Do(c => (CustomerDiscount(c.CustomerType), YearsDiscount(c.Years)))
                    .CaseOf<UnRegistered>().Do((0, 0))
                    .Result();

        private static decimal AsPercent(int amount) => amount / 100.0m;

        private static decimal ReducePriceBy(int discount, decimal price) =>
            price - price * AsPercent(discount);

        private static decimal CalculateDicountPrice(Union<Registered, UnRegistered> account, decimal price)
        {
            var (customerDiscount, yearsDiscount) = AccountDiscount(account);
            var customerReducedPrice = ReducePriceBy(customerDiscount, price);
            return ReducePriceBy(yearsDiscount, customerReducedPrice);
        }

        [Test]
        public static void Tests()
        {
            var creator = Union.UnionCreator<Registered, UnRegistered>();
            AreEqual(94.05000m, CalculateDicountPrice(creator.Create(new Registered(MostValuable, 1)), 100.0m));
            AreEqual(92.15000m, CalculateDicountPrice(creator.Create(new Registered(Valuable, 6)), 100.0m));
            AreEqual(98.01000m, CalculateDicountPrice(creator.Create(new Registered(Simple, 1)), 100.0m));
            AreEqual(100.0m, CalculateDicountPrice(creator.Create(new UnRegistered()), 100.0m));
        }
    }
}
