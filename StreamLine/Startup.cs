using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StreamLine.Startup))]
namespace StreamLine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
