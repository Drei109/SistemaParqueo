using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaParqueo.Startup))]
namespace SistemaParqueo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
