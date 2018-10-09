using Microsoft.EntityFrameworkCore;
using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces.District;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShoppingApplication.Repository.DistrictRepository
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(ShopingContext shopingContext) : base(shopingContext)
        {
        }

        public override IQueryable<District> GetAll()
        {
            return ShoppingContext.District.Include(shop => shop.Shops).Include(vendors => vendors.VendorDistrict);
        }

        public override District GetItem(int id)
        {
            return ShoppingContext.District.Include(shop => shop.Shops).Include(vendors => vendors.VendorDistrict).Where(dsr => dsr.Id == id).FirstOrDefault();
        }

        public override IEnumerable<District> FindByCondition(Expression<Func<District, bool>> expression)
        {
            return ShoppingContext.District.Where(expression).ToList();
        }
    }
}
