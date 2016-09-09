using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace MicrosoftAzure.Tutorial.ServiceBus.SendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Endpoint=sb://azure-iot-tutorial.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=quR0knmSPx6ZlVSnS7Wuxl3lHXDpNjLUlx4dad9TcmM=";
            var queueName = "q-azure-iot-tutorial";

            var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            var message = new BrokeredMessage("This is a test message !");
            queueClient.Send(message);
        }
    }
}
