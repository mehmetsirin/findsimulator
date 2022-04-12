﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionPerson
{
    public  class SessionwithPersonwithDetailModel
    {
        public SessionwithPersonwithDetailModel()
        {
            sessionPersonViews = sessionPersonViews ?? new List<SessionPersonView>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public bool IsTeacher { get; set; }
        public string CompanyName { get; set; }
        public string SimulatorType { get; set; }
        public string AircraftType { get; set; }
        public string Engine { get; set; }
          
     public List<SessionPersonView> sessionPersonViews { get; set; }
       
    }
}