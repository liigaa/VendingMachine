using System;

namespace VendingMachine.Exceptions
{
    public class InvalidMoneyCoinException : Exception
    {
        public InvalidMoneyCoinException(Money amount) : 
            base($"Coin {amount} not valid.") { }
    }
}
