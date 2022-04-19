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
        Task AddManyAsync<TDocument>(List<TDocument> documents, CancellationToken cancellationToken = default) where TDocument :   class,IEntity<TKey>;
        //Task AddManyAsync<TDocument>(List<TDocument> documents) where TDocument : class, IEntity<TKey>;

        void AddMany<TDocument>(List<TDocument> documents) where TDocument : class,IEntity<TKey>;
        Task  AddOneAsync<TDocument>(TDocument document) where TDocument :class, IEntity<TKey>;

    }
}
