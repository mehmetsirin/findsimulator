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
        public int ManufacturerID { get; set; }
        public bool Uprt { get; set; }
        public string MotionSystem { get; set; }
        public string ImageGenerator { get; set; }
        public int? ManufacturerYear { get; set; }
        public int SimulatorTypeID { get; set; }
        public int AirCraftsID { get; set; }
        public int CompanyID { get; set; }
        public int DeviceLocationID { get; set; }
        public SimulatorDevice()
        {
        }

        public SimulatorDevice(string approval, string code, string simulatorCertificate, string easaCode, string cerfiticationLevel, string engineType, int year, int manufacturerID, bool uprt, string motionSystem, string ımageGenerator, int deviceLocationID, int? manufacturerYear,int companyID, int simulatorTypeID, int airCraftsID,int id)
        {
            Approval = approval;
            Code = code;
            SimulatorCertificate = simulatorCertificate;
            EasaCode = easaCode;
            CerfiticationLevel = cerfiticationLevel;
            EngineType = engineType;
            Year = year;
            ManufacturerID = manufacturerID;
            Uprt = uprt;
            MotionSystem = motionSystem;
            ImageGenerator = ımageGenerator;
            DeviceLocationID = deviceLocationID;
            ManufacturerYear = manufacturerYear;
            CompanyID = companyID;
            SimulatorTypeID = simulatorTypeID;
            AirCraftsID = airCraftsID;
            ID = id;
        }
    }
}
