using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
  public    partial   class SessionDetails: BaseEntity<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SessionsID { get; set; }
        public int  Reserved { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public Guid OrderID { get; set; }
    }
}
