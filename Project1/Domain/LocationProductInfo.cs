using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class LocationProductInfo
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public Guid ProductId { get; set; }
        public double SaleDiscount { get; set; }
        public int ItemsPerOrder { get; set; }
        public int TotalItems { get; set; }
    }
}
