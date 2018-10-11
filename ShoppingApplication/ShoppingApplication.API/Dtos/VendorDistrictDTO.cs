using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.API.Dtos
{
    public class VendorDistrictDTO
    {
        public int IdVendor { get; set; }
        public int IdDistrict { get; set; }
        public string Status { get; set; }
    }
}
