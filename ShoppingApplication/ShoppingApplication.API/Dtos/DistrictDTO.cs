using ShoppingApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApplication.API.Dtos
{
    public class DistrictDTO
    {
        public DistrictDTO()
        {
            Shops = new List<ShopDTO>();
            Vendors = new List<VendorDTO>();
        }

        public int Id { get; set; }
        public string DistrictName { get; set; }

        public List<ShopDTO> Shops { get; set; }
        public List<VendorDTO> Vendors { get; set; }
    }
}
