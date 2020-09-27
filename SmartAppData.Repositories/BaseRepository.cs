using SmartAppData.Persistence.Contexts;

namespace SmartAppData.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SmartAppDataDbContext _context;

        public BaseRepository(SmartAppDataDbContext context)
        {
            _context = context;
        }
    }
}
