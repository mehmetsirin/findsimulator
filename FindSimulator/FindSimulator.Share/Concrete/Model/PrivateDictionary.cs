using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Concrete.Model
{
  public   class PrivateDictionary
    {
        public PrivateDictionary()
        {
        }

        public PrivateDictionary(Guid key, string value)
        {
            Key = key;
            Value = value;
        }

        public  Guid Key { get; set; }
        public   string  Value { get; set; }

    }
}
