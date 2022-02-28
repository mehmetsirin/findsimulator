using FindSimulator.Infrastructure.Repositories.IBaseRepository;
using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{
    public partial class BaseRepository<TKey> : DataAccessBase, IBaseRepository_Create<TKey> where TKey : IEquatable<TKey>
    {
        public void AddMany<TDocument>(IEnumerable<TDocument> documents) where TDocument : IEntity<TKey>
        {
            throw new NotImplementedException();
        }

        public Task AddManyAsync<TDocument>(IEnumerable<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : IEntity<TKey>
        {
            throw new NotImplementedException();
        }

        public   TDocument AddOne<TDocument>(TDocument document) where TDocument :   class,IEntity<TKey>
        {
            try
            {
                 _context.Set<TDocument>().AddAsync(document);

              
                return document;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }


        //public async Task<TDocument> AddOneAsync<TDocument>(TDocument document) where TDocument : class,IEntity<TKey>
        //{
        //    try
        //    {
        //        await _context.Set<TDocument>().AddAsync(document);
        //        return document;

        //    }
        //    catch (Exception ex)
        //    {

        //        return document;
        //        //throw new Exception(ex.Message.ToString());
        //    }
        //}
    }
}
