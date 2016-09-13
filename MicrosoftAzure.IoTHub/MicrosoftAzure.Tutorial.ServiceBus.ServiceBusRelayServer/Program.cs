using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.ServiceBus;
using MicrosoftAzure.Tutorial.ServiceBus.Common;

namespace MicrosoftAzure.Tutorial.ServiceBus.ServiceBusRelayServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh = new ServiceHost(typeof(ProblemSolver));
            //sh.AddServiceEndpoint(typeof(IProblemSolver), new NetTcpBinding(), "net.tcp://localhost:9358/solver");
            //sh.AddServiceEndpoint(typeof(IProblemSolver), new NetTcpRelayBinding(), ServiceBusEnvironment.CreateServiceUri("sb", "azure-iot-tutorial", "solver"))
            //  .EndpointBehaviors.Add(new TransportClientEndpointBehavior { TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "quR0knmSPx6ZlVSnS7Wuxl3lHXDpNjLUlx4dad9TcmM=") });
            sh.Open();

            Console.WriteLine("Please press enter to close");
            Console.ReadLine();

            sh.Close();    
        }
    }
}
