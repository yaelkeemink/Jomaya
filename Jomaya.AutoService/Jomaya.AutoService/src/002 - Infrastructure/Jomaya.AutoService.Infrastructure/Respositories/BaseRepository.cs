using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Jomaya.AutoService.Services.Interfaces;

namespace Jomaya.AutoService.Infrastructure.DAL.Repositories
{
    public abstract class BaseRepository<Entity, Key, Context>
   : IRepository<Entity, Key>,
       IDisposable
       where Context : DbContext
       where Entity : class
    {
        protected Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }
        protected abstract DbSet<Entity> GetDbSet();
        protected abstract Key GetKeyFrom(Entity item);

        public virtual Entity Find(Key id)
        {
            return GetDbSet().Single(a => GetKeyFrom(a).Equals(id));
        }

        public virtual IEnumerable<Entity> FindAll()
        {
            return GetDbSet();
        }

        public virtual int Insert(Entity item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public virtual int Update(Entity item)
        {
            _context.Attach(item);
            _context.Update(item);
            return _context.SaveChanges();
        }

        public virtual int Count()
        {
            return GetDbSet().Count();
        }
        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}