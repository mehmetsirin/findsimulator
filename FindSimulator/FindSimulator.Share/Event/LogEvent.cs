using FindSimulator.Share.RabbitMq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Event
{
    public class LogEvent : IntegrationEvent
    {
        public int UserID { get; set; }
        public string Action { get; set; }
        public string IP { get; set; }
        public string Content { get; set; }

        //public LogsEnum LogsEnum { get; set; }

    }
}
