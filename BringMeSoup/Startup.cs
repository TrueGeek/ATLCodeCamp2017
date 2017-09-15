using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BringMeSoup.Startup))]
namespace BringMeSoup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
