using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MarioBackend.Startup))]

namespace MarioBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}