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
        }


        public ISessionDetailRepository SessionDetailRepository {get;set;}
        public ISessionsRepository SessionsRepository {get;set;}

        public IUserComponentRepository UserComponentRepository { get; set; }


        public IUsersRepository UsersRepository {get;set;}

        

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public int Complete()
        {
            try
            {
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
            throw new NotImplementedException();
        }
    }
}
