
using FindSimulator.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Concrete.EntityFramework.Context
{
    sealed public class SimulatorContext : DbContext
    {

        public SimulatorContext(DbContextOptions<SimulatorContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        DbSet<Users> Users { get; set; }
        DbSet<AirCraft>  AirCrafts { get; set; }
        DbSet<Simulator>  Simulators { get; set; }
        DbSet<Sessions> Sessions { get; set; }
        DbSet<SimulatorType> SimulatorType { get; set; }
        DbSet<SimulatorDevice> SimulatorDevice { get; set; }
        DbSet<SessionDetails>  SessionDetails { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            
            base.OnModelCreating(modelBuilder);
        }

    }
}
