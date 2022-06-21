using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Domain.Entities
{
    public  class Users: BaseEntity<int>
    {
        

        public Users()
        {
        }

        public Users(string userName, string password, string email, string personIdentity, string surname, string deviceID, string telNo, string countryCode) :base()
        {
            UserName = userName;
            Password = password;
            Email = email;
            PersonIdentity = personIdentity;
            Surname = surname;
            DeviceID = deviceID;
            TelNo = telNo;
            CountryCode = countryCode;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PersonIdentity { get; set; }
        public string Surname { get; set; }
        public  string DeviceID { get; set; }
        public string TelNo { get; set; }
        public string CountryCode { get; set; }
        public int  CompanyID { get; set; }
    }
}
