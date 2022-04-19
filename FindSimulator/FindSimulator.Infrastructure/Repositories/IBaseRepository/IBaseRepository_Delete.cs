using FindSimulator.Share.Abstract.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.IBaseRepository
{
    public  interface IBaseRepository_Delete<TKey>  where TKey : IEquatable<TKey>
    {
          Task<bool>  DeleteAsync<TDocument>(TKey id) where TDocument : class, IEntity<TKey>;

    }
}
