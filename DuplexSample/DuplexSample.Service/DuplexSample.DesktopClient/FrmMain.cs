using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuplexSample.DesktopClient
{
    public partial class FrmMain : Form
    {
        SampleServiceClient.SampleServiceClient sampleServiceClient;
        SampleServiceCallback serviceCallback;

        public FrmMain()
        {
            InitializeComponent();
            serviceCallback = new SampleServiceCallback();
            sampleServiceClient = new SampleServiceClient.SampleServiceClient(new System.ServiceModel.InstanceContext(serviceCallback));
            sampleServiceClient.Open();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var result = sampleServiceClient.Login(txtUserName.Text, txtUserName.Text);

            if (result.IsSuccess)
                MessageBox.Show("Logged in");
            else
                MessageBox.Show(result.Message);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var result = sampleServiceClient.AddProduct(new SampleServiceClient.Product()
            {
                Id = (int)numProductId.Value,
                Name=txtProdcutName.Text
            });
        }
    }
}
