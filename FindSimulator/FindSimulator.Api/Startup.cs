using FindSimulator.Api.Configure;
using FindSimulator.Api.Jwt;
using FindSimulator.Api.ServiceCollection;
using FindSimulator.Infrastructure.Concrete.EntityFramework.Context;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;
using FindSimulator.Share.AppConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Api.Action;
using FindSimulator.Infrastructure.EventBus.EventBusHandler;
using RabbitMQ.Client;
using System;
using FindSimulator.Share.RabbitMq;
using FindSimulator.Share.Event;
using FindSimulator.Api.Filter;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using FindSimulator.Api.Controllers;
using FindSimulator.Infrastructure.UnitWork;
using FindSimulator.Infrastructure.Utilities;
using FindSimulator.Service.Core;
using FindSimulator.Api.Middleware;
using FindSimulator.Share.Scope;
using FindSimulator.Share.Claims;

namespace FindSimulator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var settings = Configuration.Get<AppConfiguration>();
            var d = Configuration.GetSection("DocumentSettings");
            //services.AddDbContext<SimulatorContext>(options => options(settings.SqlServerSettings.ConnectionString));
            //services.AddScoped<LogEventAction>();
            services.AddTransient<LogEventHandler>(provider => { return new LogEventHandler(services); });
            services.Configure<DocumentSettings>(Configuration.GetSection("DocumentSettings"));


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory()
                {
                    Uri = new Uri(settings.RabbitMQSetting.ConnectionString)

                };
                return new DefaultRabbitMQPersistentConnection(factory);
            });
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();


                return new EventBusRabbitMQ(rabbitMQPersistentConnection, sp);
            });

            services.AddScoped<LogEventAction>();

            services.AddDbContext<SimulatorContext>(options => options.UseSqlServer(settings.SqlServerSettings.ConnectionString));
            services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));
            services.Configure<DocumentSettings>(Configuration.GetSection("DocumentSettings"));
            services.AddScoped<DbContext, SimulatorContext>();
            services.AddScoped<IJWTAuthenticacationManager, JWTAuthenticacationManager>();
            services.AddScoped(typeof(FindSimulator.Infrastructure.Repositories.BaseRepository.IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseManager<>), typeof(BaseManager<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BusinessManagerFactory>();

            //services.AddControllers().AddJsonOptions(j =>
            //{
            //    j.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //});
            services.AddTransient<test1, Test1>();
            services.AddTransient<test2, Test2>();
            services.AddTransient<test3, Test3>();
            services.AddMvc(options => { options.Filters.Add<ActionScopeFilter>(); });

            services.AddScoped<IClaimService, ClaimService>();

            services.Repositories();
            services.ServiceRedis();
            services.Jwt(settings.TokenSettings);
            services.Mapper();
            services.RabbitMq(settings.RabbitMQSetting);
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FindSimulator.Api", Version = "v1" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Title = "JWTToken_Auth_API",
                //    Version = "v1"
                //});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<LogEventTH, LogEventHandler>();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindSimulator.Api v1"));
            app.App();
            app.UseCors("MyPolicy");
            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
