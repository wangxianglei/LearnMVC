using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicrosoftAzure.Tutorial.RedisCache.Startup))]
namespace MicrosoftAzure.Tutorial.RedisCache
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
