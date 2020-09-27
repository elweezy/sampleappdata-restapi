using Microsoft.EntityFrameworkCore;
using SmartAppData.DAL.Entities;
using SmartAppData.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Repositories.CategoryRepository
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(SmartAppDataDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                                 .Where(c => !c.Deleted)
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken);

        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
        }

        public async Task<Category> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.Deleted, cancellationToken);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
