using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.API.Dtos;
using ShoppingApplication.Repository.Interfaces.District;
using ShoppingApplication.Repository.Interfaces.Vendors;

namespace ShoppingApplication.API.Controllers
{
    [Produces("application/json")]
    [Route("api/District")]
    public class DistrictController : Controller
    {
        private IDistrictRepository _districtRepository;
        private IVendorRepository _vendorRepository;

        public DistrictController(IDistrictRepository districtRepository, IVendorRepository vendorRepository)
        {
            _districtRepository = districtRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpGet]
        public JsonResult GetDistricts()
        {
            var allDistricts = _districtRepository.GetAll().ToList();
            var districtDtoList = new List<DistrictDTO>();

            foreach (var item in allDistricts)
            {
                var districtDto = new DistrictDTO();
                districtDto.Id = item.Id;
                districtDto.DistrictName = item.DistrictName;

                //shops
                if(item.Shops.Count > 0)
                {
                    foreach (var shops in item.Shops)
                    {
                        districtDto.Shops.Add(new ShopDTO
                        {
                            Id = shops.Id,
                            ShopName = shops.ShopName
                        });
                    }
                }

                //vendors
                if(item.VendorDistrict.Count > 0)
                {
                    foreach (var vendor in item.VendorDistrict)
                    {
                        var vendorFromDistrict = _vendorRepository.GetItem(vendor.IdVendor);
                        districtDto.Vendors.Add(new VendorDTO
                        {
                            Id = vendorFromDistrict.Id,
                            Status = vendor.Status,
                            VendorName = vendorFromDistrict.VendorName
                        });
                    }
                }

                districtDtoList.Add(districtDto);
                
            }

            return Json(districtDtoList);
        }


    }
}