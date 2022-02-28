using FindSimulator.Share.RabbitMq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.EventBus.Event
{
    public  class DictionaryEvent: IntegrationEvent
    {
        public Guid Key { get; set; }
        public string Value { get; set; }

        public DictionaryEvent(Guid key, string value)
        {
            Key = key;
            Value = value;
        }

        public DictionaryEvent()
        {

        }
    }
}
