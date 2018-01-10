using System.ComponentModel.DataAnnotations;

namespace Webshop.Features.ProductRegistration
{
    /// <summary>
    /// I usually prefer a more programatic style with some like FluentValidation
    /// because it's more declarative, but I recognize that the annotations style
    /// is more common.
    /// </summary>
    public class NewProductInputModel
    {
        [Range(1, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Range(1, 1000000000)] // A business rule replicated here indicating that no one price can exceed 1 million of dolars
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}