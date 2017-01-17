using Microsoft.EntityFrameworkCore;
using Jomaya.Klantenservice.Infrastructure.DAL;
using Jomaya.Klantenservice.Entities;

namespace Jomaya.Klantenservice.Infrastructure.Repositories
{
    public class KlantRepository
        : BaseRepository<Klant, long, KlantContext>
    {
        public KlantRepository(KlantContext context) : base(context)
        {
        }

        protected override DbSet<Klant> GetDbSet()
        {
            return _context.Klanten;
        }

        protected override long GetKeyFrom(Klant item)
        {
            return item.Id;
        }
    }
}