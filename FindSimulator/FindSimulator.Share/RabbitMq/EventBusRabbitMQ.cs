
using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.RabbitMq
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMQPersistentConnection _rabbitMQPersistentConnection = null;
        public void Dispose()
        {
            _rabbitMQPersistentConnection.Dispose();
        }
        public EventBusRabbitMQ(IRabbitMQPersistentConnection rabbitMQPersistentConnection, IServiceProvider serviceProvider)
        {
            _rabbitMQPersistentConnection = rabbitMQPersistentConnection;
            _serviceProvider = serviceProvider;
        }

        public void Publish(IntegrationEvent @event, string queueName = "")
        {

            if (string.IsNullOrEmpty(queueName))
            {
                queueName = @event.GetType().Name;
            }

            try
            {
                var eventName = @event.GetType().Name;
                if (!_rabbitMQPersistentConnection.IsConnected)
                    _rabbitMQPersistentConnection.TryConnect();
                var channel = _rabbitMQPersistentConnection.CreateModel();
                channel.ExchangeDeclare(queueName, durable: true, autoDelete: false, type: ExchangeType.Direct);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true; //  diske  kaydediyor;
                var messages = JsonConvert.SerializeObject(@event);
                var bodyBtye = Encoding.UTF8.GetBytes(messages);

                channel.BasicPublish(queueName, routingKey: queueName, properties, body: bodyBtye);
            }
            catch (Exception ex)
            {
                var d = "";
                throw new Exception("rabbit mg");
            }


        }

        public void Subscribe<T, TH>(string queueName = "")
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (string.IsNullOrEmpty(queueName))
            {
                queueName = typeof(T).Name;
            }
            if (!_rabbitMQPersistentConnection.IsConnected)
            {
                _rabbitMQPersistentConnection.TryConnect();

            }
            var channel = _rabbitMQPersistentConnection.CreateModel();


            channel.ExchangeDeclare(queueName, durable: true, autoDelete: false, type: ExchangeType.Direct);


            try
            {
                channel.QueueBind(queue: queueName, exchange: queueName, queueName);
                //channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);


                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                        var eventType = typeof(T);
                        var handler = _serviceProvider.GetService(typeof(TH));
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                        try
                        {

                            ((Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent })).Wait();
                            channel.BasicAck(ea.DeliveryTag, multiple: false);

                        }
                        catch (Exception ex)
                        {
                            channel.BasicAck(ea.DeliveryTag, multiple: false);
                            Console.WriteLine(ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);


            }
            catch (Exception ex)
            {
                _rabbitMQPersistentConnection.Dispose();
                channel.Dispose();
            }

        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}