using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.RabbitMq
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {

        bool IsConnected { get; }
        bool TryConnect();
        RabbitMQ.Client.IModel CreateModel();
    }
}
