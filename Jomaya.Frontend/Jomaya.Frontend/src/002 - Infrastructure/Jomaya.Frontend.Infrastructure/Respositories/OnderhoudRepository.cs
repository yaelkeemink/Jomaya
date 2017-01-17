using Microsoft.EntityFrameworkCore;
using Jomaya.Frontend.Infrastructure.DAL;
using Jomaya.Frontend.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Jomaya.Frontend.Infrastructure.DAL.Repositories
{
    public class OnderhoudRepository
        : BaseRepository<Onderhoudsopdracht, long, FrontEndContext>
    {
        public OnderhoudRepository(FrontEndContext context) : base(context)
        {
        }

        protected override DbSet<Onderhoudsopdracht> GetDbSet()
        {
            return _context.Opdrachten;
        }

        protected override long GetKeyFrom(Onderhoudsopdracht item)
        {
            return item.Id;
        }

        public override IEnumerable<Onderhoudsopdracht> FindBy(Expression<Func<Onderhoudsopdracht, bool>> filter)
        {
            return GetDbSet().Include(a => a.Auto).Include(a => a.Auto.klant).Where(filter);
        }
    }
}