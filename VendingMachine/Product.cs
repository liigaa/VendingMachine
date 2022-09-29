
namespace VendingMachine
{
    public struct Product
    {
        ///<summary>Gets or sets the available amount of product.</summary>
        public int Available { get; set; }
        ///<summary>Gets or sets the product price.</summary>
        public Money Price { get; set; }
        ///<summary>Gets or sets the product name.</summary>
        public string Name { get; set; }

        public Product(string name, Money price, int quantity)
        {
            Name = name;
            Price = price;
            Available = quantity;
        }

        public override string ToString()
        {
            return $"Product {Name}, price: {Price}, available: {Available}";
        }
    }
}
