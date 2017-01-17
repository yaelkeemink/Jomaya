using Microsoft.EntityFrameworkCore;
using Jomaya.AutoService.Infrastructure.DAL;
using Jomaya.AutoService.Entities;
using System.Linq;

namespace Jomaya.AutoService.Infrastructure.DAL.Repositories
{
    public class OnderhoudRepository
        : BaseRepository<Onderhoudsopdracht, long, AutosBackendContext>
    {
        public OnderhoudRepository(AutosBackendContext context) : base(context)
        {
        }

        protected override DbSet<Onderhoudsopdracht> GetDbSet()
        {
            return _context.Onderhoudsopdrachten;
        }

        protected override long GetKeyFrom(Onderhoudsopdracht item)
        {
            return item.Id;
        }

        public override Onderhoudsopdracht Find(long id)
        {
            return GetDbSet().Include(a => a.Auto).Single(a => GetKeyFrom(a).Equals(id));
        }
    }
}