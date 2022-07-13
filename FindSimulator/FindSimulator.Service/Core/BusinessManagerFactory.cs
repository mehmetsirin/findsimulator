using FindSimulator.Infrastructure.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Core
{
   public  class BusinessManagerFactory
    {
        public IUnitOfWork UnitOfWork { get; }
        public static int x = 10;
        public BusinessManagerFactory(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
