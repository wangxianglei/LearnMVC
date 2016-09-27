using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAzure.Tutorial.RedisCache.HelloWorld
{
    public static class GettingStarted
    {
        private static Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(ConfigurationManager.AppSettings["RedisCacheConnection"]);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        public static void Run()
        {
            IDatabase cache = Connection.GetDatabase();

            //cache.KeyDelete("itemKey");
            //cache.KeyDelete("i");

            var itemKey = "itemKey";

            string itemValue = cache.StringGet(itemKey);
            if (itemValue == null)
            {
                itemValue = GetFromPersistentStorage(itemKey);
                cache.StringSet(itemKey, itemValue);
            }

            Console.WriteLine("Value retrieved: {0}", cache.StringGet(itemKey));
        }


        private static string GetFromPersistentStorage(string itemKey)
        {
            return DateTime.Now.ToString();
        }
    }
}
