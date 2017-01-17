using Jomaya.Frontend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.Frontend.Infrastructure.DAL
{
    public class FrontEndContext : DbContext
    {
        public virtual DbSet<Auto> Autos { get; set; }
        public virtual DbSet<Klant> Klanten { get; internal set; }
        public virtual DbSet<Onderhoudsopdracht> Opdrachten { get; internal set; }

        public FrontEndContext()
        {
            //Database.Migrate();
            Database.EnsureCreated();
        }

        public FrontEndContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            //Staat in startup - Martijn

            //if (!optionsBuilder.IsConfigured) 
            //{
            //    optionsBuilder.UseSqlServer(@"Server=db;Database=GameServer;UserID=sa,Password=admin");
            //}
        }
    }
}