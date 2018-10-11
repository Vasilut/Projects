using ShoppingApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApplication.Repository.Interfaces
{
    public interface IVendorDistrictRepository : IRepositoryBase<VendorDistrict>
    {
        VendorDistrict GetItem(int vendorId, int districtId);
    }
}
