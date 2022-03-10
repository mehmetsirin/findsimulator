using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.RedisCache
{
    public static class RedisConnectionFactory
    {
        public static bool connected { get; set; }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        static RedisConnectionFactory()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost" },
                DefaultDatabase = 15
            };

            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }
        public static ConnectionMultiplexer Connection => lazyConnection.Value;

    }
}