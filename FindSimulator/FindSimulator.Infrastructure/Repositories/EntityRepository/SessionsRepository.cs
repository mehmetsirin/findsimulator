using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Repositories.BaseRepository;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.EntityRepository
{
      public     partial   class SessionsRepository: BaseRepository<int>, ISessionsRepository
    {
        public SessionsRepository(DbContext context) : base(context)
        {
        }


    }
}
