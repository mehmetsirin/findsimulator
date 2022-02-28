using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Share.AppConfiguration
{
    public class AppConfiguration
    {
        public RabbitMQSettings RabbitMQSetting { get; set; }
        public SmtpServerSettings SmtpServer { get; set; }
        public SendGridSettting SendGrid { get; set; }
        public LogServerSettings LogServerSetting { get; set; }
        public RedisSettings RedisSetting { get; set; }
        public AppSettings App { get; set; }
        public string MailList { get; set; }
        public string InfoMailList { get; set; }
        public PressClipperSettings PressClipperSettings { get; set; }
        public PushNotificationSettings PushNotificationSettings { get; set; }

        public SqlServerSettings SqlServerSettings { get; set; }
        public MongoServerSettings MongoServerSettings { get; set; }
        public ReverseGeoSettings ReverseGeoSettings { get; set; }
        public MailSettings MailSettings { get; set; }

        public TokenSettings TokenSettings { get; set; }
    }

    //public   class TokenValidationParametersModel
    //{
    //    public bool SaveSigninToken { get; set; }
    //    public bool ValidateIssuerSigningKey { get; set; }
    //    public bool ValidateIssuer { get; set; }
    //    public bool ValidateAudience { get; set; }
    //    public bool ValidateLifetime { get; set; }
    //    public bool RequireExpirationTime { get; set; }
    //    public bool IssuerSigningKey { get; set; }
    //    public bool ValidIssuer { get; set; }
    //    public bool ValidAudience { get; set; }
    //}

    //public   class JwtSecurityTokenModel
    //{


    public class TokenSettings{
        public string SigningKey { get; set; }
    }

    public class MailSettings
    {
        public string MailUserName { get; set; }
        public string MailPassword { get; set; }
        public string PathUrl { get; set; }
        public string ServiceUrl { get; set; }
        public string MailAddress { get; set; }
        public string TemplatePathUrl { get; set; }
    }
    public class ReverseGeoSettings
    {
        public string ConnectionString { get; set; }

        public string LocalConnectionString { get; set; }
    }
    public class RedisSettings
    {
        public string HostUrl { get; set; }
        public string InstanceName { get; set; }
        public int DbName { get; set; }
        public int ExpireDate { get; set; }
    }
    public class SqlServerSettings
    {
        public string ConnectionString { get; set; }

        public string LocalConnectionString { get; set; }
    }

    public class MongoServerSettings
    {
        public string ConnectionString { get; set; }

        public string LocalConnectionString { get; set; }
    }
    public class SendGridSettting
    {
        public string ApiKey { get; set; }
    }

    public class LogServerSettings
    {
        public SolrLogServerSettings SolrLogServer { get; set; }

        public class SolrLogServerSettings
        {
            public string SolrUrl { get; set; }
        }
    }

    public class SmtpServerSettings
    {
        public string FromAddress { get; set; }
        public string DisplayName { get; set; }
        public string HostName { get; set; }
        public int PortNo { get; set; }
        public string Password { get; set; }
    }

    public class RabbitMQSettings
    {
        public string ConnectionString { get; set; }
    }
    public class SignalrSettings
    {
        public string ConnectionString { get; set; }
        public string LocalConnectionString { get; set; }
    }

    public class AppSettings
    {
        public string ServerRootAddress { get; set; }
        public string ClientRootAddress { get; set; }
    }

    public class PressClipperSettings
    {
        public string Uri { get; set; }
        public string SesId { get; set; }
    }

    public class PushNotificationSettings
    {
        public string Server_Api_Key { get; set; }
        public string Sender_ID { get; set; }
    }
    public class DocumentSettings
    {
        public string Path { get; set; }
        public string PathShow { get; set; }

    }

    public class SmsSettings
    {
        public string kno { get; set; }

        public string kad { get; set; }

        public string ksifre { get; set; }

        public string orjinator { get; set; }

        public string tur { get; set; }
    }

    public class SessionSettings
    {
        public int Timer { get; set; }
    }
}
