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
    public partial record SimulatorDeviceView(
     string ImageGenerator, string Approval, string Code, string SimulatorCertificate, string EasaCode, string CerfiticationLevel, string EngineType, string Year, string Manufacturer, string Uprt, string MotionSytem);
    public sealed partial record SimulatorDeviceUpdate();
    public sealed partial record SimulatorDeviceCreate();
    public sealed partial record SimulatorDeviceDelete();
    //public sealed partial record SimulatorDeviceUpdate();




 





}
