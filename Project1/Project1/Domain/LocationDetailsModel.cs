using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LocationDetailsModel
    {
        public Location location { get; set; }
        public List<LocationProductInfo> productInfoes { get; set; }
        public List<int> addToOrderTotal; //To send back how many of each item to add to order
    }
}
