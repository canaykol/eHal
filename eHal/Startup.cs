using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eHal.Startup))]
namespace eHal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
