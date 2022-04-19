using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Session
{
   public class SessionCreate
    {
        public DateTime StartDate { get; set; }
        public    DateTime EndDate { get; set; }
        public int SimulatorDeviceID { get; set; }
        public decimal Price { get; set; }
        public bool IsTeacher { get; set; }
        public string Engine { get; set; }
    }
}
