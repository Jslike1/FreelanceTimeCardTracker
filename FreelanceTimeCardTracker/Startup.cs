using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreelanceTimeCardTracker.Startup))]
namespace FreelanceTimeCardTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
