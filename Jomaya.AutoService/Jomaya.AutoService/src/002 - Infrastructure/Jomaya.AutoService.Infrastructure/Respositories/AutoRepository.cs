using Microsoft.EntityFrameworkCore;
using Jomaya.AutoService.Infrastructure.DAL;
using Jomaya.AutoService.Entities;

namespace Jomaya.AutoService.Infrastructure.DAL.Repositories
{
    public class AutoRepository
        : BaseRepository<Auto, long, AutosBackendContext>
    {
        public AutoRepository(AutosBackendContext context) : base(context)
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