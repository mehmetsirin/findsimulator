using AutoMapper;

using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Notification;
using FindSimulator.Infrastructure.Repositories.EntityRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.AutoMapper;
using FindSimulator.Service.Concrete;
using FindSimulator.Share.AppConfiguration;
using FindSimulator.Share.RabbitMq;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Api.ServiceCollection
{
    public     static class ServicesExtensions
    {
        public static void Repositories(this IServiceCollection services){
            services.AddScoped<IUserManager, UsersManager>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ISessionsManager, SessionsManager>();
            services.AddScoped<ISessionsRepository, SessionsRepository>();
            services.AddScoped<ISessionDetailManager, SessionDetailManager>();
            services.AddScoped<ISessionDetailRepository, SessionDetailRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ISessionPersonManager, SessionPersonManager>();
            services.AddScoped<ISimulatorDeviceService, SimulatorDeviceService>();
            services.AddScoped<IAirCraftManager, AirCraftManager>();


        }

        public static void Mapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
        public static void RabbitMq(this IServiceCollection services, RabbitMQSettings rabbitMQSettings)
        {

        }

        public static void Jwt(this IServiceCollection services, TokenSettings tokenSettings)
        {
            //var audienceConfig = configuration.GetSection("Audience");

            //var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SigningKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "localhost",
                ValidateAudience = true,
                ValidAudience = "mehmet",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };



            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer("TestKey", x =>
                 {
                     x.RequireHttpsMetadata = false;
                     x.TokenValidationParameters = tokenValidationParameters;
                 });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             //JWT kullanacağım ve ayarları da şunlar olsun dediğimiz yer ise burasıdır.
             .AddJwtBearer(x =>
             {
                 //Gelen isteklerin sadece HTTPS yani SSL sertifikası olanları kabul etmesi(varsayılan true)
                 x.RequireHttpsMetadata = false;
                 //Eğer token onaylanmış ise sunucu tarafında kayıt edilir.
                 x.SaveToken = true;
                 //Token içinde neleri kontrol edeceğimizin ayarları.
                 x.TokenValidationParameters = new TokenValidationParameters
                 {

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = signingKey,
                     ValidateIssuer = true,
                     ValidIssuer = "localhost",
                     ValidateAudience = true,
                     ValidAudience = "mehmet",
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                     RequireExpirationTime = true,
                 };
             });

        }

    }

    }
