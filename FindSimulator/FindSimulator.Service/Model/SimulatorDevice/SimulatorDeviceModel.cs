using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SimulatorDevice
{
    public class SimulatorDeviceModel
    {
    }
    public partial record SimulatorDeviceView
    {
        public int ID { get; set; }
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
        public string CraftName { get; set; }
        public string SimulatorTypeName { get; set; }
   
    }
    public  record SimulatorDeviceUpdate
    {

        public int ID { get; set; }
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

        public string Approval { get; set; }
    }
    public sealed partial record SimulatorDeviceCreate {

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

        public  string Approval { get; set; }
    }

    
    public sealed partial record SimulatorDeviceDelete();
    //public sealed partial record SimulatorDeviceUpdate();




 





}
