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
        public Form1()
        {
            InitializeComponent();
            _httpClientService = new HttpClientService();

        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            var list = await _httpClientService.GetDistrict("api/District");
            dataGridView1.DataSource = list;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
