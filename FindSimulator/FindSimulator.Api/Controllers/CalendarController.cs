using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Calendar;
using FindSimulator.Share.Results.Concrete;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarManager _calendarManager;
        private readonly ISessionDetailManager _sessionDetailManager;
        public CalendarController(ICalendarManager calendarManager, ISessionDetailManager sessionDetailManager)
        {
            _calendarManager = calendarManager;
            _sessionDetailManager = sessionDetailManager;
        }

        [HttpGet]
          public   async  Task<DataResult<CalendarInfoView>> GetCalendarInfo(int SessionID, int SessionDetailID )
        {
            var data =  await _calendarManager.GetCalendarInfoAsync(SessionID, SessionDetailID);
            return data;
        }
        [HttpGet]
        public async Task<DataResult<List<CalendarView>>> GetCalendarAsync(int simulatorDeviceID, string aircraftType)
        {
            var data = await _sessionDetailManager.GetCalendarAsync(simulatorDeviceID, aircraftType);
            return data;
        }
    }
}
