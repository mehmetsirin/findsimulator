using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.SessionPerson
{
    class SessionPersonModel
    {
    }
  public   sealed record SessionPersonView
    {
        [JsonIgnore]
        public int ID { get; init; } = default!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string Nationality { get; set; } = default;
        public string LicenseNumber { get; set; }
        public string CompanyName { get; set; }
       
        public string Url { get; set; }
        public int  SessionPersonID { get; set; }

    }
    public    sealed record SessionPersonAdd
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string Nationality { get; set; } = default;
        public string LicenseNumber { get; set; }
        public string CompanyName { get; set; }
        public int SessionID { get; set; }
        public int SessionDetailID { get; set; }
        public string Url { get; set; }

    }
    public sealed record SessionPersonUpdate
    {
        public int ID { get; init; } = default!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelNo { get; set; }
        public string Nationality { get; set; } = default;
        public string LicenseNumber { get; set; }
        public string CompanyName { get; set; }
        public int SessionID { get; set; }
        public int SessionDetailID { get; set; }
    }
}
