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
    [Route("api/Vendor")]
    public class VendorController : Controller
    {
        private IVendorRepository _vendorRepository;
        private IVendorDistrictRepository _vendorDistrictRepository;
        private IDistrictRepository _districtRepository;

        public VendorController(IVendorRepository vendorRepository, IVendorDistrictRepository vendorDistrictRepository,
                                IDistrictRepository districtRepository)
        {
            _vendorRepository = vendorRepository;
            _vendorDistrictRepository = vendorDistrictRepository;
            _districtRepository = districtRepository;
        }

        [HttpDelete("{vendorId}/{districtId}")]
        public JsonResult Delete(int vendorId, int districtId)
        {
            //delete vendor
            //we need also the district id to make this operation
            var vendorDistrict = _vendorDistrictRepository.GetItem(vendorId, districtId);
            if (vendorDistrict.Status == VendorStatus.Primary.ToString())
            {
                return Json("The vendor that you're trying to delete is primary");
            }
            _vendorDistrictRepository.Delete(vendorDistrict);
            _vendorDistrictRepository.SaveChanges();

            return Json("The vendor from that district was deleted!");
        }

        [HttpGet("{districtId}")]
        public JsonResult GetVendors(int districtId)
        {
            //we get the vendors for a district
            //then we remove this vendors from all the vendors to see the available (remaining) vendors for a district

            var districtToReturn = _districtRepository.GetItem(districtId);
            var lstOfAllVendors = _vendorRepository.GetAll().ToList();
            var lstAvailableVendor = new List<VendorDTO>();

            //vendors
            if (districtToReturn.VendorDistrict.Count > 0)
            {
                foreach (var vendor in districtToReturn.VendorDistrict)
                {
                    var vendorFromDistrict = _vendorRepository.GetItem(vendor.IdVendor);
                    lstOfAllVendors.Remove(vendorFromDistrict);
                }

                lstAvailableVendor = BuildAvailableVendors(lstOfAllVendors);
                return Json(lstAvailableVendor);
            }

            lstAvailableVendor = BuildAvailableVendors(lstOfAllVendors);

            return Json(lstAvailableVendor);
        }

        [HttpPost]
        public JsonResult AddVendor([FromBody] VendorDistrictDTO vendor)
        {
            //add vendor
            if (ModelState.IsValid)
            {
                var vendorPrimary = _vendorDistrictRepository.GetAll().Where(vdr => vdr.IdDistrict == vendor.IdDistrict &&
                                                                            vdr.Status == VendorStatus.Primary.ToString()).FirstOrDefault();
                if(vendorPrimary != null && vendor.Status == VendorStatus.Primary.ToString())
                {
                    return Json("Cannot insert two primary vendors into the same district");
                }
                _vendorDistrictRepository.Create(new VendorDistrict
                {
                    IdDistrict = vendor.IdDistrict,
                    IdVendor = vendor.IdVendor,
                    Status = vendor.Status
                });
                _vendorDistrictRepository.SaveChanges();

                return Json("Vendor succesfully assigned to the district");
            }
            return Json("Error, the vendor was not assigned to the district");
        }

        private List<VendorDTO> BuildAvailableVendors(List<Vendor> allVendors)
        {
            var lstAvailableVendor = new List<VendorDTO>();
            foreach (var item in allVendors)
            {
                lstAvailableVendor.Add(new VendorDTO
                {
                    Id = item.Id,
                    VendorName = item.VendorName
                });
            }
            return lstAvailableVendor;
        }
    }
}