using System;
using Webshop.Types;

namespace Webshop.Features.ProductRegistration
{
    public class Product
    {
        public int Number { get; }
        public Text Title { get; }
        public decimal Price { get; }
        public Text Description { get; }

        public Product(int number, Text title, decimal price, Text description) // Using a strong and specialized type (Text) avoids to replicate null checking and similar logic everywhere
        {
            if (price <= 0 || price > 1000000000) // The price could be managed better by its own specialized type, but well, this is the unique use of this invariant.
                throw new Exception("The Price must be between 1 and 1000000000.");

            Number = number; // TODO: Guard against erroneous values here, maybe creating a specialized type
            Title = title;
            Price = price;
            Description = description;
        }
    }
}