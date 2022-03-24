using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
    public  partial class SessionPerson: BaseEntity<int>
    {

        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string Nationality { get; set; }
        public string LicenseNumber { get; set; }
        public string  CompanyName { get; set; }
        public int  SessionID { get; set; }
        public int  SessionDetailID { get; set; }

        public int UserID { get; set; }

    }
}
