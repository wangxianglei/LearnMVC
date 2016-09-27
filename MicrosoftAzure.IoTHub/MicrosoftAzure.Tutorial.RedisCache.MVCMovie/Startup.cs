using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicrosoftAzure.Tutorial.RedisCache.MVCMovie.Startup))]
namespace MicrosoftAzure.Tutorial.RedisCache.MVCMovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
