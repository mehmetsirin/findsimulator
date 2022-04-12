using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Model.AirCraft
{
     public class AirCraftGroupView
    {


        public string Name { get; set; }
         public List<LocationCraft> Locations { get; set; }


        
    }
    
  public  record  LocationCraft(string District,string Province,int  Id);
}
