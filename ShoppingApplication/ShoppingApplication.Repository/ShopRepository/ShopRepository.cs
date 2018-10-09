using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces.Shop;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApplication.Repository.ShopRepository
{
    public class ShopRepository : RepositoryBase<Shops>, IShopRepository
    {
        public ShopRepository(ShopingContext shopingContext) : base(shopingContext)
        {
        }
    }
}
