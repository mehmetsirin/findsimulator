using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public   interface ISimulatorDeviceService
    {

        Task<DataResult<List<SimulatorDeviceView>>> ListAsync();
        Task<DataResult<List<SimulatorDeviceView>>> ListAsync(int id);
        Task<DataResult<List<SimulatorDeviceView>>>GetByIDAsync(int id);


    }
}
