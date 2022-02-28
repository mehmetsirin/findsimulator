using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.IBaseRepository
{
    public interface IBaseRepository_Create<TKey> where TKey : IEquatable<TKey>
    {
        TDocument AddOne<TDocument>(TDocument document) where TDocument : class, IEntity<TKey>;
        Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : IEntity<TKey>;
        void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IEntity<TKey>;
        //Task<TDocument> AddOneAsync<TDocument>(TDocument document) where TDocument : IEntity<TKey>;

    }
}
