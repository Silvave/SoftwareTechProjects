using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HangoverPartII.Startup))]
namespace HangoverPartII
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
