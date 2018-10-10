using Newtonsoft.Json;
using ShoppingApplication.ViewApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _client.BaseAddress = new Uri("http://localhost:30528/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<DistrictDTO>> GetDistrict(string path)
        {
            List<DistrictDTO> districtDTOs = new List<DistrictDTO>();
            HttpResponseMessage response = await _client.GetAsync(path);
            if(response.StatusCode  == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                districtDTOs = JsonConvert.DeserializeObject<List<DistrictDTO>>(result);
            }

            return districtDTOs;
        }
    }
}
