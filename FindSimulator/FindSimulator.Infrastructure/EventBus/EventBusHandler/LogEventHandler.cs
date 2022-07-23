using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.EventBus.Event;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Share.AppConfiguration;
using FindSimulator.Share.Event;
using FindSimulator.Share.RabbitMq;
using FindSimulator.Share.Results.Concrete;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.EventBus.EventBusHandler
{
    public class LogEventHandler : IIntegrationEventHandler<LogEventTH>
    {
        public Infrastructure.Repositories.BaseRepository.IBaseRepository<Guid> baseRepository;
        IServiceCollection services = null;
        internal static IServiceProvider ServiceProvider { get; private set; }
        public LogEventHandler(IServiceCollection serviceProvider)
        {
            services = serviceProvider;
        }

        public async Task Handle(LogEventTH @event)
        {
            await LogAdd(@event);
        }

        public async Task LogAdd(LogEventTH logEvent)
        {
               ServiceProvider = services.BuildServiceProvider();

                baseRepository = ServiceProvider.GetRequiredService<IBaseRepository<Guid>>();
                Logger logger = new Logger(logEvent.Action, logEvent.Content, logEvent.IP, logEvent.UserID);
                baseRepository.AddOne<Logger>(logger);
                baseRepository.SaveChanges();
        }

    }
    public interface ILogEventHandler
    {
        public Task LogAdd(LogEventTH @event);

    }
}
