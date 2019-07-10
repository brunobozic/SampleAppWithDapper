using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleAppWithDapper.Startup))]
namespace SampleAppWithDapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
