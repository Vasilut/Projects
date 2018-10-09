using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingApplication.Repository.VendorRepository
{
    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public VendorRepository(ShopingContext shopingContext) : base(shopingContext)
        {
        }

        public override IQueryable<Vendor> GetAll()
        {
            return ShoppingContext.Vendor.Include(vend => vend.VendorDistrict);
        }

        public override Vendor GetItem(int id)
        {
            return ShoppingContext.Vendor.Include(vend => vend.VendorDistrict).Where(ven => ven.Id == id).FirstOrDefault();
        }
    }
}
