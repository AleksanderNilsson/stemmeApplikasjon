using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(stemmeApp.Startup))]
namespace stemmeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
       
    }
}
