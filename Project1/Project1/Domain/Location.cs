using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }
    }
}
