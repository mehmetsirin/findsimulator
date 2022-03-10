﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.Users
{
    public record UserModelView
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PersonIdentity { get; set; }
        public string Surname { get; set; }
        public string DeviceID { get; set; }
        public string TelNo { get; set; }
        public int ID { get; set; }
        public string AccessToken { get; set; }
         public string RefreshToken { get; set; }

    }
    public record UserLoginModel(string pass, string email,string  deviceID=null);
    public   record  UserRegisterModel(  string userName, string Surname, string password,string email,string personIdentity,string deviceID, string TelNo);
    public  record  UserUpdate(string userName, string Surname, string password, string email, string personIdentity, string TelNo,int  ID);
}
