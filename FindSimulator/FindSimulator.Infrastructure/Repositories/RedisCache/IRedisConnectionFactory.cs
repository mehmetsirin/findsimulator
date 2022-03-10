using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.RedisCache
{
    public interface IRedisConnectionFactory
    {
        bool connected { get; set; }



        public static ConnectionMultiplexer Connection;


    }
}
