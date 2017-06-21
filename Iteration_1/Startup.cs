using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Iteration_1.Startup))]
namespace Iteration_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
