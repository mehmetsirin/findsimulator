using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.RequestModel
{
   public class OrderConfirmRequest
    {
        public   Guid OrderID { get; set; }
        public int Status { get; set; }
    }
}
