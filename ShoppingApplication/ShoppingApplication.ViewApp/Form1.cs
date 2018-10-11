using ShoppingApplication.ViewApp.HttpClientServiceRetriever;
using ShoppingApplication.ViewApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingApplication.ViewApp
{
    public partial class Form1 : Form
    {
        private HttpClientService _httpClientService;
        private const string DISTRICT_API = "api/District";
        public Form1()
        {
            InitializeComponent();
            _httpClientService = new HttpClientService();

        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            await FillData();

            dataGridView1.CellMouseClick += async (send, args) =>
            {
                int rowIndex = -1;
                if (dataGridView1.CurrentRow != null)
                {
                    rowIndex = dataGridView1.CurrentRow.Index;
                    await UpdateShopsAndVendors(rowIndex);
                }
            };

        }

        private async Task FillData()
        {
            var list = await _httpClientService.GetDistricts(DISTRICT_API);
            dataGridView1.DataSource = list;  
            await UpdateShopsAndVendors(0);
        }

        private async Task UpdateShopsAndVendors(int index)
        {
            if(index == -1)
            {
                return;
            }
            if(dataGridView1.Rows != null && dataGridView1.Rows.Count > 0)
            {
                var id = dataGridView1.Rows[index].Cells["Id"].Value;
                var district = await _httpClientService.GetDistrict($"{DISTRICT_API}/{id}");

                dataGridView2.DataSource = district.Shops;
                dataGridView3.DataSource = district.Vendors;
            }
        }

        private async Task UpdateVendors()
        {
            if (dataGridView1.Rows != null && dataGridView1.Rows.Count > 0)
            {
                int rowIndex = dataGridView1.CurrentRow.Index;
                var districtId = dataGridView1.Rows[rowIndex].Cells["Id"].Value;
                var district = await _httpClientService.GetDistrict($"{DISTRICT_API}/{districtId}");
                dataGridView3.DataSource = district.Vendors;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //delete vendor
            int rowIndex = dataGridView3.CurrentRow.Index;
            var vendorId = Convert.ToInt32(dataGridView3.Rows[rowIndex].Cells["IdVendor"].Value.ToString());

            int rowIndexDistrict = dataGridView1.CurrentRow.Index;
            var districtId = Convert.ToInt32(dataGridView1.Rows[rowIndexDistrict].Cells["Id"].Value.ToString());

            var response = await _httpClientService.DeleteProductAsync($"{DISTRICT_API}/{vendorId}/{districtId}");

            MessageBox.Show(response.ToString());

            await UpdateVendors();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //add vendor
        }
    }
}
