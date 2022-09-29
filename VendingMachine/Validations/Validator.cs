using VendingMachine.Exceptions;

namespace VendingMachine.Validations
{
    public static class Validator
    {
        public static void ProductNameIsNotNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidProductNameException();
            }
        }

        public static void ProductAmountLessThanZero(int amount)
        {
            if (amount < 0)
            {
                throw new InvalidProductAmountException();
            }
        }

        public static void ProductIdOutOfRange(int id, Product[] product)
        {
            if (id < 0 || id >= product.Length)
            {
                throw new InvalidProductNumberException();
            }
        }

        public static void ProductOutOfStock(int id, Product[] product)
        {
            if (product[id].Available == 0)
            {
                throw new ProductOutOfStockException(id);
            }
        }

        public static void IsEnoughMoney(double price, double money)
        {
            if (price > money)
            {
                throw new NotEnoughMoneyException();
            }
        }
    }
}
