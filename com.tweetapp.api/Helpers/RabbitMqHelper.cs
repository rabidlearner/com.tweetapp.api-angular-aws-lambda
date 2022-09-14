using com.tweetapp.api.Models;
using com.tweetapp.api.Repo.Implementation;
using com.tweetapp.api.Repo.IRepo;
using MassTransit;

namespace com.tweetapp.api.Helpers
{
    public static class RabbitMqHelper
    {
        public static void ConfigureRabbitMq(this IServiceCollection services)
        {            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TweetsRepo>();

                //x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) => {
                    cfg.Host("amqps://admin:Adminrabbitmq12345@b-3070b4f4-4bc3-4a0a-82b2-700ff521afca.mq.us-west-2.amazonaws.com:5671");
                    cfg.ReceiveEndpoint("tweet-queue", c =>
                    {
                        c.ConfigureConsumer<TweetsRepo>(context);
                    });
            });
            });
            //services.AddMassTransitHostedService();
        }
    }
}
