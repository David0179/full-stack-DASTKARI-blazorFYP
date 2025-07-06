using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CartItem
    {
        public ProductsModel Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
