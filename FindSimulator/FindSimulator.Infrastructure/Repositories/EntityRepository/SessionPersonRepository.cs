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
    public class SessionPersonRepository : BaseRepository<int>, ISessionPersonRepository
    {
        public SessionPersonRepository(DbContext context) : base(context)
        {
        }
    }
}
