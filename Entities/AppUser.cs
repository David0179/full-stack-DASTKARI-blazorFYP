using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }    // from Google
        public string Email { get; set; }   // from Google
        public string Role { get; set; }    // e.g., "Customer" or "Artist"
    }
}
