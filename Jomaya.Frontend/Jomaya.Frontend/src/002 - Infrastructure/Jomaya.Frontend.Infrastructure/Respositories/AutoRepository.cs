using Microsoft.EntityFrameworkCore;
using Jomaya.Frontend.Infrastructure.DAL;
using Jomaya.Frontend.Entities;

namespace Jomaya.Frontend.Infrastructure.DAL.Repositories
{
    public class AutoRepository
        : BaseRepository<Auto, long, FrontEndContext>
    {
        public AutoRepository(FrontEndContext context) : base(context)
        {
        }

        protected override DbSet<Auto> GetDbSet()
        {
            return _context.Autos;
        }

        protected override long GetKeyFrom(Auto item)
        {
            return item.Id;
        }
    }
}