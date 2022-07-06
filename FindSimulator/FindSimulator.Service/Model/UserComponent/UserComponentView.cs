using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.UserComponent
{
    public class UserComponentView
    {
        public int PageComponentID { get; set; }
        //public string CompanentID { get; set; }
        //public bool IsAuthorize { get; set; } = false;
        public string ComponentName { get; set; }


        public bool? IsRead { get; set; } = true;
        public bool ? IsWrite { get; set; } = true;
        public bool? IsCreate { get; set; } = true;
        public bool? IsDelete { get; set; } = true;
        public bool? IsUpdate { get; set; }
    }
}

