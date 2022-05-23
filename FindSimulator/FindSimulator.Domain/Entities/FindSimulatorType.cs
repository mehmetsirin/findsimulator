using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
  public  class FindSimulatorType : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int GroupID { get; set; }
        public int CompanyID { get; set; }

    }
}
