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
        public VendorDistrictRepository(ShopingContext shopingContext) : base(shopingContext)
        {
        }

        public override IQueryable<VendorDistrict> GetAll()
        {
            return ShoppingContext.VendorDistrict.Include(vdr => vdr.IdDistrictNavigation).Include(vdr => vdr.IdVendorNavigation);
        }




    }
}
