using System;
using System.Collections.Generic;

namespace ShoppingApplication.Data.Models
{
    public partial class VendorDistrict
    {
        public int IdVendor { get; set; }
        public int IdDistrict { get; set; }
        public string Status { get; set; }

        public District IdDistrictNavigation { get; set; }
        public Vendor IdVendorNavigation { get; set; }
    }
}
