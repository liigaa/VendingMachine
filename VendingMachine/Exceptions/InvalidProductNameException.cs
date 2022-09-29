using System;

namespace VendingMachine.Exceptions
{
    public class InvalidProductNameException : Exception
    {
        public InvalidProductNameException() : 
            base("Product name cannot be empty") { }
    }
}
