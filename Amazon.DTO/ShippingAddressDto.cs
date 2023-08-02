using Amazon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.DTO
{
    public class ShippingAddressDto
    {
        public int id { get; set; }
        public string street { get; set; }
        public string buildname { get; set; }
        public string userid { get; set; }
        
        public string? cityname { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
