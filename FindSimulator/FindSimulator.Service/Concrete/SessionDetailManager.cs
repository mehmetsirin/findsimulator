using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Repositories.EntityRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.SessionDetail;
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
    public  class SessionDetailManager:  ISessionDetailManager
    {
        private readonly ISessionDetailRepository sessionDetail;
        private readonly IBaseManager<int> baseManager;
         private  readonly IMapper mapper;

        public SessionDetailManager(ISessionDetailRepository sessionDetail, IBaseManager<int> baseManager, IMapper mapper)
        {
            this.sessionDetail = sessionDetail;
            this.baseManager = baseManager;
            this.mapper = mapper;
        }

        public    async Task<DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>> GetSessionDetail(int sessionID, int SimulatorDeviceID)
        {
            var device = await baseManager.GetByIDAsync<SimulatorDevice>(SimulatorDeviceID);
            var sessionsDetailData = sessionDetail.GetQueryable<SessionDetails>().GetAwaiter().GetResult().Data.Where(y => y.SessionsID == sessionID).ToList();

            var tuple = new Tuple<SimulatorDevice, List<SessionDetails>>(device.Data, sessionsDetailData);
            return new DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>(ResultStatus.Success,tuple);
        }
    }
}
