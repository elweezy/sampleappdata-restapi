using SmartAppData.DAL.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task<Product> FindByIdAsync(int id, CancellationToken cancellationToken);
        void Update(Product product);
        void Remove(Product product);
    }
}
