using Jomaya.FoutLogging.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.FoutLogging.Infrastructure.DAL
{
    public class FoutLoggingContext : DbContext
    {
        //public virtual DbSet<Room> Games { get; set; }
        public virtual DbSet<CustomException> Exceptions { get; set; }

        public FoutLoggingContext()
        {
            Database.EnsureCreated();
        }

        public FoutLoggingContext(DbContextOptions options)  : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomException>().Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<CustomException>()
            .Property(e => e.StackTrace).HasColumnType("TEXT");
            //modelBuilder.Entity<Player>().Property(p => p.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
}