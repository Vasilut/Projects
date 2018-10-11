using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoppingApplication.ViewApp.HttpClientServiceRetriever;
using ShoppingApplication.ViewApp.Model;
using ShoppingApplication.ViewApp.Utilities;

namespace ShoppingApplication.ViewApp
{
    public partial class AddVendor : Form
    {
        private int districtId;
        private List<VendorDTO> lstOfAvailableVendors;
        private HttpClientService _httpClientService;
        private string vendorApi;

        
        public AddVendor(HttpClientService httpClientService, string vendorApi, int districtId, List<VendorDTO> lstOfAvailableVendors)
        {
            InitializeComponent();
            _httpClientService = httpClientService;
            this.vendorApi = vendorApi;
            this.districtId = districtId;
            this.lstOfAvailableVendors = lstOfAvailableVendors;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void AddVendor_Load(object sender, EventArgs e)
        {
            label4.Text = districtId.ToString();

            //setting the status
            comboBox1.DataSource = Enum.GetValues(typeof(VendorStatus));

            //setting the name of available vendors
            comboBox2.DataSource = lstOfAvailableVendors.Select(x => x.VendorName).ToList();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var status = comboBox1.SelectedValue.ToString();
            var nameOfVendor = comboBox2.SelectedValue.ToString();
            var distrId = districtId;
            var vendorIdToAddToDistrict = lstOfAvailableVendors.Where(vendor => vendor.VendorName == nameOfVendor).First().Id; //we need to check

            //make call to api to add this vendor
            //in the api we'll check if already exist a primary vendor. if not we'll add the vendor.
            var vendorDistrictDTO = new VendorDistrictDTO
            {
                IdDistrict = distrId,
                Status = status,
                IdVendor = vendorIdToAddToDistrict
            };

            var response = await _httpClientService.CreateProductAsync($"{vendorApi}", vendorDistrictDTO);

            MessageBox.Show(response.ToString());

        }
    }
}
