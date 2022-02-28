using FindSimulator.Infrastructure.Repositories.IBaseRepository;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{

    public partial class BaseRepository<TKey> : DataAccessBase, IBaseRepository_Update<TKey> where TKey : IEquatable<TKey>
    {
           TDocument IBaseRepository_Update<TKey>.UpdateOne<TDocument>(TDocument modifiedDocument)
        {


            _context.Entry(modifiedDocument).State = EntityState.Detached;
            _context.Set<TDocument>().Update(modifiedDocument);
            return modifiedDocument;
        
        }

          async   Task<bool> IBaseRepository_Update<TKey>.UpdateOneAsync<TDocument>(TDocument modifiedDocument)
        {
            _context.Entry(modifiedDocument).State = EntityState.Detached;

            await Task.Run(() => { _context.Set<TDocument>().Update(modifiedDocument); });
            return true ;
        }
    }
}
