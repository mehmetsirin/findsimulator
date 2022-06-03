using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Session
{
    public  class SessionUpdate
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ID { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
