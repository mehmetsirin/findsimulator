using FindSimulator.Api.Middleware;

using Microsoft.AspNetCore.Builder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.Configure
{
    public static class ConfigureExtensions
    {

        public static void App(this IApplicationBuilder app)
        {
            app.UseCors("MyPolicy");
            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        public static void Subscribe(this IApplicationBuilder builder)
        {

            //var eventBus = builder.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<LogEvent, LogEventHandler>();
            //eventBus.Subscribe<DictionaryEvent, VehicleDictionaryUpdateHandler>();
        }
    }
}
