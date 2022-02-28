using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Concrete.Model
{
    public class ContactPerson:ValueObject
    {
        public List<string> Emails { get; set; }
        public List<string> TelNos { get; set; }
        public   List<string>  Address { get; set; }
        public string Name { get; set; }

        public ContactPerson(List<string> emails, List<string> telNos, List<string> address, string name)
        {
            Emails = emails;
            TelNos = telNos;
            Address = address;
            Name = name;
        }

        public ContactPerson()
        {
        }
    }
}