using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicrosoftAzure.Tutorial.RedisCache.HelloWorld
{
    public static class RedisString
    {
        public static void Run()
        {
            IDatabase cache = RedisHelper.Connection.GetDatabase();

            cache.StringSet("CurrentValue", 1);
            Console.WriteLine("Current Value: {0}", cache.StringGet("CurrentValue"));

            var contact = new Contact() { Id = 1, Name = "HelloWorld", Email = "helloworld@hello.world" };
            cache.StringSet("contact", JsonConvert.SerializeObject(contact));
            var contactCache = JsonConvert.DeserializeObject<Contact>(cache.StringGet("contact"));
            Console.WriteLine("Contact Email: {0}", contactCache.Email);

            cache.StringSet("CurrentValueWithExpiration", 1, TimeSpan.FromSeconds(5));
            Console.WriteLine("CurrentValue: {0}", cache.StringGet("CurrentValueWithExpiration"));
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("CurrentValue: {0}", cache.StringGet("CurrentValueWithExpiration"));
            

        }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
