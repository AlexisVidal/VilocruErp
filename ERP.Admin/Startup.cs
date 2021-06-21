using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ERP.Admin.Startup))]
namespace ERP.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
