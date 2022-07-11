using FindSimulator.Service.Core;
using FindSimulator.Service.Model.Payment;
using FindSimulator.Share.Utils;

using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        public PaymentController(BusinessManagerFactory businessManagerFactory) : base(businessManagerFactory)
        {
        }

        [HttpPost]
        public object Add(AddPaymentBO bo)
        {
            //PaymentUserModelDTO payingUser = ApiRequest.Get<PaymentUserModelDTO>(this.InternalHttpCallPath + "/User/GetUserProfile", this.AccessToken);
            //if (payingUser == null)
            //    return new AddPaymentViewModel() { Status = "FAIL", ErrorMessage = "Unable to find paying user.", Id = Guid.Empty };

            //var command = new AddPaymentCommand()
            //{
            //    ActionUserId =1,
            //    BillingType = bo.BillingType,
            //    CompanyName = bo.CompanyName,//CompanyName ve LegalName
            //    LegalName = bo.LegalName,
            //    PlanId = bo.PlanId,
            //    UserCount = bo.UserCount,
            //    CardHolderName = CryptoHelper.Encrypt(bo.CardHolderName),
            //    CreditCardNo = CryptoHelper.Encrypt(bo.CreditCardNo),
            //    ExpirationDate = CryptoHelper.Encrypt(bo.ExpirationDate),
            //    City = CryptoHelper.Encrypt(bo.City),
            //    Country = CryptoHelper.Encrypt(bo.Country),
            //    ZipCode = CryptoHelper.Encrypt(bo.ZipCode),
            //    State = CryptoHelper.Encrypt(bo.State),
            //    Address = CryptoHelper.Encrypt(bo.Address),
            //    CVV = bo.Cvv
            //};
            //_commandPublisher.Publish(command);

            Options options;
            options = new Options();
            options.ApiKey = "U7zDK0zITab2cqJhE9mNq9z8kub4MXMK";
            options.SecretKey = "HjI2nEH6uaZOklA3ofUnhRdSY1cFSJz2";
            options.BaseUrl = "https://api.iyzipay.com";
            //options.BaseUrl = "https://sandbox-api.iyzipay.com";
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "command.CommandUniqueId.ToString()";//PaymentID
            request.Price = "1.2";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = bo.PlanId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://api.workybe.com:5009/Payment/PaymentResult";


            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = bo.LegalName;
            paymentCard.CardNumber = bo.CreditCardNo;
            paymentCard.ExpireMonth = bo.ExpirationDate.Split('/')[0];
            paymentCard.ExpireYear = bo.ExpirationDate.Split('/')[1];
            paymentCard.Cvc = bo.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            string firstName = string.Empty; string lastName = string.Empty;
            string[] nameArray = bo.LegalName.Split(' ');
            if (nameArray.Length == 1)
                firstName = nameArray[0];
            else
            {
                firstName = nameArray[0];
                lastName = nameArray[1];
            }

            Buyer buyer = new Buyer();
            buyer.Id = 1.ToString();
            buyer.Name = firstName;
            buyer.Surname = lastName;
            buyer.GsmNumber = "payingUser.PhoneNumber";
            buyer.Email = "payingUser.Email";
            buyer.IdentityNumber = "11111111111";
            buyer.LastLoginDate = DateTime.Now.ToString();
            buyer.RegistrationDate = "payingUser.CreatedAt".ToString();
            buyer.RegistrationAddress = bo.Address;
            buyer.Ip = HttpContext.Connection.RemoteIpAddress.ToString();
            buyer.City = bo.City;
            buyer.Country = bo.Country;
            buyer.ZipCode = bo.ZipCode;
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = bo.LegalName;
            shippingAddress.City = bo.City;
            shippingAddress.Country = bo.Country;
            shippingAddress.Description = bo.Address;
            shippingAddress.ZipCode = bo.ZipCode;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = bo.LegalName;
            billingAddress.City = bo.City;
            billingAddress.Country = bo.Country;
            billingAddress.Description = bo.Address;
            billingAddress.ZipCode = bo.ZipCode;
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = bo.PlanId.ToString();
            firstBasketItem.Name = "Workybe Monthly/Yearly Subscription";
            firstBasketItem.Category1 = "WorkyBe";
            firstBasketItem.Category2 = "WorkyBe";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "1.2";
            basketItems.Add(firstBasketItem);
            request.BasketItems = basketItems;

            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);
            if (threedsInitialize.Status == "failure")
                return new AddPaymentViewModel() { Status = "FAIL", ErrorMessage = threedsInitialize.ErrorMessage, Id = Guid.NewGuid() };



            return new AddPaymentViewModel() { Status = "OK", Content = threedsInitialize.HtmlContent, Id = Guid.NewGuid() };
        }
        [HttpGet]
        public object PaymentResult()
        {
            Options options = new Options();

            options.ApiKey = "U7zDK0zITab2cqJhE9mNq9z8kub4MXMK";
            options.SecretKey = "HjI2nEH6uaZOklA3ofUnhRdSY1cFSJz2";
            options.BaseUrl = "https://api.iyzipay.com";


            string body = string.Empty;
            using (StreamReader stream = new StreamReader(Request.Body))
                body = stream.ReadToEndAsync().GetAwaiter().GetResult();


            var paymentId = HttpUtility.ParseQueryString(body).Get("paymentId");
            var conversationData = HttpUtility.ParseQueryString(body).Get("conversationData");
            var conversationId = HttpUtility.ParseQueryString(body).Get("conversationId");
            var mdStatus = HttpUtility.ParseQueryString(body).Get("mdStatus");
            var status = HttpUtility.ParseQueryString(body).Get("status");


            Guid _conversationId = Guid.Parse(conversationId);
            //_dataOperator.UpdatePaymentRawResponse(_conversationId, body);
            //_dataOperator.UpdatePaymentID(_conversationId, paymentId);

            RetrievePaymentRequest request = new RetrievePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = conversationId;
            request.PaymentId = paymentId;
            IyzipayCore.Model.Payment payment = IyzipayCore.Model.Payment.Retrieve(request, options);

            CreateThreedsPaymentRequest paymentRequest = new CreateThreedsPaymentRequest();
            paymentRequest.Locale = Locale.TR.ToString();
            paymentRequest.ConversationId = conversationId;
            paymentRequest.PaymentId = paymentId;
            paymentRequest.ConversationData = conversationData;
            ThreedsPayment threedsPayment = ThreedsPayment.Create(paymentRequest, options);

            //_dataOperator.UpdateThreeDSRawResult(_conversationId, JsonConvert.SerializeObject(threedsPayment));

            if (threedsPayment.Status == "success")
            {
            //    ApiRequest.Put("https://api.workybe.com:4999/monitored_commands/" + conversationId + "/status/success/statusCode/" +
            //        AllConstraints.SuccessCodes.Payment_PaymentSuccessful + "/statusMessage/Ödeme Başarılı", null);

            //    _eventPublisher.Publish(new SuccessfulPaymentEvent()
            //    {
            //        ActionUserId = this.UserId,
            //        UserId = this.UserId,
            //        AmountPaid = Convert.ToDecimal(threedsPayment.PaidPrice),
            //        PaymentId = Guid.Parse(threedsPayment.PaymentId),
            //        PlanId = Guid.Parse(threedsPayment.BasketId),
            //        UserCount = _dataOperator.GetUserCountOfPayment(_conversationId),
            //        BillingPeriod = _dataOperator.GetPlanTypeOfPayment(_conversationId),
            //        FraudStatus = threedsPayment.FraudStatus
            //    });

            //}
            //else
            //{
            //    ApiRequest.Put("https://api.workybe.com:4999/monitored_commands/" + conversationId + "/status/failure/statusCode/" +
            //        AllConstraints.SuccessCodes.Payment_PaymentError + "/statusMessage/" + payment.ErrorMessage, null);
            }
            return "Request Finished";
        }



    }
}
