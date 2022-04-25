using FindSimulator.Infrastructure.Repositories.IBaseRepository;
using FindSimulator.Share.Abstract.Model;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{
    public partial class BaseRepository<TKey> : DataAccessBase, IBaseRepository_Delete<TKey> where TKey : IEquatable<TKey>
    {
      public  async  Task<bool> DeleteAsync<TDocument>(TKey id) where TDocument : class, IEntity<TKey>
        {
            var data = await GetByIdAsync<TDocument>(id);
            if (data == null)
                return false;
            data.Data.IsActive= false;
              
             UpdateOne<TDocument>(data.Data);
             await SaveChangesAsync();
            return true;
        }
    }
}
