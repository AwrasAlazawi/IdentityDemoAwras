using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityDemoAwras.Startup))]
namespace IdentityDemoAwras
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
