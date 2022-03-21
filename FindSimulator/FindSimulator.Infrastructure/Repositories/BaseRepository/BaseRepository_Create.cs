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
        public void AddMany<TDocument>(List<TDocument> documents) where TDocument : class, IEntity<TKey>
        {
            try
            {
                _context.Set<TDocument>().AddRange(documents);


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

      

        public   async Task AddManyAsync<TDocument>(List<TDocument> documents, CancellationToken cancellationToken = default) where TDocument : class,IEntity<TKey>
        {
            try
            {
                await  _context.Set<TDocument>().AddRangeAsync(documents);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
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


        
    }
}
