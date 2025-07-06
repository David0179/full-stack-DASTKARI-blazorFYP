using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ArtistDropdownModel
    {
            public int Id { get; set; } // User ID
        public int ArtistId { get; set; } // Artist ID
        public string Name { get; set; }
            public string ProfileLink { get; set; }
            public string? Description { get; set; }
        public string? About { get; set; }
        public string? ImageUrl { get; set; }
        public string? workImage{ get; set; }
        public string? Location { get; set; }
    }
}

