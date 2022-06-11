using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Calendar;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class CalendarManager : ICalendarManager
    {
       public readonly   ISessionsManager _sessionsManager;
        public readonly ISessionPersonManager _sessionPersonManager;

        public CalendarManager(ISessionsManager sessionsManager, ISessionPersonManager sessionPersonManager)
        {
            _sessionsManager = sessionsManager;
            _sessionPersonManager = sessionPersonManager;
        }

        public   async Task<DataResult<CalendarInfoView>> GetCalendarInfoAsync(int sessionID, int sessionDetailID)
        {
            var session = await _sessionsManager.SimulatorSessionByID(sessionID);
            var sessionPersons = await _sessionPersonManager.ListAsync(sessionID, sessionDetailID);
            var calendarInfoView = new CalendarInfoView(session.Data, sessionPersons.Data);
            return   new DataResult<CalendarInfoView>(ResultStatus.Success,calendarInfoView);

        }
    }
}
