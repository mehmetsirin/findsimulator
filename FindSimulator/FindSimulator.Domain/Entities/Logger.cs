using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
    public  partial  class Logger: BaseEntity<Guid>
    {

        public  string ActionName  { get; set; }
        public string LogContent { get; set; }
        public string Ip { get; set; }
        public    int  UserID { get; set; }

        public Logger()
        {
        }

        public Logger(string actionName, string logContent, string ıp, int userID)
        {
            ActionName = actionName;
            LogContent = logContent;
            Ip = ıp;
            UserID = userID;
        }
    }
}
