using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
    public sealed record CalendarView
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; } = true;

        public ExtendedProps ExtendedProps { get; set; }

        public CalendarView(int ıd, string url, string title, DateTime start, DateTime end, bool allDay, string extendedProps)
        {
            Id = ıd;
            Url = url;
            Title = title;
            Start = start;
            End = end;
            AllDay = allDay;
            ExtendedProps = new ExtendedProps(extendedProps);
        }

        public CalendarView()
        {
        }
    }
    public record ExtendedProps
    {
        public ExtendedProps(string calendar)
        {
            Calendar = calendar;
        }

        public string Calendar { get; set; }
    }
}
