using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssignmentV3.Startup))]
namespace AssignmentV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
