using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Users
{
  public  class UserWithUserComponentUpdate
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<UserComponentUpdate>  userComponentUpdates { get; set; }
    }
    public  class UserComponentUpdate
    {
        public int UserComponentID { get; set; }

        public bool IsRead { get; set; } = true;
        public bool IsWrite { get; set; } = true;
        public bool IsCreate { get; set; } = true;
        public bool IsDelete { get; set; } = true;
        public bool IsUpdate { get; set; } = false;
    }
}
