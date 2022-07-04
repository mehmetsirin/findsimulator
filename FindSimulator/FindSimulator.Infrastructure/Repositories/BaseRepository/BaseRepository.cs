using FindSimulator.Infrastructure.Repositories.IBaseRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.BaseRepository
{


    public  partial interface IBaseRepository<TKey> : IBaseRepository_Create<TKey>, IBaseReadOnlyRepository<TKey>, IBaseRepository_Update<TKey>, IBaseRepository_Delete<TKey> where TKey : IEquatable<TKey>
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
    public partial class BaseRepository<TKey> : IBaseRepository<TKey>
    {
        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();

            }
            catch (Exception  ex)
            {

                throw;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
