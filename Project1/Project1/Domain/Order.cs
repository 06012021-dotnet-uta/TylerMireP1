using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderCreationDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
