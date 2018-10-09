using System;
using System.Collections.Generic;

namespace ShoppingApplication.Data.Models
{
    public partial class District
    {
        public District()
        {
            Shops = new HashSet<Shops>();
            VendorDistrict = new HashSet<VendorDistrict>();
        }

        public int Id { get; set; }
        public string DistrictName { get; set; }

        public ICollection<Shops> Shops { get; set; }
        public ICollection<VendorDistrict> VendorDistrict { get; set; }
    }
}
