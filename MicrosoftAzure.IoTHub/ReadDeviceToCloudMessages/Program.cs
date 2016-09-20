using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;

namespace MicrosoftAzure.Tutorial.IoT.ReadDeviceToCloudMessages
{
    class Program
    {
        static string connectionString = "HostName=AzureIoTHubWXL.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=wGptxByn6vS7lvxsvWgsq5TwJKESgxQOv8XEcEOEZ6w=";
        static string iotHubD2CEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Receive messages. Ctrl - C to exist \n");

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2CEndpoint);
            var deviceToCloudPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tokenSource.Cancel();
                Console.WriteLine("Existing...");
            };

            var tasks = new List<Task>();
            foreach (string partition in deviceToCloudPartitions)
            {
                tasks.Add(ReceivedMessagesFromDeviceAsync(partition, tokenSource.Token));
            }

            Task.WaitAll(tasks.ToArray());
        }

        private static async Task ReceivedMessagesFromDeviceAsync(string partition, CancellationToken token)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;

                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null)
                    continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());

                Console.WriteLine("Message Received, Partition: {0}, Data: {1}", partition, data);
            }

        }
    }
}
