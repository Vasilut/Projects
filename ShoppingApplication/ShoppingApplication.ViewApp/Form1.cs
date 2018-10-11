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
            UpdateShopsAndVendors();

        }

        private async Task FillData()
        {
            var list = await _httpClientService.GetDistricts(DISTRICT_API);
            dataGridView1.DataSource = list;
        }

        private async Task UpdateShopsAndVendors()
        {
            if(dataGridView1.Rows != null && dataGridView1.Rows.Count > 0)
            {
                var id = dataGridView1.Rows[0].Cells["Id"].Value;
                var district = await _httpClientService.GetDistrict($"{DISTRICT_API}/{id}");

                dataGridView2.DataSource = district.Shops;
                dataGridView3.DataSource = district.Vendors;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
