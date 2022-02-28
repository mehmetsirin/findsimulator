using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Concrete.Model
{
  public  class Contact
    {
        public Contact(string email, string telNo)
        {
            Email = email;
            TelNo = telNo;
        }
        public Contact()
        {
        }

        public string Email { get; set; }
        public string TelNo { get; set; }
    }
}
