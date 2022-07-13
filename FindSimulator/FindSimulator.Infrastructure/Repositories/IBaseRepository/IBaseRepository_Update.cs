using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.IBaseRepository
{
   public interface IBaseRepository_Update<TKey> where TKey : IEquatable<TKey>
    
    {
        Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument)
          where TDocument : class, IEntity<TKey>;


        Task<bool> UpdateManyAsync<TDocument>(IList<TDocument> update)
         where TDocument : class, IEntity<TKey>;


        TDocument UpdateOne<TDocument>(TDocument modifiedDocument)
            where TDocument : class, IEntity<TKey>;


        //Task<int> UpdateManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
        //   class, IEntity<TKey>;

        //Task<bool> UpdateOneAsync<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;

        //bool UpdateOne<TDocument, TKey>(TDocument documentToModify, UpdateDefinition<TDocument> update)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;

        //bool UpdateOne<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<bool> UpdateOneAsync<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;

        //Task<bool> UpdateOneAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //bool UpdateOne<TDocument, TKey, TField>(TDocument documentToModify, Expression<Func<TDocument, TField>> field, TField value)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<bool> UpdateOneAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //bool UpdateOne<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<long> UpdateManyAsync<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<long> UpdateManyAsync<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<long> UpdateManyAsync<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //Task<long> UpdateManyAsync<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //long UpdateMany<TDocument, TKey, TField>(Expression<Func<TDocument, bool>> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //long UpdateMany<TDocument, TKey, TField>(FilterDefinition<TDocument> filter, Expression<Func<TDocument, TField>> field, TField value, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //long UpdateMany<TDocument, TKey>(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;


        //long UpdateMany<TDocument, TKey>(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> updateDefinition, string partitionKey = null)
        //    where TDocument : IEntity
        //    where TKey : IEquatable<TKey>;

    }
}
