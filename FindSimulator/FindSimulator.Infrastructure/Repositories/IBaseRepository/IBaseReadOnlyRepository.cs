using FindSimulator.Share.Abstract.Model;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.IBaseRepository
{
    public interface IBaseReadOnlyRepository<TKey> where TKey : IEquatable<TKey>
    {
    //    Task<DataResult<TDocument>> GetByIdAsync<TDocument>(TKey id, string partitionKey = null, CancellationToken cancellationToken = default)
    //where TDocument : IEntity<TKey>;

        DataResult<TDocument> GetById<TDocument>(  TKey id, string partitionKey = null)
        where TDocument :  class ,IEntity<TKey>;
        Task<DataResult<TDocument>> GetByIdAsync<TDocument>(TKey id)
where TDocument :  class, IEntity<TKey>;


        Task<DataResult<TDocument>> GetOneAsync<TDocument>(Expression<Func<TDocument, bool>> filter, string partitionKey = null, CancellationToken cancellationToken = default)
           where TDocument : class,IEntity<TKey>;
        Task<DataResult<IQueryable<TDocument>>> GetQueryable<TDocument>() where TDocument : class, IEntity<TKey>;
        Task<DataResult<List<TDocument>>> List<TDocument>() where TDocument :  class ,IEntity<TKey>;
   


    }
}
