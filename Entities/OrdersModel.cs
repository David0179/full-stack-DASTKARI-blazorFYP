using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrdersModel
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; } // Foreign Key
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public string Status { get; set; } = "Pending"; // Default status
    }
}
