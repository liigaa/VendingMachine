using System;
using System.Linq;
using VendingMachine.Exceptions;
using VendingMachine.Validations;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        public string Manufacturer { get; }

        public bool HasProducts => Array.Exists(Products, product => product.Available > 0); 

        public Money Amount { get; private set; }

        public Product[] Products { get; set; }

        public VendingMachine(string manufacturer, Product[] products)
        {
            Manufacturer = manufacturer;            
            Products = products;
        }

        public bool AddProduct(string name, Money price, int count)
        {
            Validator.ProductNameIsNotNullOrEmpty(name);
            Validator.ProductAmountLessThanZero(count);

            var emptyIndex = Array.FindIndex(Products, product => product.Name == null);

            if (emptyIndex == -1)
            {
                throw new NoPlaceForNewProductException();
            }

            Products[emptyIndex] = new Product(name, price, count);
            Console.WriteLine($"Product {name} added successfully");
            return true;
        }        

        public Money InsertCoin(Money amount)
        {
            //valid coins 0.10, 0.20, 0.50, 1.00, 2.00
            string[] acceptedCoins = { "0,10 €", "0,20 €", "0,50 €", "1,00 €", "2,00 €" };

            if (!acceptedCoins.Contains(amount.ToString()))
            {
                throw new InvalidMoneyCoinException(amount);
            }

            Amount = Amount.AddCoins(amount);
            Console.WriteLine($"You added {amount.Euros},{amount.Cents}");
            return Amount;
        }

        public Money BuyProduct(int productId)
        {
            Validator.ProductIdOutOfRange(productId, Products);
            Validator.ProductOutOfStock(productId, Products);

            var price = Products[productId].Price.Euros + Products[productId].Price.Cents*0.01;
            var totalMoney = Amount.Euros + Amount.Cents*0.01;
            
            Validator.IsEnoughMoney(price, totalMoney);
             
            Console.WriteLine($"{Products[productId].Name} bought successfully");
            Products[productId].Available--;                        
            Amount = Amount.ReduceMoney(Products[productId].Price);
            return Amount;
        }

        public Money ReturnMoney()
        {
            Console.WriteLine("Your refund: " + Amount.ToString());
            Amount = new Money();
            return Amount;            
        }

        public bool UpdateProduct(int productNumber, string name, Money? price, int amount)
        {
            Validator.ProductIdOutOfRange(productNumber, Products);
            Validator.ProductNameIsNotNullOrEmpty(name);

            Products[productNumber].Name = name;
            Products[productNumber].Available = amount;

            if (price.HasValue)
            {
                Products[productNumber].Price = (Money)price;
            }

            return true;
        }

        //public void PrintAllProduct()
        //{
        //    for (var i = 0; i < Products.Length; i++)
        //    {
        //        Console.WriteLine($"Product id = {i}, {Products[i]}" );
        //    }
        //}
    }
}
