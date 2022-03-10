using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
     public      class SimulatorDevice:BaseEntity<int>
    {
        public string Approval { get; set; }
        public string Code { get; set; }
        public string SimulatorCertificate { get; set; }
        public string EasaCode { get; set; }
        public string CerfiticationLevel { get; set; }
        public string EngineType { get; set; }
        public string Year { get; set; }
        public string Manufacturer { get; set; }
        public bool Uprt { get; set; }
        public string MotionSytem { get; set; }
        public string ImageGenerator { get; set; }
      

    }
}
