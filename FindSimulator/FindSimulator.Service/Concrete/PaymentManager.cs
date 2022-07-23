using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Utilities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.Payment;
using FindSimulator.Share.Claims;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Event;
using FindSimulator.Share.RabbitMq;
using FindSimulator.Share.Results.Concrete;

using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Fleet.Share.ComplexTypes.CommonEnum;

namespace FindSimulator.Service.Concrete
{
   public  class PaymentManager: IPaymentManager
    {

        private IUnitOfWork _unitOfWork;
        private IClaimService _claimService;
         private readonly IEventBus _eventBus;

        public PaymentManager(IUnitOfWork unitOfWork, IClaimService claimService, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _claimService = claimService;
            _eventBus = eventBus;
        }

        public   async Task<DataResult<object>> Add(AddPaymentBO bo)
        {
            var session = await this._unitOfWork.SessionsRepository.GetByIdAsync<Sessions>(bo.SessionID);
             var sessionDetail = await this._unitOfWork.SessionDetailRepository.GetByIdAsync<SessionDetails>(bo.SessionDetailID);


            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = sessionDetail.Data.OrderID.ToString();
            request.Price = sessionDetail.Data.Price.ToString();
            request.PaidPrice = sessionDetail.Data.Price.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = sessionDetail.Data.ID.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = bo.LegalName;
            paymentCard.CardNumber = bo.CreditCardNo;
            paymentCard.ExpireMonth = bo.ExpirationDate.Split('/')[0];
            paymentCard.ExpireYear = bo.ExpirationDate.Split('/')[1];
            paymentCard.Cvc = bo.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = _claimService.UserID.ToString();
            buyer.Name = _claimService.UserName;
            buyer.Surname = _claimService.Surname;
            buyer.GsmNumber = _claimService.TelNo;
            buyer.Email = _claimService.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = sessionDetail.Data.OrderID.ToString();
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = sessionDetail.Data.Price.ToString();
            basketItems.Add(firstBasketItem);

         
            IyzipayCore.Options options = new Options();


            options.ApiKey = "sandbox-5LEsYIWr4OWI8Lt2KYEkWXdd2YQwPiFA";
            options.SecretKey = "sandbox-wY8PvAtX7Uzu5WABnd454knpgAlN2uVc";
            //options.BaseUrl = "https://api.iyzipay.com";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
            Payment payment = Payment.Create(request, options);
            if (payment.PaymentStatus == "successfull")
            {
                sessionDetail.Data.Status = (int)SessionDetailStatus.Sold;
                sessionDetail.Data.UpdateDate = DateTime.Now;
                 await   _unitOfWork.SessionDetailRepository.UpdateOneAsync<SessionDetails>(sessionDetail.Data);
               var data=    await _unitOfWork.CompleteAsync();
                _eventBus.Publish(new LogEventTH() { IP = "", Action = "Payment", Content = JsonConvert.SerializeObject(payment)+"/"+data, UserID = _claimService.UserID }, "LogEventTH");
            }
            return   new DataResult<object>(ResultStatus.Success,payment);

        }
    }
}
