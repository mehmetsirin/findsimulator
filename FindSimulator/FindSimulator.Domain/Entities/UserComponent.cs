using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
  public   class UserComponent: BaseEntity<int>
    {
        public int  UserID { get; set; }
        public int PageComponentID { get; set; }
    }
}
