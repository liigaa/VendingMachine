using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine.Tests
{
    [TestClass]
    public class MoneyTests
    {
        private Money _money;

        [TestMethod]
        [DataRow(0 , 40)]
        [DataRow(1 , 40)]
        public void MoneyCreationSetCorrectly(int euro, int cents)
        {
            // Act
            _money = new Money(euro, cents);

            // Assert
            _money.Euros.Should().Be(euro);
            _money.Cents.Should().Be(cents);
        }

        [TestMethod]
        [DataRow(0, 40)]
        [DataRow(1, 40)]
        public void MoneyToStringReturnCorrectly(int euro, int cents)
        {
            // Act
            _money = new Money(euro, cents);

            // Assert
            _money.ToString().Should().Be($"{euro},{cents} €");
        }
    }
}
