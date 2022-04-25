using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
    public class SessionDetailCreate
    {
        public List<SessionDate> sessionDates { get; set; }
         public int SessionsID { get; set; }

    }
    public class SessionDate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
