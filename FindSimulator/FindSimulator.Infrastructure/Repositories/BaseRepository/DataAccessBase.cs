using FindSimulator.Infrastructure.Concrete.EntityFramework.Context;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{
     public  abstract class DataAccessBase
    {
        private readonly object _initLock = new object();
        public SimulatorContext _context;
        protected DataAccessBase()
        {
            //lock (_initLock)
            //{
            //    if (_context == null)
            //    {
            //    //  _context = new  SimulatorContext();
            //    }
                
            //}


        }
    }
}
