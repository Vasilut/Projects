﻿using Newtonsoft.Json;
using ShoppingApplication.ViewApp.Model;
using ShoppingApplication.ViewApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApplication.ViewApp.HttpClientServiceRetriever
{
    public class HttpClientService
    {
        private HttpClient _client;
        public HttpClientService()
        {
            InitClient();
        }

        private void InitClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(APIRoute.ApiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<DistrictDTO>> GetDistricts(string path)
        {
            List<DistrictDTO> districtDTOs = new List<DistrictDTO>();
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                districtDTOs = JsonConvert.DeserializeObject<List<DistrictDTO>>(result);
            }

            return districtDTOs;
        }

        public async Task<DistrictDTO> GetDistrict(string path)
        {
            var district = new DistrictDTO();
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                district = JsonConvert.DeserializeObject<DistrictDTO>(result);
            }
            return district;
        }

        public async Task<List<VendorDTO>> GetVendor(string path)
        {
            List<VendorDTO> vendorDTOs = new List<VendorDTO>();
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                vendorDTOs = JsonConvert.DeserializeObject<List<VendorDTO>>(result);
            }

            return vendorDTOs;
        }

        public async Task<string> CreateProductAsync(string path, VendorDistrictDTO vendorDistrict)
        {
            string message = string.Empty;
            var response = await _client.PostAsJsonAsync(
                path, vendorDistrict);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                message = JsonConvert.DeserializeObject<string>(result);
            }
            return message;
        }

        public async Task<string> DeleteProductAsync(string path)
        {
            string message = string.Empty;
            var response = await _client.DeleteAsync(path);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                message = JsonConvert.DeserializeObject<string>(result);
            }
            return message;
        }
    }
}
