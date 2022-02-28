using FindSimulator.Domain.Entities;
using FindSimulator.Infrastructure.Concrete.Repositories;
using FindSimulator.Infrastructure.Repositories.BaseRepository;
using FindSimulator.Share.Abstract.Model;
using FindSimulator.Share.Results.Concrete;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Repositories.EntityRepository
{
    public sealed class UsersRepository : BaseRepository<int>, IUsersRepository
    {
        public UsersRepository(DbContext context) : base(context)
        {
        }

    }
}
