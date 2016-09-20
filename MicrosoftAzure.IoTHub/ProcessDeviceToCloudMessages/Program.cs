using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace MicrosoftAzure.Tutorial.IoT.ProcessDeviceToCloudMessages
{
    class Program
    {
        static void Main(string[] args)
        {
            string iotHubConnectionString = "HostName=azure-iot-hub-tutorial.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=SV1bzKTvHh0MhLx/KTPhpC74VpLm7diGh42buCsdFME=";
            string iotHubD2CEndpoint = "messages/events";
            StoreEventProcessor.StorageConnectionString = "HveOBLi5lr6dH+PyozHEX+sdkuBpWzxJSryM76K4ClbgdbXoH7wRAQSPxxbCisJKX3YLOEG8h2oRZvX3JoJEdg==";
            StoreEventProcessor.ServiceBusConnectionString = "Endpoint=sb://azure-iot-tutorial.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=Z9DKlYQ3gKzEV7/Ei1usbo6wjz3e56PJFL4wQHyBf/w=;EntityPath=q-azure-iot-tutorial";

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, iotHubD2CEndpoint, EventHubConsumerGroup.DefaultGroupName, iotHubConnectionString, StoreEventProcessor.StorageConnectionString, "messages-events");
            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<StoreEventProcessor>().Wait();

            Console.WriteLine("Receiving, press enter key to stop worker");
            Console.ReadLine();

            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
