using FindSimulator.Infrastructure.Utilities;
using FindSimulator.Service.Abstract;
using FindSimulator.Share.Claims;
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
        public IUnitOfWork _unitOfWork { get; }
        public readonly IEventBus _eventBus;

       public  IPaymentManager _payment;
        public IClaimService _claimService;
        public BusinessManagerFactory(IUnitOfWork unitOfWork, IEventBus eventBus, IPaymentManager payment,IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
            _payment = payment;
            _claimService = claimService;
        }
    }
}
