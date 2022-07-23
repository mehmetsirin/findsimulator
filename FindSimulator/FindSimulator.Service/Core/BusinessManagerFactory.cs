using FindSimulator.Infrastructure.Utilities;
using FindSimulator.Share.RabbitMq;

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
        public readonly IEventBus eventBus;

        public static int x = 10;
        public BusinessManagerFactory(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            UnitOfWork = unitOfWork;
            this.eventBus = eventBus;
        }
    }
}
