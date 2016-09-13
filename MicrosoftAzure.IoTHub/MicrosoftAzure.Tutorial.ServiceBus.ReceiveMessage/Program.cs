using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure;
using Microsoft.ServiceBus;

namespace MicrosoftAzure.Tutorial.ServiceBus.ReceiveMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(connectionString, "topicTest", "subscriptionHighMessage");

            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            client.OnMessage((message) =>
            {
                try
                {
                    Console.WriteLine("\nHigh messages");
                    Console.WriteLine("Body: " + message.GetBody<string>());
                    Console.WriteLine("Message Id: " + message.MessageId);
                    Console.WriteLine("Message Number: " + message.Properties["MessageNumber"]);
                    message.Complete();
                }
                catch (Exception)
                {
                    message.Abandon();
                }
            }, options);

            namespaceManager.DeleteTopic("topicTest");
            namespaceManager.DeleteSubscription("topicTest", "subscriptionHighMessages");
        }
    }
}
