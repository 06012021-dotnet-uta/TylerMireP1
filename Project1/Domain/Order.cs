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
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public DateTime LastOrderDate { get; set; }
        public int TotalItems { get; set; }
        public double Total { get; set; }
    }
}
