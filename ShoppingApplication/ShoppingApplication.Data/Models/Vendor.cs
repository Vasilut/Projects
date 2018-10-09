using System;
using System.Collections.Generic;

namespace ShoppingApplication.Data.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            VendorDistrict = new HashSet<VendorDistrict>();
        }

        public int Id { get; set; }
        public string VendorName { get; set; }

        public ICollection<VendorDistrict> VendorDistrict { get; set; }
    }
}
