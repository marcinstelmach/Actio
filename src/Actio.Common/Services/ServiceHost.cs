using System;
using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RawRabbit;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost webHost;

        public ServiceHost(IWebHost webHost)
        {
            this.webHost = webHost;
        }

        public async Task Run()
            => await webHost.RunAsync();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace ?? throw new InvalidOperationException();
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(config)
                .UseStartup<TStartup>();
            return new HostBuilder(webHostBuilder.Build());

        }
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost webHost;
        private IBusClient busClient;

        public HostBuilder(IWebHost webHost)
        {
            this.webHost = webHost;
        }

        public BusBuilder UseRabbitMq()
        {
            busClient = (IBusClient)webHost.Services.GetService(typeof(IBusClient));
            return new BusBuilder(webHost, busClient);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }

    }

    public class BusBuilder : BuilderBase
    {
        private readonly IBusClient busClient;
        private readonly IWebHost webHost;

        public BusBuilder(IWebHost webHost, IBusClient busClient)
        {
            this.webHost = webHost;
            this.busClient = busClient;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(webHost);
        }

        public BusBuilder SubscibeToCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>) webHost.Services.GetService(typeof(ICommandHandler<TCommand>));
            busClient.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscibeToEvent<TEvent>(TEvent command) where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)webHost.Services.GetService(typeof(IEventHandler<TEvent>));
            busClient.WithEventHandlerAsync(handler);

            return this;
        }
    }

}
