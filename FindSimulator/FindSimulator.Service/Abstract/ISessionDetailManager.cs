using FindSimulator.Domain.Entities;
using FindSimulator.Service.Model.Calendar;
using FindSimulator.Service.Model.RequestModel;
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
        Task<DataResult<bool>> SessionAddAsync(SessionCreate model, int companyID, int userId);

        Task<DataResult<bool>> SessionRemove(int id);
        Task<DataResult<List<SessionDetailView>>> SessionwithSessionDetailAsync();
        Task<DataResult<bool>> RemoveAsync(int id);
        Task<DataResult<List<CalendarView>>> GetCalendarAsync(int simulatorDeviceID, string aircraftType);
        Task<DataResult<bool>> SessionStateUpdateAsync(List<SessionDetailStateUpdate> sessionDetailStateUpdates);
        Task<DataResult<List<SessionDetailWithSessionView>>> GetSessionDetailOrderAsync();
        Task<DataResult<bool>> OrderConfirm(OrderConfirmRequest request);

    }
}
