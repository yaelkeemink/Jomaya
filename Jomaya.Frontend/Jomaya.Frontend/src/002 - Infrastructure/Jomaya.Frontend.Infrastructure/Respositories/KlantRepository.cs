using System;
using Jomaya.Frontend.Infrastructure.DAL;
using Jomaya.Frontend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jomaya.Frontend.Infrastructure.DAL.Repositories
{
    public class KlantRepository
    : BaseRepository<Klant, long, FrontEndContext>
    {
        public KlantRepository(FrontEndContext context) : base(context)
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