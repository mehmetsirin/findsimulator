using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FindSimulator.Share.RabbitMq
{
    public interface IEventBus : IDisposable
    {
        void Publish(IntegrationEvent @event, string queueName = "");

        void Subscribe<T, TH>(string queueName = "")
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        //[JsonProperty]
        public Guid Id { get; private set; }
        //[JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
