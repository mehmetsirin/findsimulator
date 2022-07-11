using FindSimulator.Infrastructure.Concrete.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Utilities
{
  public  interface IUnitOfWork
    {
        public ISessionDetailRepository SessionDetailRepository { get; set; }
        public ISessionsRepository SessionsRepository { get; set; }
        public IUserComponentRepository UserComponentRepository { get; }
        public IUsersRepository  UsersRepository { get; set; }

        public ISessionPersonRepository  SessionPersonRepository { get; set; }
        int Complete();

        Task<int> CompleteAsync();

        void BeginTransaction();

        void RollbackTransaction();

        void CommitTransaction();

        Task BeginTransactionAsync();
    }
}
