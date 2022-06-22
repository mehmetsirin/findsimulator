using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Helper;
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
        Task<DataResult<SimulatorDeviceView>>GetByIDAsync(int id);
        Task<DataResult<bool>> AddAsync(SimulatorDeviceCreate create);
        Task<DataResult<bool>> DeleteAsync(int id);
        Task<DataResult<bool>> UpdateAsync(SimulatorDeviceUpdate update);
        Task<DataResult<List<SelectObject>>> GetSelectObjectAync();
    }
}
