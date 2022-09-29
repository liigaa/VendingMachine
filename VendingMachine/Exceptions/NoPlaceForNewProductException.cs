using System;

namespace VendingMachine.Exceptions
{
    public class NoPlaceForNewProductException : Exception
    {
        public NoPlaceForNewProductException() : 
            base("No free place for new product.") { }
    }
}
