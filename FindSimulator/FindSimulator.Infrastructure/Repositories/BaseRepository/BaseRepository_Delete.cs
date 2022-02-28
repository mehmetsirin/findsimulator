using FindSimulator.Infrastructure.Repositories.IBaseRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{
    public    partial  class BaseRepository<TKey>: DataAccessBase, IBaseRepository_Delete<TKey> where  TKey:IEquatable<TKey>
    {
    }
}
