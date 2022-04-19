using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
   public   class Sessions : BaseEntity<int>
    {


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public bool  IsTeacher { get; set; }
        public string CompanyName { get; set; }
        public string SimulatorType { get; set; }
        public string AircraftType { get; set; }
        public string Engine { get; set; }
        public decimal Price { get; set; }
        public int SimulatorDeviceID { get; set; }

        public Sessions(DateTime startDate, DateTime endDate, string location, bool ısTeacher, string companyName, string simulatorType, string aircraftType, string engine, decimal price, int simulatorDeviceID)
        {
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            IsTeacher = ısTeacher;
            CompanyName = companyName;
            SimulatorType = simulatorType;
            AircraftType = aircraftType;
            Engine = engine;
            Price = price;
            SimulatorDeviceID = simulatorDeviceID;
        }

        public Sessions()
        {
        }
    }
}
