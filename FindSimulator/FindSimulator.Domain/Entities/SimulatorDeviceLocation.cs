using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
    public class SimulatorDeviceLocation: BaseEntity<int>
    {

        public string District { get; set; }
        public string Province { get; set; }
        public int CompanyID { get; set; }
    }
}
