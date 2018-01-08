namespace Webshop
{
    public class StoredProduct
    {
        public int Number { get; }
        public string Title { get; }
        public decimal Price { get; }

        public StoredProduct(int number, string title, decimal price)
        {
            Number = number;
            Title = title;
            Price = price;
        }
    }
}