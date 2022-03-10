using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Results.Concrete
{
     public     abstract record  NotificationModel
    {
        public DateTime SenderDateTime { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Data { get; set; }
        public int NotificationType { get; set; }
        public int SenderID { get; set; }
        public string SenderName { get; set; }
        public int NotificationID { get; set; }
        public bool IsRead { get; set; }
        public int ReceiverID { get; set; }
    }
}

