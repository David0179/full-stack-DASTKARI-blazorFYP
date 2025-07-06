using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } // e.g. "Stripe" or "COD"
    }

}
