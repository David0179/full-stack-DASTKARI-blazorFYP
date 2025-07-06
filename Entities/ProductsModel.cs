using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ProductsModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product image is required")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Artist name is required")]
        public string ArtistName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Artist ID is required")]
        public int ArtistId { get; set; }

        public string? ArtistProfileDescription { get; set; }

        public string? ArtistImageUrl { get; set; }

        
        public string? ArtistProfileLink { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        // Calculated property for UI display
        public bool ShouldBeAvailable => StockQuantity > 0 && IsAvailable;

        public int UserId { get; set; }
    }
}