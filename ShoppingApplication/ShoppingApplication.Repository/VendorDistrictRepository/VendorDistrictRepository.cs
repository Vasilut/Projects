using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingApplication.Repository.VendorDistrictRepository
{
    public class VendorDistrictRepository : RepositoryBase<VendorDistrict>, IVendorDistrictRepository
    {
        ShopingContext _db;
        public VendorDistrictRepository(ShopingContext shopingContext) : base(shopingContext)
        {
            _db = shopingContext;
        }

        public override IQueryable<VendorDistrict> GetAll()
        {
            return ShoppingContext.VendorDistrict.Include(vdr => vdr.IdDistrictNavigation).Include(vdr => vdr.IdVendorNavigation);
        }


        public VendorDistrict GetItem(int vendorId, int districtId)
        {
            return _db.VendorDistrict.Where(vdr => vdr.IdVendor == vendorId && vdr.IdDistrict == districtId).FirstOrDefault();
        }
    }
}
