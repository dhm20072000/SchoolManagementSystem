using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolManamentSystem.Startup))]
namespace SchoolManamentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
