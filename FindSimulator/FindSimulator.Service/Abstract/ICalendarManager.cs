using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindSimulator.Share.Results.Concrete;
using FindSimulator.Service.Model.Calendar;

namespace FindSimulator.Service.Abstract
{
   public interface ICalendarManager
    {
        Task<DataResult<CalendarInfoView>> GetCalendarInfoAsync(int SessionID, int SessionDetailID);


    }
}
