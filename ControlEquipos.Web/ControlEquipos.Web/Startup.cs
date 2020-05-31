using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlEquipos.Web.Startup))]
namespace ControlEquipos.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
