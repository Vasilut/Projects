using System;
using System.Collections.Generic;

namespace ShoppingApplication.Data.Models
{
    public partial class Shops
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public int IdDistrict { get; set; }

        public District IdDistrictNavigation { get; set; }
    }
}
