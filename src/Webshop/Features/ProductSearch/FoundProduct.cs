namespace Webshop.Features.ProductSearch
{
    public class FoundProduct
    {
        public int Number { get; }
        public string Title { get; }
        public decimal Price { get; }

        public FoundProduct(int number, string title, decimal price)
        {
            Number = number;
            Title = title;
            Price = price;
        }
    }
}