using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.DistrictRepository;
using ShoppingApplication.Repository.Interfaces.District;
using ShoppingApplication.Repository.Interfaces.Shop;
using ShoppingApplication.Repository.Interfaces.Vendors;
using ShoppingApplication.Repository.ShopRepository;
using ShoppingApplication.Repository.VendorRepository;
using System;
using System.Linq;

namespace ShoppingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ShopingContext();
            IDistrictRepository districtRepository = new DistrictRepository(db);
            IVendorRepository vendorRepository = new VendorRepository(db);
            IShopRepository shopRepository = new ShopRepository(db);

            var allDistrict = districtRepository.GetAll().ToList();


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
