using FindSimulator.Domain.Entities;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using Microsoft.Exchange.WebServices.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Notification
{
    public sealed partial record NotificationRepository : INotificationRepository
    {
        public DataResult<bool> SendDeviceNotification(string deviceID, NotificationModel notificationDto)
        {
            throw new NotImplementedException();
        }

        public DataResult<bool> SendNotificationFirabase(string deviceID, NotificationModel notificationDto)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<bool>> SendPhoneValidationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<bool>> SendPhoneValidationAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<bool>> SendSms(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<bool>> SenGmail(string body, string senMail)
        {
            string UserName = "mhmtsirinsk@gmail.com";
            string Password = "5443544268aA";
            string Host = "smtp.gmail.com";
            string Port = "587";



            try
            {
                SmtpClient smtp = new SmtpClient();
                MailMessage ePosta = new MailMessage();
                smtp.Credentials = new System.Net.NetworkCredential(UserName, Password);
                smtp.Port = int.Parse(Port);
                smtp.Host = Host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;

                ePosta.IsBodyHtml = true;
                ePosta.From = new MailAddress("Rota Araç Takip <rota.internet.net@gmail.com>");
                ePosta.To.Add(senMail);
                ePosta.Subject = "Rota İnternet Teknoloji Hizmetleri Alarm Servisleri";
                ePosta.IsBodyHtml = true;
                ePosta.Body = body;
                smtp.Send(ePosta);
            }
            catch (Exception ex)
            {
                //Logs.Log(ex.StackTrace + "---Hata Açıklaması---BUG---Not Sended Mail : " + item + "----" + ex.Message);
            }
            return new DataResult<bool>(ResultStatus.Success, true);

        }

        public   async Task<DataResult<bool>> SenMailOutlook(string body, string senMail, Users  users)
        {
            if (!string.IsNullOrWhiteSpace(senMail) && senMail != ".")
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
                service.Credentials = new WebCredentials("service.smtp@rota.net.tr", "KEd89zAP470147");

                try
                {
                    string serviceUrl = "https://outlook.office365.com/ews/exchange.asmx";
                    service.Url = new Uri(serviceUrl);
                    EmailMessage emailMessage = new EmailMessage(service);
                    emailMessage.Subject = "Kayıt " + users.UserName + "" +  users.Surname;
                    emailMessage.Body = new MessageBody(body);
                    emailMessage.ToRecipients.Add(senMail);
                    emailMessage.SendAndSaveCopy();
                }
                catch (Exception ex)
                {
                    //Logs.Log(ex.StackTrace + "---Hata Açıklaması---BUG---Not Sended Mail : " + item + "----" + ex.Message);
                }

            }
            return new DataResult<bool>();
        }
        public Task<DataResult<bool>> SmsCodeCheck(int smsCode)
        {
            throw new NotImplementedException();
        }
    }
}
