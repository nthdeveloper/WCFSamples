using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WCFSample.WebAPI;

namespace WCFSample.WinServiceHost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            ServiceHost host = new ServiceHost(typeof(SampleAPI));
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
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WinService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
