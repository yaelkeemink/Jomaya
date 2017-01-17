using Jomaya.Klantenservice.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.Klantenservice.Infrastructure.DAL
{
    public class KlantContext : DbContext
    {
        public virtual DbSet<Klant> Klanten { get; set; }

        public KlantContext()
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        public KlantContext(DbContextOptions options)
            : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>().Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {            
        }
    }
}