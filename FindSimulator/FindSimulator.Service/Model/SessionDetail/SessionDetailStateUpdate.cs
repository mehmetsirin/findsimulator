using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
     public class SessionDetailStateUpdate
    {
        public  int SessionDetailID { get; set; }
        public int SessionID { get; set; }

        public Guid OrderID { get; set; }
    }
}
