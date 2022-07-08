using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Society.Startup))]
namespace Society
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
