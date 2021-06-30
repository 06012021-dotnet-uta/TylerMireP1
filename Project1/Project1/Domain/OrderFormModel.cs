using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderFormModel
    {
        public List<Guid> ProductId { get; set; }
        public List<int> ItemsAdded { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}
