using Microsoft.EntityFrameworkCore;
using Jomaya.FoutLogging.Infrastructure.DAL;
using Jomaya.FoutLogging.Entities;

namespace Jomaya.FoutLogging.Infrastructure.Repositories
{
    public class CustomExceptionRepository
        : BaseRepository<CustomException, long, FoutLoggingContext>
    {
        public CustomExceptionRepository(FoutLoggingContext context) : base(context)
        {
        }

        protected override DbSet<CustomException> GetDbSet()
        {
            return _context.Exceptions;
        }

        protected override long GetKeyFrom(CustomException item)
        {
            return item.Id;
        }
    }
}