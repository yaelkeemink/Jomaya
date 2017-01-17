using Jomaya.IntegratieService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.IntegratieService.Infrastructure.DAL
{
    public class GamesBackendContext : DbContext
    {
        public virtual DbSet<Eigenaar> Games { get; set; }
        public virtual DbSet<Auto> Players { get; set; }

        public GamesBackendContext()
        {
            Database.Migrate();
        }

        public GamesBackendContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(@"Server=db;Database=GameServer;UserID=sa,Password=admin");
            }
        }
    }
}