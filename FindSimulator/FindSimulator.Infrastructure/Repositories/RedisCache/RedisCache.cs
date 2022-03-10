using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.RedisCache
{
    public class RedisCache : IRedisCache
    {
        private readonly StackExchange.Redis.IDatabase _redisDb;
        //   private readonly IRedisConnectionFactory _redisConnectionFactory;
        public RedisCache()
        {

            _redisDb = RedisConnectionFactory.Connection.GetDatabase();

        }

        public async Task<DataResult<bool>> Delete(string key)
        {
            DataResult<bool> result = new DataResult<bool>();

            try
            {

                result.Data = _redisDb.KeyDelete(key);
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();

                
            }
            return result;
        }

        public async Task<DataResult<bool>> Exists(string key)
        {

            DataResult<bool> result = new DataResult<bool>();
            try
            {
                result.Data = _redisDb.KeyExists(key);
            }
            catch (Exception ex)
            {

                result.Message = ex.ToString();
            }
            return result;
        }

        public async Task<DataResult<RedisModel>> Get(string key)
        {
            var result = new DataResult<RedisModel>();

            try
            {

                var redisObject = _redisDb.StringGet(key);
                result.Data = redisObject.HasValue ? JsonSerializer.Deserialize<RedisModel>(redisObject) : Activator.CreateInstance<RedisModel>();
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();

            }
            return result;
        }

        public async Task<DataResult<List<RedisModel>>> GetAll()
        {
            var result = new DataResult<List<RedisModel>>();
            try
            {
                var redisObject = _redisDb.StringGet("");
                result.Data = redisObject.HasValue ? JsonSerializer.Deserialize<List<RedisModel>>(redisObject) : Activator.CreateInstance<List<RedisModel>>();
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();

            }
            return result;
        }

        public Task<DataResult<List<RedisModel>>> GetAll(Func<Exception, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<RedisModel>> Set(string key, RedisModel obj, DateTime expireDate)
        {
            var result = new DataResult<RedisModel>();
            try
            {
                var expireTimeSpan = expireDate.Subtract(DateTime.Now);

                _redisDb.StringSet(key, JsonSerializer.Serialize(obj));
                result.Data = obj;
            }
            catch (Exception ex)
            {

                result.Message = ex.ToString();
            }
            return result;
        }

        public async Task<DataResult<bool>> Set(string key, string checkCode, DateTime expireDate)
        {
            var result = new DataResult<bool>();
            try
            {
                //sms kodu için redise atılan kayıt 2 dakika sonra siliniyor
                DateTime origin = expireDate.AddMinutes(2);

                _redisDb.StringSet(key, checkCode);
                _redisDb.KeyExpire(key, origin);
            }
            catch (Exception ex)
            {

                result.Message = ex.ToString();
            }
            return result;
        }
    }
}
