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
        public Form1()
        {
            InitializeComponent();
            HttpClientService httpClientService = new HttpClientService();
            var list = httpClientService.GetDistrict("api/District").Result;
            dataGridView1.DataSource = new List<DistrictDTO>() { new DistrictDTO { Id = 1, DistrictName = "AAA" } };

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
