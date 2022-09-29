using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Exceptions;

namespace VendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        private VendingMachine _machine;
        private Product[] _products;

        [TestInitialize]
        public void Setup()
        {
            _products = new Product[3];
            _machine = new VendingMachine("School Toy", _products);
            _products[0] = new Product("Fanta", new Money(1, 20), 10);
            _products[1] = new Product("Sprite", new Money(1, 50), 10);
        }

        [TestMethod]
        public void VendingMachineConstructorSetCorrectly()
        {
            // Assert
            _machine.Manufacturer.Should().Be("School Toy");
            _machine.Products.Length.Should().Be(3);
        }

        [TestMethod]
        public void HasProduct_ReturnTrueIfProductsAvailable()
        {
            //Assert
            _machine.HasProducts.Should().BeTrue();
        }

        [TestMethod]
        public void HasProduct_ReturnFalseIfNoProductsAvailable()
        {
            // Arrange
            _products[0].Available = 0;
            _products[1].Available = 0;

            //Assert
            _machine.HasProducts.Should().BeFalse();
        }

        [TestMethod]
        public void AddProduct_ProductAddedToArray()
        {
            // Act
            var result = _machine.AddProduct("Pepsi", new Money(1, 50), 10);
            var freeIndex = Array.FindIndex(_products, x => x.Name == null);

            // Assert
            result.Should().BeTrue();
            freeIndex.Should().Be(-1);
        }

        [TestMethod]
        public void AddProduct_ProductNameNullOrEmpty_ThrowInvalidProductNameException()
        {
            // Act
            Action act = () => _machine.AddProduct("", new Money(1, 20), 10);
            
            // Assert
            act.Should().Throw<InvalidProductNameException>()
                .WithMessage("Product name cannot be empty");
        }
        
        [TestMethod]
        public void AddProduct_ProductAmountNullOrLessThanZero_ThrowInvalidProductAmountException()
        {
            // Act
            Action act = () => _machine.AddProduct("Fanta", new Money(1, 20), -10);

            // Assert
            act.Should().Throw<InvalidProductAmountException>()
                .WithMessage("Added amount cannot be less than zero.");
        }

        [TestMethod]
        public void AddProduct_IfNoEmptySlot_ThrowNoPlaceForNewProductException()
        {
            // Arrange
            _products[2] = new Product("Pepsi", new Money(1, 30), 10);

            // Acct
            var emptyIndex = Array.FindIndex(_products, x => x.Name == null);
            Action act = () => _machine.AddProduct("Juice", new Money(1, 50), 10);

            // Assert
            emptyIndex.Should().Be(-1);
            act.Should().Throw<NoPlaceForNewProductException>()
                .WithMessage("No free place for new product.");
        }

        [TestMethod]
        [DataRow(0,10)]
        [DataRow(0,20)]
        [DataRow(0,50)]
        [DataRow(1,00)]
        [DataRow(2,00)]
        public void AddCoins_ReturnAddedAmount(int euro, int cents)
        {
            // Arrange
            var coin = new Money(euro, cents);

            // Act
            var result = _machine.InsertCoin(coin);

            // Assert
            result.Should().Be(coin);
        }

        [TestMethod]
        [DataRow(0, 10)]
        public void AddCoins_CoinsAddedToAmount_AddToTotalAmount(int euro, int cents)
        {
            // Arrange
            var coin = new Money(euro, cents);
            _machine.InsertCoin(new Money(1, 00));

            // Act
            var result = _machine.InsertCoin(coin);

            // Assert
            result.Should().Be(new Money(1, 10));
        }

        [TestMethod]
        [DataRow(0, 30)]
        [DataRow(0, 70)]
        [DataRow(1, 10)]
        [DataRow(3, 00)]
        public void AddCoins_NotValidCoins_ThrowInvalidMoneyCoinException(int euro, int cents)
        {
            // Arrange
            var coin = new Money(euro, cents);

            // Act
            Action act = () =>_machine.InsertCoin(coin);

            // Assert
            act.Should().Throw<InvalidMoneyCoinException>()
                .WithMessage($"Coin {coin} not valid.");
        }

        [TestMethod]
        [DataRow(1, 0)]
        [DataRow(0, 50)]
        public void ReturnMoney_MoneyAmountShouldBeZero(int euro, int cents)
        {
            // Arrange
            _machine.InsertCoin(new Money(euro, cents));

            // Act
            _machine.ReturnMoney();

            //Assert
            _machine.Amount.Should().Be(new Money(0, 00));
        }

        [TestMethod]
        public void BuyProduct_ReduceAmountOfProductQuantity()
        {
            // Act
            _machine.InsertCoin(new Money(2, 00));
            _machine.BuyProduct(0);

            // Assert
            _products[0].Available.Should().Be(9);
        }

        [TestMethod]
        public void BuyProduct_ReduceTotalAmountOfMoney()
        {
            // Arrange
            var amount = new Money(0, 80);

            // Act
            _machine.InsertCoin(new Money(2, 00));
            var result = _machine.BuyProduct(0).ToString();

            // Assert
            result.Should().Be(amount.ToString());
        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(4)]
        public void BuyProduct_InvalidId_ThrowInvalidProductIdException(int productNumber)
        {
            // Act
            Action act = () => _machine.BuyProduct(productNumber);

            // Assert
            act.Should().Throw<InvalidProductNumberException>()
                .WithMessage("Wrong product number.");
        }

        [TestMethod]
        public void BuyProduct_ProductOutOfStock_ThrowProductOutOfStockException()
        {
            // Arrange
            _products[2] = new Product("Water", new Money(0, 90), 0);

            // Act
            Action act = () => _machine.BuyProduct(2);

            // Assert
            act.Should().Throw<ProductOutOfStockException>()
                .WithMessage("Product with id 2 out of stock.");
        }

        [TestMethod]
        public void BuyProduct_NotEnoughMoney_ThrowNotEnoughMoneyException()
        {
            // Act
            Action act = () => _machine.BuyProduct(0);

            // Assert
            act.Should().Throw<NotEnoughMoneyException>()
                .WithMessage("Not enough money.");
        }

        [TestMethod]
        public void UpdateProduct_UpdateProductSuccessfully_IfNotChangePrice()
        {
            // Act 
           var result = _machine.UpdateProduct(0, "Juice", null, 4);

           // Assert
           result.Should().BeTrue();
           _products[0].Name.Should().Be("Juice");
           _products[0].Price.Should().Be(new Money(1, 20));
           _products[0].Available.Should().Be(4);
        }

        [TestMethod]
        public void UpdateProduct_UpdateProductSuccessfully_IfChangePrice()
        {
            // Act 
            var result = _machine.UpdateProduct(0, "Juice", new Money(1, 40), 4);

            // Assert
            result.Should().BeTrue();
            _products[0].Name.Should().Be("Juice");
            _products[0].Price.Should().Be(new Money(1, 40));
            _products[0].Available.Should().Be(4);
        }

        [TestMethod]
        [DataRow(-2)]
        [DataRow(4)]
        public void UpdateProduct_WrongProductNumber_ThrowInvalidProductNumberException(int productNumber)
        {
            // Act
            Action act = () => _machine.UpdateProduct(productNumber, "Juice", null, 10);

            // Assert
            act.Should().Throw<InvalidProductNumberException>()
                .WithMessage("Wrong product number.");
        }

        [TestMethod]
        public void UpdateProduct_ProductNameNullOrEmpty_ThrowInvalidProductNameException()
        {
            // Act
            Action act = () => _machine.UpdateProduct(0, "", null, 10);

            // Assert
            act.Should().Throw<InvalidProductNameException>()
                .WithMessage("Product name cannot be empty");
        }
    }
}
