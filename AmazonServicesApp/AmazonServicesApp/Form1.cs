using AmazonServicesApp.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace AmazonServicesApp
{
    public partial class Form1 : Form
    {
        private AmazonUploader uploader;
        private string bucketRootName;

        public Form1()
        {
            InitializeComponent();
            uploader = new AmazonUploader();
            bucketRootName = ConfigurationManager.AppSettings["Bucket"];
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = GetFilesForBucket();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //delete file
            int rowIndex = dataGridView1.CurrentRow.Index;
            string keyName = dataGridView1.Rows[rowIndex].Cells["FileName"].Value.ToString();
            uploader.DeleteFile(bucketRootName, keyName);
            dataGridView1.DataSource = GetFilesForBucket();
            MessageBox.Show(" The file was deleted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //upload file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select files";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                string fileName = Path.GetFileName(dialog.FileName);
                uploader.SendFileAnotherMethod(path, bucketRootName, "", fileName);
                dataGridView1.DataSource = GetFilesForBucket();
                MessageBox.Show(" The file was uploaded successfully");
            }

        }

        private List<FilesDto> GetFilesForBucket()
        {
            return uploader.GetListOfObjectForASpecificBucket(bucketRootName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //download a file
            int rowIndex = dataGridView1.CurrentRow.Index;
            string keyName = dataGridView1.Rows[rowIndex].Cells["FileName"].Value.ToString();
            uploader.DownloadFile(bucketRootName, keyName);
            MessageBox.Show(" The file was donwloaded successfully.. you can check for the data in your desktop folder ");
        }
    }
}
