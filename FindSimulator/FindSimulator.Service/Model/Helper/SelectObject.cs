using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Helper
{
  public  class SelectObject
    {
        public int ID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public int ParentID { get; set; }
    }
}
