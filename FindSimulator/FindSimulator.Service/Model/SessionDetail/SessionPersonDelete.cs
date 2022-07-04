using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
  public  class SessionPersonDelete
    {

        public int SessionID { get; set; }
        public int SessionDetailID { get; set; }

        public SessionPersonDelete(int sessionsID, int sessionsDeatailID)
        {
            SessionID = sessionsID;
            SessionDetailID = sessionsDeatailID;
        }

        public SessionPersonDelete()
        {
        }
    }
}
