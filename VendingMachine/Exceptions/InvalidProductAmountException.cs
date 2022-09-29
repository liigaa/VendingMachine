using System;

namespace VendingMachine.Exceptions
{
    public class InvalidProductAmountException : Exception
    {
        public InvalidProductAmountException() : 
            base("Added amount cannot be less than zero.") { }
    }
}
