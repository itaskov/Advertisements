using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Advertisements.Web.Startup))]
namespace Advertisements.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
