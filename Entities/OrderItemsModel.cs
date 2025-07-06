using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderItemsModel
    {
        [Required]
        public int OrderItemId { get; set; }
        [Required]
        public int OrderId { get; set; } // Foreign Key
        [Required]
        public int ProductId { get; set; } // Foreign Key
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal PriceAtPurchase { get; set; }
    }
}
