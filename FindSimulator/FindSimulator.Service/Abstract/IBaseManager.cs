using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Share.Abstract.Model;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Abstract
{
      public interface IBaseManager<TKey>  where TKey : IEquatable<TKey>
    {

        public Task<DataResult< List<TDocument>>> ListAsync<TDocument>() where TDocument : class, IEntity<TKey>;
    }
}
