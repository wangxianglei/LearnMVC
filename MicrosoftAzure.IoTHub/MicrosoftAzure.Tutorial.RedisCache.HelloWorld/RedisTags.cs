using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAzure.Tutorial.RedisCache.HelloWorld
{
    public static class RedisTags
    {
        public static List<BlogPost> Posts;
        public static List<string[]> Tags;

        public static void Run()
        {
            IDatabase cache = RedisHelper.Connection.GetDatabase();

            Tags = new List<string[]>
            {
                new string[] { "iot","csharp" },
                new string[] { "iot","azure","csharp" },
                new string[] { "csharp","git","big data" },
                new string[] { "iot","git","database" },
                new string[] { "database","git" },
                new string[] { "csharp","database" },
                new string[] { "iot" },
                new string[] { "iot","database","git" },
                new string[] { "azure","database","big data","git","csharp" },
                new string[] { "azure" }
            };

            Posts = new List<BlogPost>();
            int blogKey = 0;
            int blogPostId = 0;
            int numberOfPost = 20;
            Random random = new Random();


        }
    }
}
