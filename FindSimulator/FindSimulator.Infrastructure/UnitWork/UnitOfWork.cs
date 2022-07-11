using FindSimulator.Infrastructure.Concrete.EntityFramework.Context;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Repositories.EntityRepository;
using FindSimulator.Infrastructure.Utilities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimulatorContext _simulatorContext;

        public UnitOfWork(SimulatorContext simulatorContext)
        {
            _simulatorContext = simulatorContext;
            UserComponentRepository = new UserComponentRepository(_simulatorContext);
            SessionDetailRepository = new SessionDetailRepository(_simulatorContext);
            SessionsRepository = new SessionsRepository(_simulatorContext);
            UsersRepository = new UsersRepository(_simulatorContext);
            SessionPersonRepository = new SessionPersonRepository(_simulatorContext );
        }


        public ISessionDetailRepository SessionDetailRepository {get;set;}
        public ISessionsRepository SessionsRepository {get;set;}

        public IUserComponentRepository UserComponentRepository { get; set; }


        public IUsersRepository UsersRepository {get;set;}
        public ISessionPersonRepository SessionPersonRepository { get; set; }

        public void BeginTransaction()
        {
            _simulatorContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
           await  _simulatorContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _simulatorContext.Database.CommitTransaction();
        }

        public int Complete()
        {
            try
            {
                CommitTransaction();
                return _simulatorContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public    async Task<int> CompleteAsync()
        {

            try
            {
                return await _simulatorContext.SaveChangesAsync();

            }
            catch (Exception  ex)
            {

                throw;
            }
        }

        public void RollbackTransaction()
        {
            _simulatorContext.Database.RollbackTransaction();
        }
    }
}
