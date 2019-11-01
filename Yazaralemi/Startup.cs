using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yazaralemi.Startup))]
namespace Yazaralemi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
