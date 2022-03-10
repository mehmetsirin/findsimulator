using FindSimulator.Infrastructure.Repositories.RedisCache;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Concrete;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindSimulator.Api.ServiceCollection
{
     public   static class ServicesRedisExtensions
    {
      public   static  void   ServiceRedis(this IServiceCollection services)
        {
            services.AddScoped<IRedisCache, RedisCache>();
            services.AddScoped<IRedisManager, RedisManager>();


        }

    }
}
