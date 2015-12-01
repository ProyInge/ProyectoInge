using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GestionPruebas.Startup))]
namespace GestionPruebas
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
