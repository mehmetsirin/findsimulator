﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionDetail
{
    public  class SessionDetailModel
     {
    }

     public   record SessionDetailView()
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SessionsID { get; set; }
    }
}
