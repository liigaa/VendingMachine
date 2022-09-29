using System;
using VendingMachine.Validations;

namespace VendingMachine
{
    public struct Money
    {
        public int Euros { get; set; }
        public int Cents { get; set; }

        public Money(int euro, int cents)
        {
            Euros = euro;
            Cents = cents;
        }

        public Money AddCoins(Money amount)
        {
            return new Money(Euros + amount.Euros, Cents + amount.Cents);
        }

        public Money ReduceMoney(Money amount)
        {
            return new Money(Euros - amount.Euros, Cents - amount.Cents);
        }

        public override string ToString()
        {
            return String.Format("{0:C}", Euros + Cents * 0.01);
        }
    }
}
