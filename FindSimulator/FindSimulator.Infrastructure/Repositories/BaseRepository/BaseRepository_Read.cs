using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Repositories.IBaseRepository;
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

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{
    public partial class BaseRepository<TKey> : IBaseReadOnlyRepository<TKey> where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _context = null;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public Task<DataResult<TDocument>> GetByCompanyIdAsync<TDocument>(TKey id, CancellationToken cancellationToken = default) where TDocument : IEntity<TKey>
        {
            throw new NotImplementedException();
        }

        public DataResult<TDocument> GetById<TDocument>( TKey id, string partitionKey = null) where TDocument : class,IEntity<TKey>
        {
            try
            {
                var data = _context.Set<TDocument>().Where(y => y.IsActive == true && Convert.ToInt32(y.ID) == Convert.ToInt32(id)).FirstOrDefault();
                return new DataResult<TDocument>(ResultStatus.Success, data);
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }

        
        public async Task<DataResult<List<TDocument>>> List<TDocument>() where TDocument : class, IEntity<TKey>
        {
            try
            {
                var data =  _context.Set<TDocument>().Where(y => y.IsActive == true).ToList();
                return new DataResult<List<TDocument>>(ResultStatus.Success, data);

            }
            catch (Exception  ex)
            {

                throw;
            }

           
        }

        public   async Task<DataResult<TDocument>> GetByIdAsync<TDocument>(TKey id) where TDocument :class,IEntity<TKey>
        {
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


            try
            {

                if (id.GetType() == typeof(int))
                {
                    var data = _context.Set<TDocument>().ToList().Where((y => Convert.ToInt32(y.ID) == Convert.ToInt32(id) 
                    )).AsQueryable().AsNoTracking().FirstOrDefault();
                    return new DataResult<TDocument>(ResultStatus.Success, data);

                }
                else
                {
                    var data = _context.Set<TDocument>().ToList().Where((y => Convert.ToInt32(y.ID) == Convert.ToInt32(id) && y.IsActive == true)).AsQueryable().AsNoTracking().FirstOrDefault();
                    return new DataResult<TDocument>(ResultStatus.Success, data);
                }

                //var data = _context.Set<TDocument>().AsQueryable().Where(y => Convert.ToInt32(y.ID) == Convert.ToInt32(id)).FirstOrDefault();
                //return new DataResult<TDocument>(ResultStatus.Success, data);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public Task<DataResult<TDocument>> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default) where TDocument :class, IEntity<TKey>
        {
            throw new NotImplementedException();
        }

        public    async  Task<DataResult<IQueryable<TDocument>>> GetQueryable<TDocument>() where TDocument :  class,IEntity<TKey>
        {
           var data =    _context.Set<TDocument>().Where(y => y.IsActive == true).AsQueryable();

            return new DataResult<IQueryable<TDocument>>(ResultStatus.Success,data);
        }

      

      

      
    }
}
