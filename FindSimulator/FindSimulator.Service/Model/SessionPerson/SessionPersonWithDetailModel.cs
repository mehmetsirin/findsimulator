using FindSimulator.Service.Model.SessionDetail;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionPerson
{
    public  class SessionPersonWithDetailModel
    {

        public List<SessionPersonView>  SessionPersonViews { get; set; }

        public List<SessionDetailView>  SessionDetailViews { get; set; }

        public SessionPersonWithDetailModel(List<SessionPersonView> sessionPersonViews, List<SessionDetailView> sessionDetailViews)
        {
            SessionPersonViews = sessionPersonViews;
            SessionDetailViews = sessionDetailViews;
        }
    }
}
