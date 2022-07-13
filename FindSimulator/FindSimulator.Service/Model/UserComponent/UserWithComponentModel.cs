using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.UserComponent
{
    public class UserWithComponentModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public int UserID { get; set; }

        public List<UserComponentView> userComponentViews;
       

        public UserWithComponentModel()
        {
            userComponentViews = userComponentViews ?? new List<UserComponentView>();
        } 
    }
    
}
