using FindSimulator.Domain.Entities;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Notification
{
    public   partial  interface INotificationRepository
    {
        Task<DataResult<bool>> SenGmail(string body, string senMail);
        Task<DataResult<bool>> SenMailOutlook(string body, string senMail, Users users);
        Task<DataResult<bool>> SendSms(string phoneNumber, string message);
        Task<DataResult<bool>> SendPhoneValidationAsync();
        Task<DataResult<bool>> SendPhoneValidationAsync(string phoneNumber);
        Task<DataResult<bool>> SmsCodeCheck(int smsCode);
        DataResult<bool> SendNotificationFirabase(string deviceID, NotificationModel notificationDto);
        DataResult<bool> SendDeviceNotification(string deviceID, NotificationModel notificationDto);
    }
}
