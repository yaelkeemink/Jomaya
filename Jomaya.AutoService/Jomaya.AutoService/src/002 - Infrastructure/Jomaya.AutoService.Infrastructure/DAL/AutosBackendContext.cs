using Jomaya.AutoService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.AutoService.Infrastructure.DAL
{
    public class AutosBackendContext : DbContext
    {
        public virtual DbSet<Auto> Autos { get; set; }
        public virtual DbSet<Onderhoudsopdracht> Onderhoudsopdrachten { get; set; }

        public AutosBackendContext()
        {
            //Database.Migrate();
            Database.EnsureCreated();
        }

        public AutosBackendContext(DbContextOptions options)
            : base(options) { Database.EnsureCreated(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>().Property(r => r.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=db;Database=Jomaya;UserID=sa,Password=admin");
            }
        }
    }
}