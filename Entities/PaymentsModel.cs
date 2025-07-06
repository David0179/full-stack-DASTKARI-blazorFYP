using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaymentsModel
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public int OrderId { get; set; } // Foreign Key
        [Required]
        public string PaymentMethod { get; set; } = string.Empty; // Example: Credit Card, JazzCash, etc.
        [Required]
        public string PaymentStatus { get; set; } = "Pending"; // Default status
        [Required]
        public string TransactionId { get; set; } = string.Empty;
    }
}
