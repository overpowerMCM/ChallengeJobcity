using ChallengeBackend.Sources;
using Microsoft.Owin;
using Owin;
using RabbitMQ.Client;

[assembly: OwinStartupAttribute(typeof(ChallengeBackend.Startup))]
namespace ChallengeBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MessengerRabbit v = MessengerRabbit.Instance;//.ConfigureRabbitMQ();
            app.MapSignalR();
            ConfigureAuth(app);
        }

    }
}
