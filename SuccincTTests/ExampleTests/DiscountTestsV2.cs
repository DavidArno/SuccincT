using NUnit.Framework;
using SuccincT.PatternMatchers.GeneralMatcher;
using System.Collections.Generic;
using static NUnit.Framework.Assert;
using static SuccincTTests.ExampleTests.DiscountTestsV2.CustomerType;

namespace SuccincTTests.ExampleTests
{
    [TestFixture]
    public class DiscountTestsV2
    {
        public enum CustomerType { Unregistered, Simple, Valuable, MostValuable }

        public class Customer
        {
            public CustomerType CustomerType { get; }
            public int Years { get; }

            public Customer(CustomerType customerType, int years)
            {
                CustomerType = customerType;
                Years = customerType == Unregistered ? 0 : years;
            }
        }

        private static Dictionary<CustomerType, int> CustomerDiscountMap =
            new Dictionary<CustomerType, int>
            {
                [Unregistered] = 0,
                [Simple] = 1,
                [Valuable] = 3,
                [MostValuable] = 5
            };

        private static int YearsDiscount(int years) =>
            years.Match().To<int>()
                 .Where(y => y > 5).Do(5)
                 .Else(y => y).Result();

        private static (int typeDiscount, int yearsDiscount) AccountDiscount(Customer customer) =>
            customer.Match().To<(int, int)>()
                    .Where(c => c.CustomerType == Unregistered).Do((0, 0))
                    .Else(c => (CustomerDiscountMap[c.CustomerType], YearsDiscount(c.Years)))
                    .Result();

        private static decimal ApplyDiscount(decimal price, decimal discount) => 
            price - price * discount / 100.0m;

        private static decimal ReducePriceBy((int type, int years) discount, decimal price) => 
            ApplyDiscount(ApplyDiscount(price, discount.type), discount.years);

        public static decimal CalculateDiscountPrice(Customer account, decimal price) => 
            ReducePriceBy(AccountDiscount(account), price);

        [Test]
        public void Tests()
        {
            AreEqual(94.05000m, CalculateDiscountPrice(new Customer(MostValuable, 1), 100.0m));
            AreEqual(92.15000m, CalculateDiscountPrice(new Customer(Valuable, 6), 100.0m));
            AreEqual(98.01000m, CalculateDiscountPrice(new Customer(Simple, 1), 100.0m));
            AreEqual(100.0m, CalculateDiscountPrice(new Customer(Unregistered, 0), 100.0m));
        }
    }
}
