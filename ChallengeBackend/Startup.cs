using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChallengeBackend.Startup))]
namespace ChallengeBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
