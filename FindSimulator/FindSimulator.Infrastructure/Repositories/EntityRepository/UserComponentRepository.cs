using FindSimulator.Infrastructure.Concrete.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.EntityRepository
{
    public class UserComponentRepository : BaseRepository.BaseRepository<int>, IUserComponentRepository
    {
        public UserComponentRepository(DbContext context) : base(context)
        {
        }
    }
}
