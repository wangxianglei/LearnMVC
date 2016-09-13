using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.Azure;
using Microsoft.ServiceBus;

namespace MicrosoftAzure.Tutorial.ServiceBus.SendMessage
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        //    var queueName = "q-azure-iot-tutorial";

        //    var queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
        //    var message = new BrokeredMessage("This is a test message !");
        //    queueClient.Send(message);
        //}

        static void Main(string[] args)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            TopicDescription topicDesc = new TopicDescription("topicTest");
            topicDesc.MaxSizeInMegabytes = 5120;
            topicDesc.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);

            if (!namespaceManager.TopicExists("topicTest"))
            {
                namespaceManager.CreateTopic(topicDesc);
            }

            if (!namespaceManager.SubscriptionExists("topicTest", "subscriptionAllMessages"))
            {
                namespaceManager.CreateSubscription("topicTest", "subscriptionAllMessages");
            }

            if (!namespaceManager.SubscriptionExists("topicTest", "subscriptionHighMessages"))
            {
                SqlFilter filterHighMessages = new SqlFilter("MessageNumber > 3");
                namespaceManager.CreateSubscription("topicTest", "subscriptionHighMessages", filterHighMessages);
            }

            if (!namespaceManager.SubscriptionExists("topicTest", "subscriptionLowMessages"))
            {
                SqlFilter filterLowMessages = new SqlFilter("MessageNumber <= 3");
                namespaceManager.CreateSubscription("topicTest", "subscriptionLowMessages", filterLowMessages);
            }

            TopicClient topicClient = TopicClient.CreateFromConnectionString(connectionString, "topicTest");
            //topicClient.Send(new BrokeredMessage());

            for (int i = 0; i < 5; i++)
            {
                BrokeredMessage message = new BrokeredMessage("Test message " + i);
                message.Properties["MessageNumber"] = i;

                topicClient.Send(message);
                Console.WriteLine("Message: " + message.ToString());
            }

        }
    }
}
