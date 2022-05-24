using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.ComplexTypes;
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
        private readonly IMapper mapper;

        public SimulatorDeviceService(IBaseRepository<int> baseRepository, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.mapper = mapper;
        }

        public async Task<DataResult<SimulatorDeviceView>> GetByIDAsync(int id)
        {
            var data = await baseRepository.GetByIdAsync<SimulatorDevice>(id);
            var resData = mapper.Map<SimulatorDeviceView>(data.Data);
            var airCrafts = await baseRepository.List<AirCraft>();
            var simulatorTypes = await baseRepository.List<SimulatorType>();
            resData.CraftName = airCrafts.Data.Where(y => y.ID == resData.AirCraftsID)?.FirstOrDefault()?.Name;
            resData.SimulatorTypeName = simulatorTypes.Data.Where(y => y.ID == resData.SimulatorTypeID)?.FirstOrDefault()?.Name;



            return new DataResult<SimulatorDeviceView>(ResultStatus.Success, resData);
        }

        public async Task<DataResult<List<SimulatorDeviceView>>> ListAsync()
        {
            var data = await baseRepository.List<SimulatorDevice>();
            var resData = mapper.Map<List<SimulatorDeviceView>>(data.Data);
            var airCrafts = await baseRepository.List<AirCraft>();
            var simulatorTypes = await baseRepository.List<SimulatorType>();
            foreach (var item in resData)
            {
                item.CraftName = airCrafts.Data.Where(y => y.ID == item.AirCraftsID)?.FirstOrDefault()?.Name;
                item.SimulatorTypeName = simulatorTypes.Data.Where(y => y.ID == item.SimulatorTypeID)?.FirstOrDefault()?.Name;

            }

            return new DataResult<List<SimulatorDeviceView>>(ResultStatus.Success, resData);
        }

        public async Task<DataResult<List<SimulatorDeviceView>>> ListAsync(int id)
        {
            var data = baseRepository.GetQueryable<SimulatorDevice>().GetAwaiter().GetResult().Data.Where(y => y.ID == id).ToList();
            dynamic resData = mapper.Map<SimulatorDeviceView>(data);
            return new DataResult<List<SimulatorDeviceView>>(ResultStatus.Success, resData);
        }

        public async Task<DataResult<bool>> AddAsync(SimulatorDeviceCreate create)
        {
            var map = mapper.Map<SimulatorDevice>(create);
            map.ManufacturerYear = map.Year;
            await baseRepository.AddOneAsync<SimulatorDevice>(map);
            baseRepository.SaveChanges();
            return new DataResult<bool>(ResultStatus.Success, true);
        }

        public async Task<DataResult<bool>> DeleteAsync(int id)
        {
            var delete = await baseRepository.DeleteAsync<SimulatorDevice>(id);
            _ = baseRepository.SaveChangesAsync();
            return new DataResult<bool>(ResultStatus.Success, delete);

        }

        public async Task<DataResult<bool>> UpdateAsync(SimulatorDeviceUpdate update)
        {
            var modelUpdate = new SimulatorDevice(update.Approval, update.Code, update.SimulatorCertificate, update.EasaCode, update.CerfiticationLevel, update.EngineType, update.Year, update.ManufacturerID, update.Uprt, update.MotionSystem, update.ImageGenerator, update.DeviceLocationID, update.ManufacturerYear, update.CompanyID, update.SimulatorTypeID, update.AirCraftsID, update.ID);
            await baseRepository.UpdateOneAsync<SimulatorDevice>(modelUpdate);
            await baseRepository.SaveChangesAsync();
            return new DataResult<bool>(ResultStatus.Success, true);

        }
    }
}
