using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApplication.API.Utilities;
using ShoppingApplication.Repository.Interfaces;
using ShoppingApplication.Repository.Interfaces.Vendors;

namespace ShoppingApplication.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Vendor")]
    public class VendorController : Controller
    {
        private IVendorRepository _vendorRepository;
        private IVendorDistrictRepository _vendorDistrictRepository;

        public VendorController(IVendorRepository vendorRepository, IVendorDistrictRepository vendorDistrictRepository)
        {
            _vendorRepository = vendorRepository;
            _vendorDistrictRepository = vendorDistrictRepository;
        }

        [HttpDelete("{vendorId}/{districtId}")]
        public JsonResult Delete(int vendorId, int districtId)
        {
            var vendorDistrict = _vendorDistrictRepository.GetItem(vendorId, districtId);
            if (vendorDistrict.Status == VendorStatus.Primary.ToString())
            {
                return Json("The vendor that you're trying to delete is primary");
            }
            _vendorDistrictRepository.Delete(vendorDistrict);
            _vendorDistrictRepository.SaveChanges();

            return Json("The vendor from that district was deleted!");
        }
    }
}