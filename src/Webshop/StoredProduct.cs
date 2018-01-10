namespace Webshop
{
    public class StoredProduct
    {
        public int Id { get; }
        public int Number { get; }
        public string Title { get; }
        public decimal Price { get; }
        public string Description { get; }

        public StoredProduct(int id, int number, string title, decimal price, string description)
        {
            Id = id;
            Number = number;
            Title = title;
            Price = price;
            Description = description;
        }
    }
}