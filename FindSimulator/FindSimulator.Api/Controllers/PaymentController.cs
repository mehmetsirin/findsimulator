using FindSimulator.Api.Action;
using FindSimulator.Service.Core;
using FindSimulator.Service.Model.Payment;
using FindSimulator.Share.Event;
using FindSimulator.Share.Utils;

using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FindSimulator.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(LogEventAction))]

    public class PaymentController : BaseController
    {
        public PaymentController(BusinessManagerFactory businessManagerFactory) : base(businessManagerFactory)
        {
        }

        [HttpPost]
        public object Add3ds(AddPaymentBO bo)
        {

            Options options;
            options = new Options();
            options.ApiKey = "sandbox-5LEsYIWr4OWI8Lt2KYEkWXdd2YQwPiFA";
            options.SecretKey = "sandbox-wY8PvAtX7Uzu5WABnd454knpgAlN2uVc";
            //options.BaseUrl = "https://api.iyzipay.com";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
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
            request.CallbackUrl = "http://178.18.200.116:96/api/Payment/PaymentResult";


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
            buyer.GsmNumber = "05443544268";
            buyer.Email = "mhmtsirinsk@gmail.com";
            buyer.IdentityNumber = "11111111111";
            buyer.LastLoginDate = "2022-04-21 15:12:09";
            buyer.RegistrationDate = "2022-04-21 15:12:09";
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
            firstBasketItem.Category1 = "test1";
            firstBasketItem.Category2 = "test2";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "1.2";
            basketItems.Add(firstBasketItem);
            request.BasketItems = basketItems;

            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);
            BusinessManagerFactory._eventBus.Publish(new LogEventTH() { IP = "", Action = "PaymentResult", Content = JsonConvert.SerializeObject(threedsInitialize), UserID = 1 }, "LogEventTH");
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
            options.BaseUrl = "https://sandbox-api.iyzipay.com";


            string body = string.Empty;
            using (StreamReader stream = new StreamReader(Request.Body))
                body = stream.ReadToEndAsync().GetAwaiter().GetResult();


            var paymentId = HttpUtility.ParseQueryString(body).Get("paymentId");
            var conversationData = HttpUtility.ParseQueryString(body).Get("conversationData");
            var conversationId = HttpUtility.ParseQueryString(body).Get("conversationId");
            var mdStatus = HttpUtility.ParseQueryString(body).Get("mdStatus");
            var status = HttpUtility.ParseQueryString(body).Get("status");


            Guid _conversationId;
            if (conversationId != null)
                _conversationId = Guid.Parse(conversationId);
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

            BusinessManagerFactory._eventBus.Publish(new LogEventTH() { IP = "", Action = "PaymentResult", Content = JsonConvert.SerializeObject(threedsPayment), UserID = 1 }, "LogEventTH");

            //_dataOperator.UpdateThreeDSRawResult(_conversationId, JsonConvert.SerializeObject(threedsPayment));


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
            string body1 = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body1 = stream.ReadToEndAsync().GetAwaiter().GetResult();
            }
            BusinessManagerFactory._eventBus.Publish(new LogEventTH() { IP = "", Action = "PaymentResult", Content = body1, UserID = 1 }, "LogEventTH");

            string dosya_yolu = @"C:\payment.txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            //Yazma işlemi için bir StreamWriter nesnesi oluşturduk.
            sw.WriteLine("1.Satır:Merhaba:");
            sw.WriteLine("2.Satır:Merhaba:" + body1);
            sw.WriteLine("2.Satır:Merhaba:" + DateTime.Now);

            //Dosyaya ekleyeceğimiz iki satırlık yazıyı WriteLine() metodu ile yazacağız.
            sw.Flush();
            //Veriyi tampon bölgeden dosyaya aktardık.
            sw.Close();
            fs.Close();


            return "Request Finished";
        }

        [HttpPost]
        public  async Task<object> Add(AddPaymentBO bo)
        {
            var  data=  await BusinessManagerFactory._payment.Add(bo);
            return data;
        }

    }
}
