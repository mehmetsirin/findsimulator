
using FindSimulator.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Infrastructure.Concrete.EntityFramework.Context
{
    sealed public class SimulatorContext : DbContext
    {
        public readonly ILoggerFactory _loggerFactory;
        public SimulatorContext(DbContextOptions<SimulatorContext> dbContextOptions,ILoggerFactory loggerFactory) : base(dbContextOptions)
        {
            _loggerFactory = loggerFactory;
        }
        DbSet<Users> Users { get; set; }
        DbSet<AirCraft> AirCrafts { get; set; }
        DbSet<Simulator> Simulators { get; set; }
        DbSet<Sessions> Sessions { get; set; }
        DbSet<SimulatorType> SimulatorType { get; set; }
        DbSet<SimulatorDevice> SimulatorDevice { get; set; }
        DbSet<SessionDetails> SessionDetails { get; set; }
        DbSet<Logger> Logger { get; set; }
        DbSet<SessionPerson> SessionPerson { get; set; }
        DbSet<FindSimulatorType> FindSimulatorTypes { get; set; }
        DbSet<SimulatorDeviceLocation> SimulatorDeviceLocations { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Companies> Companies { get; set; }
        DbSet<UserComponent>  UserComponent { get; set; }
        DbSet<PageComponent>  PageComponent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

       
    }
}
