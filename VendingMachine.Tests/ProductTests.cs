using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Tests
{
    [TestClass]
    public class ProductTests
    {
        private Product _product;

        [TestMethod]
        public void ProductCreation_Name_Price_QuantitySetCorrectly()
        {
            // Arrange
            var money = new Money(0, 90);

            // Act
            _product = new Product("Drink", money , 5);

            // Assert
            _product.Name.Should().Be("Drink");
            _product.Price.Should().Be(money);
            _product.Available.Should().Be(5);
        }

        [TestMethod]
        public void ProductToStringReturnsCorrectly()
        {
            // Arrange
            var money = new Money(0, 90);
            _product = new Product("Drink", money, 5);
            
            // Assert
            _product.ToString().Should().Be($"Product Drink, price: 0,90 €, available: 5");
        }
    }
}
