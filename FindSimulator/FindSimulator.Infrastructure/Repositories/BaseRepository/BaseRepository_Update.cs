using FindSimulator.Infrastructure.Repositories.IBaseRepository;
using FindSimulator.Share.Abstract.Model;

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
        public TDocument UpdateOne<TDocument>(TDocument modifiedDocument) where TDocument : class, IEntity<TKey>
        {
            {


                _context.Entry(modifiedDocument).State = EntityState.Detached;
                _context.Set<TDocument>().Update(modifiedDocument);
                return modifiedDocument;

            }
        }

        public async Task<bool> UpdateOneAsync<TDocument>(TDocument modifiedDocument) where TDocument : class, IEntity<TKey>
        {


            var data = await GetByIdAsync<TDocument>(modifiedDocument.ID);
            modifiedDocument.InsertDate = data.Data.InsertDate;
            modifiedDocument.IsActive = data.Data.IsActive;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _context.Entry(modifiedDocument).State = EntityState.Detached;
            await Task.Run(() => { _context.Set<TDocument>().Update(modifiedDocument); });
            return true;

        }
    }
}