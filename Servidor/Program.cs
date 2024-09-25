using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Servicio;

namespace Servidor
{
    internal class Program
    {
        static void Main(string[] args)

        {

            using (ServiceHost LogInServiceHost = new ServiceHost(typeof(LogInService)))
            using (ServiceHost LobbyServiceHost= new ServiceHost(typeof (LobbyService)))
            {
                LogInServiceHost.Open();
                Console.WriteLine("LogInServiceHost is running");
                LobbyServiceHost.Open();
                Console.WriteLine("LobbyServiceHost is running");

                Console.WriteLine("Tamales");
                Console.ReadLine();
            }
        }
    }
}
