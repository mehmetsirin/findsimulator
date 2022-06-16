using FindSimulator.Service.Model.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
    public class SessionDetailWithSessionView
    {
        public SessionDetailWithSessionView()
        {
            SessionsView = SessionsView ?? new SessionsView(); 
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SessionsID { get; set; }
        public int ID { get; set; }
        public int Status { get; set; }
        public string DsStatus { get; set; }
        public SessionsView  SessionsView { get; set; }

    }
}
