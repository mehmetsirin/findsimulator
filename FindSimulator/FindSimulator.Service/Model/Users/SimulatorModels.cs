using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Users
{
    public  class SimulatorModels
    {
    }
      public  record   SimulatorSearcModel( DateTime EndDate, string simulatorType,string  aircraftType,string location)
    {
        public DateTime StarDate { get; set; }
    }
}
