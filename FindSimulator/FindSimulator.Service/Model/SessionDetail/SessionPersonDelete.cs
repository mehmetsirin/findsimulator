using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
  public  class SessionPersonDelete
    {

        public int SessionsID { get; set; }
        public int SessionsDeatailID { get; set; }

        public SessionPersonDelete(int sessionsID, int sessionsDeatailID)
        {
            SessionsID = sessionsID;
            SessionsDeatailID = sessionsDeatailID;
        }

        public SessionPersonDelete()
        {
        }
    }
}
