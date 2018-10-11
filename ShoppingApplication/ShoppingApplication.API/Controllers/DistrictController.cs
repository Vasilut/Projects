using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.API.Dtos;
using ShoppingApplication.API.Utilities;
using ShoppingApplication.Data.Models;
using ShoppingApplication.Repository.Interfaces;
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
        private IVendorDistrictRepository _vendorDistrictRepository;

        public DistrictController(IDistrictRepository districtRepository, IVendorRepository vendorRepository,
                                  IVendorDistrictRepository vendorDistrictRepository)
        {
            _districtRepository = districtRepository;
            _vendorRepository = vendorRepository;
            _vendorDistrictRepository = vendorDistrictRepository;
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
                BuildExtraPropertyForDistrict(item, ref districtDto);
                districtDtoList.Add(districtDto);

            }

            return Json(districtDtoList);
        }

        [HttpGet("{id}")]
        public JsonResult GetDistrict(int id)
        {
            var districtToReturn = _districtRepository.GetItem(id);
            if (districtToReturn == null)
            {
                return Json(null);
            }

            var districtDTO = new DistrictDTO();
            districtDTO.Id = districtToReturn.Id;
            districtDTO.DistrictName = districtToReturn.DistrictName;
            BuildExtraPropertyForDistrict(districtToReturn, ref districtDTO);

            return Json(districtDTO);


        }

        [HttpDelete("{vendorId}/{districtId}")]
        public JsonResult Delete(int vendorId, int districtId)
        {
            var vendorDistrict = _vendorDistrictRepository.GetItem(vendorId, districtId);
            if(vendorDistrict.Status == VendorStatus.Primary.ToString())
            {
                return Json("The vendor that you're trying to delete is primary");
            }
            _vendorDistrictRepository.Delete(vendorDistrict);
            _vendorDistrictRepository.SaveChanges();

            return Json("The vendor from that district was deleted!");
        }

        private void BuildExtraPropertyForDistrict(District district, ref DistrictDTO dtoDistrict)
        {
            //shops
            if (district.Shops.Count > 0)
            {
                foreach (var shops in district.Shops)
                {
                    dtoDistrict.Shops.Add(new ShopDTO
                    {
                        Id = shops.Id,
                        ShopName = shops.ShopName
                    });
                }
            }

            //vendors
            if (district.VendorDistrict.Count > 0)
            {
                foreach (var vendor in district.VendorDistrict)
                {
                    var vendorFromDistrict = _vendorRepository.GetItem(vendor.IdVendor);
                    dtoDistrict.Vendors.Add(new VendorDTO
                    {
                        Id = vendorFromDistrict.Id,
                        Status = vendor.Status,
                        VendorName = vendorFromDistrict.VendorName
                    });
                }
            }
        }


    }
}