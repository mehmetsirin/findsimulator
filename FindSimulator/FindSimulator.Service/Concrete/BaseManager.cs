using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Infrastructure.Repositories.IBaseRepository;
using FindSimulator.Service.Abstract;
using FindSimulator.Share.Abstract.Model;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class BaseManager<TKey> : IBaseManager<TKey> where TKey : IEquatable<TKey>
    {
        public Infrastructure.Repositories.BaseRepository.IBaseRepository<TKey> baseRepository;

        public BaseManager(Infrastructure.Repositories.BaseRepository.IBaseRepository<TKey> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public    async Task<DataResult<List<TDocument>>> ListAsync<TDocument>() where TDocument: class, IEntity<TKey>
        {
            var res =   await this.baseRepository.List<TDocument>();
            return res;
        }

        public  async   Task<DataResult<TDocument>> GetByIDAsync<TDocument>(TKey ID) where TDocument : class, IEntity<TKey>
        {
            var res =   await baseRepository.GetByIdAsync<TDocument>(ID);
            return res;
           
        }

     public  async   Task<DataResult<IQueryable<TDocument>>>GetQueryable<TDocument>() where TDocument : class, IEntity<TKey>
        {
            var res = await baseRepository.GetQueryable<TDocument>();
            return res;
        }
    }
}
