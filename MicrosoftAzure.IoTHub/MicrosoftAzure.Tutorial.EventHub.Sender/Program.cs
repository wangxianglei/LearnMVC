using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;

namespace MicrosoftAzure.Tutorial.EventHub.Sender
{
    class Program
    {
        static string _eventHubName = "eventhub-tutorial";
        static string _eventHubConnectionString = "Endpoint=sb://azure-iot-tutorial-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ou2Cy9xGADctKf1TJLAGG1pcxh3l5twJtxeQzaTpSuQ=";
        static void Main(string[] args)
        {
            Console.WriteLine("Please Ctrl-C to stop the sender process.");
            Console.WriteLine("Please Enter to start now.");
            Console.ReadLine();
            SendRandomMessages();
        }

        private static void SendRandomMessages()
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(_eventHubConnectionString, _eventHubName);

            while (true)
            {
                try
                {
                    var message = Guid.NewGuid().ToString();
                    Console.WriteLine("{0}> Send message: {1}", DateTime.Now, message);
                    eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}> Exception: {1}", DateTime.Now, ex.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(1000);
            }
        }
    }
}
