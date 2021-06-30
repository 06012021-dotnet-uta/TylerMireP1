using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public int TotalItems { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid CartId { get; set; }
    }
}
