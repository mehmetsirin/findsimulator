using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.UserComponent
{
  public  class UserWithComponentModel
    {
        public int ComponentID { get; set; }
        //public string CompanentID { get; set; }
        public bool IsAuthorize { get; set; } = false;
        public string ComponentName { get; set; }
    }
}
