using Microsoft.EntityFrameworkCore;
using SmartAppData.DAL.Entities;
using SmartAppData.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Repositories.ProductRepository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(SmartAppDataDbContext context) : base(context) { }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<Product> FindByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id && !p.Deleted, cancellationToken);
        }

        public async Task<IList<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            IQueryable<Product> queryable = _context.Products
                                                    .Where(p => !p.Deleted)
                                                    .Include(p => p.Category)
                                                    .AsNoTracking();

            return await queryable.ToListAsync(cancellationToken);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }


    }
}
