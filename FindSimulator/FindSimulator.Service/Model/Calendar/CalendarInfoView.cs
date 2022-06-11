using FindSimulator.Service.Model.SessionPerson;
using FindSimulator.Service.Model.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Calendar
{
   public  record CalendarInfoView
    {
        public CalendarInfoView(SessionsView sessionsView, List<SessionPersonView> sessionPersonViews)
        {
            SessionsView = sessionsView;
            SessionPersonViews = sessionPersonViews;
        }

        public CalendarInfoView()
        {
        }

        public SessionsView SessionsView { get; set; }
        public List<SessionPersonView>  SessionPersonViews { get; set; }
    }
}
