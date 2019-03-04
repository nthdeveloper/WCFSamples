using DuplexSample.DesktopClient.SampleServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuplexSample.DesktopClient
{
    class SampleServiceCallback : ISampleServiceCallback
    {
        public void OnProductAdded(int id)
        {
            MessageBox.Show($"Prodcut added. Id:{id}");
        }

        public void OnProductDeleted(int id)
        {
            throw new NotImplementedException();
        }

        public void OnProductUpdated(int id)
        {
            throw new NotImplementedException();
        }
    }
}
