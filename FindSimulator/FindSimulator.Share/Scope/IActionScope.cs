using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Scope
{
  public  interface IActionScope
    {
        public int IdCompany { get; }
        public int IdUser { get;  }
    }
}
