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
                    cfg.Host("amqps://admin:rabbitmqadmin@b-4f86e223-5bb7-42e4-bc06-d7b97bedded5.mq.us-west-2.amazonaws.com:5671");
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
