using FindSimulator.Infrastructure.Repositories.RedisCache;
using FindSimulator.Service.Abstract;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class RedisManager : IRedisManager
    {

        private readonly IRedisCache _redisCache;

        public RedisManager(IRedisCache redisCache)
        {
            this._redisCache = redisCache;
        }

        public Task<DataResult<bool>> Delete(string key)
        {
            var res = _redisCache.Delete(key);
            return res;
        }

        public Task<DataResult<bool>> Exists(string key)
        {
            var res = _redisCache.Exists(key);
            return res;
        }

        public Task<DataResult<RedisModel>> Get(string key)
        {
            var res = _redisCache.Get(key);
            return res;
        }

        public Task<DataResult<List<RedisModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<RedisModel>>> GetAll(Func<Exception, bool> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<RedisModel>> Set(string key, RedisModel obj, DateTime expireDate)
        {
            var res = _redisCache.Set(key, obj, expireDate);
            return res;
        }

        public Task<DataResult<bool>> Set(string key, string checkCode, DateTime expireDate)
        {
            throw new NotImplementedException();
        }

        
    }
}
