
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.Claims
{
    public class ClaimService : IClaimService
    {

        public string UserName { get;set; }
        public string Email { get;set; }
        public string PersonIdentity { get;set; }
        public string Surname { get;set; }
        public string TelNo { get;set; }
        public string CountryCode { get;set; }
        public int UserID { get; set; }

        public void SetInit(string userName, string email, string surname, string telNo, int userID)
        {
            UserName = userName;
            Email = email;
            //PersonIdentity = personIdentity;
            Surname = surname;
            TelNo = telNo;
            UserID = userID;
        }

       
    }
}
