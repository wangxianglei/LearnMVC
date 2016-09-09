using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure;

namespace MicrosoftAzure.Tutorial.ServiceBus.ReceiveMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            var queueName = "q-azure-iot-tutorial";

            var receiveQueueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            receiveQueueClient.OnMessage(message =>
            {
                Console.WriteLine("Message body: {0}", message.GetBody<string>());
                Console.WriteLine("Message id: {0}", message.MessageId.ToString());
            });

            Console.ReadLine();
        }
    }
}
