using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sales_App.Startup))]
namespace Sales_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
