using System.Reflection;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using RawRabbit.Pipe;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient busClient, ICommandHandler<TCommand> handler)
            where TCommand : ICommand
            => busClient.SubscribeAsync<TCommand>(msg =>
                handler.HandleAsync(msg), ctx =>
                    ctx.UseConsumerConfiguration(cfg =>
                        cfg.FromDeclaredQueue(q =>
                            q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient busClient, IEventHandler<TEvent> handler)
            where TEvent : IEvent
            => busClient.SubscribeAsync<TEvent>(msg =>
                handler.HandleAsync(msg), ctx =>
                ctx.UseConsumerConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q =>
                        q.WithName(GetQueueName<TEvent>()))));


        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
}
