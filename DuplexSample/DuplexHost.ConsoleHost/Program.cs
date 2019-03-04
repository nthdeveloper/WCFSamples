using DuplexSample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DuplexHost.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(SampleService));
            try
            {
                host.Open();
                Console.WriteLine("Service started");
                Console.WriteLine("Base Addresses: ");
                for (int i = 0; i < host.BaseAddresses.Count; i++)
                {
                    Console.WriteLine("   " + host.BaseAddresses[i].AbsoluteUri);
                }

                Console.WriteLine("Press enter to stop service");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                host.Close();
            }
        }
    }
}
