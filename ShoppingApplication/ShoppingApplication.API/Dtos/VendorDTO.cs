using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.API.Dtos
{
    public class VendorDTO
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string Status { get; set; }
    }
}
