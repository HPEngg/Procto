using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoctorWeb.Startup))]
namespace DoctorWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
