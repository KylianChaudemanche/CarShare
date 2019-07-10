using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarShare.Startup))]
namespace CarShare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
