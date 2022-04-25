﻿using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Session;
using FindSimulator.Service.Model.SessionDetail;
using FindSimulator.Service.Model.SimulatorDevice;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
     public  partial interface ISessionDetailManager
    {
        Task<DataResult<Tuple<SimulatorDevice, List<SessionDetails>>>> GetSessionDetail(int sessionID, int SimulatorDeviceID);
        Task<DataResult<List<SessionDetails>>> GetSessionDetail(List<int> sessionsIds);
        Task<DataResult<List<CalendarView>>> GetCalendarAsync();
        Task<DataResult<bool>> SessionDetailAddAsync(SessionDetailCreate  models);
        Task<DataResult<bool>> SessionAddAsync(SessionCreate model);
        Task<DataResult<List<SessionwithSessionDetailView>>> SessionwithSessionDetailAsync();
        Task<DataResult<bool>> RemoveAsync(int id);


    }
}
