using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.RedisCache
{
    public interface IRedisCache
    {
        Task<DataResult<RedisModel>> Get(string key);

        Task<DataResult<List<RedisModel>>> GetAll();
        Task<DataResult<List<RedisModel>>> GetAll(Func<Exception, bool> filter);
        Task<DataResult<RedisModel>> Set(string key, RedisModel obj, DateTime expireDate);
        Task<DataResult<bool>> Set(string key, string checkCode, DateTime expireDate);
        Task<DataResult<bool>> Delete(string key);

        Task<DataResult<bool>> Exists(string key);
    }
}
