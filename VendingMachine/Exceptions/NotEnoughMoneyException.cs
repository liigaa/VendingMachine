using System;

namespace VendingMachine.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException() : base("Not enough money.") { }
    }
}
