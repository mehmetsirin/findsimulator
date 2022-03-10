using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Users
{
   public   class SessionsModel
    {
    }
    //public  record  SessionsView(string Location,bool IsTeacher,string CompanyName,string SimulatorType, string AircraftType, string Engine, decimal Price, DateTime StarDate, DateTime EndDate);
      public  record SessionDetailsView(DateTime StarDate,DateTime EndDate,int SessionsID);
      public class SessionsView {

        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public bool IsTeacher { get; set; }
        public string CompanyName { get; set; }
        public string SimulatorType { get; set; }
        public string AircraftType { get; set; }
        public string Engine { get; set; }
        public int SimulatorDeviceID { get; set; }

        public decimal Price { get; set; }
    }
}
