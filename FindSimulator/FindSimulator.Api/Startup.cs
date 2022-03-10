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

            //services.AddDbContext<SimulatorContext>(options => options(settings.SqlServerSettings.ConnectionString));
            services.AddDbContext<SimulatorContext>(options => options.UseSqlServer(settings.SqlServerSettings.ConnectionString));
            services.Configure<TokenSettings>(Configuration.GetSection("TokenSettings"));
            services.Configure<DocumentSettings>(Configuration.GetSection("DocumentSettings"));
            services.AddTransient<DbContext, SimulatorContext>();
            services.AddScoped<IJWTAuthenticacationManager, JWTAuthenticacationManager>();
            services.AddScoped(typeof(FindSimulator.Infrastructure.Repositories.BaseRepository.IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseManager<>), typeof(BaseManager<>));

            //services.AddScoped<IJWTAuthenticacationManager>(y => new JWTAuthenticacationManager(settings.TokenSettings.SigningKey));

            services.Repositories();
            services.ServiceRedis();
            services.Jwt(settings.TokenSettings);
            services.Mapper();
            services.RabbitMq(settings.RabbitMQSetting);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FindSimulator.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindSimulator.Api v1"));
            app.App();


            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
