using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
    public    sealed record CalendarView
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; } = true;

        public CalendarView(int ıd, string url, string title, DateTime start, DateTime end, bool allDay)
        {
            Id = ıd;
            Url = url;
            Title = title;
            Start = start;
            End = end;
            AllDay = allDay;
        }

        public CalendarView()
        {
        }
    }
}
