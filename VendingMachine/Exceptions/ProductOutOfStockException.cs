using System;

namespace VendingMachine.Exceptions
{
    public class ProductOutOfStockException : Exception
    {
        public ProductOutOfStockException(int id) : 
            base($"Product with id {id} out of stock.") { }
    }
}
