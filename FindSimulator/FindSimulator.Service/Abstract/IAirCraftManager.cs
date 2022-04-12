using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
    public  interface IAirCraftManager
    {
        Task<object> ListGroupAsync();
    }
}
