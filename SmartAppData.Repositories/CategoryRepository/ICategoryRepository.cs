using SmartAppData.DAL.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartAppData.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Category category, CancellationToken cancellationToken);
        Task<Category> FindByIdAsync(int id, CancellationToken cancellationToken);
        void Update(Category category);
        void Remove(Category category);
    }
}
