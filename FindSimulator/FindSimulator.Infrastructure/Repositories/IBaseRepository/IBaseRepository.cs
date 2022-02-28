using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.IBaseRepository
{
     public partial interface IBaseRepository<TKey>: IBaseRepository_Create<TKey>, IBaseRepository_Update<TKey>, IBaseRepository_Delete<TKey> where TKey : IEquatable<TKey>
    {
    }
}
