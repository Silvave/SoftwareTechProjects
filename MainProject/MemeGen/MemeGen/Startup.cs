using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MemeGen.Startup))]
namespace MemeGen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
