using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace MicrosoftAzure.Tutorial.EventHub.Receiver
{
    class Program
    {
        static string _eventHubName = "eventhub-tutorial";
        static string _eventHubConnectionString = "Endpoint=sb://azure-iot-tutorial-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ou2Cy9xGADctKf1TJLAGG1pcxh3l5twJtxeQzaTpSuQ=";
        static string _storageAccountName = "azureiottutorialstorage";
        static string _storageAccountKey = "k6MtKZACJ0EyrVHC+Dg/ZETGRxXaFrUKGu1rg+wHOY8dXisTsDlxqxwlrtieHKlpGH9yA5RjmM7HSi98Sv812A==";
        static string _storageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", _storageAccountName, _storageAccountKey);

        static void Main(string[] args)
        {
            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, _eventHubName, EventHubConsumerGroup.DefaultGroupName, _eventHubConnectionString, _storageConnectionString);
            Console.WriteLine("{0}> Register EventProcessor...", DateTime.Now);

            var eventProcessorOptions = new EventProcessorOptions();
            eventProcessorOptions.ExceptionReceived += (sender, e) =>
            {
                Console.WriteLine(e.Exception);
            };

            eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>(eventProcessorOptions).Wait();

            Console.WriteLine("{0}> Receiving. Press enter key to stop worker.", DateTime.Now);
            Console.ReadLine();

            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
