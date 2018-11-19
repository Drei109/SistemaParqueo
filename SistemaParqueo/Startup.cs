using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaParqueo.Startup))]
namespace SistemaParqueo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var defaultCulture = new CultureInfo("es-PE");
            Thread.CurrentThread.CurrentCulture = defaultCulture;
            Thread.CurrentThread.CurrentUICulture = defaultCulture;
            ConfigureAuth(app);
        }
    }
}
