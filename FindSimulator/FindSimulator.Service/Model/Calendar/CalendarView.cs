using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Calendar
{
    public sealed record CalendarView
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; } = true;
        public int Reserved { get; set; }
        public int Status { get; set; }
        public ExtendedProps ExtendedProps { get; set; }
        public Guid  OrderID { get; set; }

        public CalendarView(int ıd, string url, string title, DateTime start, DateTime end, bool allDay, string extendedProps, int reserved, string aircraftType,int sessionId=0)
        {
            Id = ıd;
            Url = url;
            Title = title + start.Hour + ":" + start.Minute + "-" + end.Hour + ":" + end.Minute;
            Start = start;
            End = end;
            AllDay = allDay;
            Reserved = reserved;
            ExtendedProps = new ExtendedProps(extendedProps, aircraftType,sessionId);
        }

        public CalendarView()
        {
        }
    }
    public record ExtendedProps
    {
        public ExtendedProps(string calendar, string aircraftType,int  sessionID)
        {
            Calendar = calendar;
            AircraftType = aircraftType;
            SessionID = sessionID;
        }

        public string Calendar { get; set; }
        public string AircraftType { get; set; }
        public int SessionID { get; set; }

    }
}
