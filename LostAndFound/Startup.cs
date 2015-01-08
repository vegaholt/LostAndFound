using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LostAndFound.Startup))]
namespace LostAndFound
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
