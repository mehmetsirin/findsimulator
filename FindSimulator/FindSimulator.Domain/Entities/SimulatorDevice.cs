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
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public bool Uprt { get; set; }
        public string MotionSystem { get; set; }
        public string ImageGenerator { get; set; }
        public string Cycle { get; set; }
        public int? ManufacturerYear { get; set; }
        public string CompanyName { get; set; }
        public int SimulatorTypeID { get; set; }
        public int AirCraftsID { get; set; }

    }
}
