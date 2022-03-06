using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public sealed partial class SimulatorDeviceService : ISimulatorDeviceService
    {
        public Infrastructure.Repositories.BaseRepository.IBaseRepository<int> baseRepository;
         private  readonly IMapper mapper;

        public SimulatorDeviceService(IBaseRepository<int> baseRepository, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }

        public   async Task<DataResult<List<SimulatorDeviceView>>> GetByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public   async Task<DataResult<List<SimulatorDeviceView>>> ListAsync()
        {
            var data =   await baseRepository.List<SimulatorDevice>();
            dynamic resData = mapper.Map<SimulatorDeviceView>(data);
            return resData;
        }

        public   async Task<DataResult<List<SimulatorDeviceView>>> ListAsync(int id)
        {
            var data =  baseRepository.GetQueryable<SimulatorDevice>().GetAwaiter().GetResult().Data.Where(y=>y.ID==id).ToList();
            dynamic resData = mapper.Map<SimulatorDeviceView>(data);
            return resData;
        }
    }
}
