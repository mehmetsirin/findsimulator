using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Results.Concrete
{
   public  class RedisModel
    {
        public string Token { get; set; }

        public DateTime InsertDateTime { get; set; }
        public string UserName { get; set; }
        public int UserTypeID { get; set; }
        public int UserID { get; set; }
    }
}
