using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CVGrupp37.Startup))]
namespace CVGrupp37
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
