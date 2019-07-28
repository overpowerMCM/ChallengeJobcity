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
            //RabbitMQConsummer v = RabbitMQConsummer.Instance;//.ConfigureRabbitMQ();
            app.MapSignalR();
            ConfigureAuth(app);
        }

    }
}
