using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MicrosoftAzure.Tutorial.SendGrid.Startup))]
namespace MicrosoftAzure.Tutorial.SendGrid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
